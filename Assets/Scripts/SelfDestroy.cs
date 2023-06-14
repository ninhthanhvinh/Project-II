using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField]
    float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(GetDestroy), lifetime);
    }

    // Update is called once per frame
    void GetDestroy()
    {
        Destroy(gameObject);
    }
}
