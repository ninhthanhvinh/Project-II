using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour, IDamageable
{
    [Tooltip("Maximum health that character has.")]
    public float maxHealth;
    public Transform player;
    public float speed = 5;
    [Tooltip("Maximum mana that character has.")]
    public float maxMana;
    [HideInInspector]
    public float mana;
    [Tooltip("Time that mana will auto regen")]
    public float cdManaReg;
    float cdManaReg_timer;
    [Tooltip("Amount mana will auto regen")]
    public float manaReg_amount;


    [HideInInspector]
    public float currentHealth;
    NavMeshAgent agent;
    Animator anim;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    BehaviorTree tree;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        tree = GetComponent<BehaviorTree>();
        currentHealth = maxHealth;
        mana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        agent.speed = speed;
        Vector3 lookVector = player.transform.position - transform.position;
        lookVector.y = transform.position.y;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);

        if (mana > maxMana)
        {
            mana = maxMana;
        }

        if (cdManaReg_timer <= 0f)
        {
            mana += manaReg_amount;
            cdManaReg_timer = cdManaReg;
        }
        cdManaReg_timer -= Time.deltaTime;

        tree.GetVariable("Mana").SetValue(mana);
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, player.position) < 5f)
        {
            agent.SetDestination(transform.position);
        }
        else agent.SetDestination(player.position);
        Vector3 worldDeltaPos = agent.destination - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPos);
        float dy = Vector3.Dot(transform.forward, worldDeltaPos);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        // Update animation parameters
        anim.SetBool("isMoving", shouldMove);
    }

    public void GetDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0f)
        {
            //Character died.
        }
    }

    public void GetHeal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        //Cant be overhealed
    }

}