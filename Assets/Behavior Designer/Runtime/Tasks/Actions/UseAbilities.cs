using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class UseAbilities : Action
    {

        public Abilities abilitiesToUse;

        public override void OnStart()
        {
            abilitiesToUse.GetComponent<NavMeshAgent>().SetDestination(abilitiesToUse.gameObject.transform.position);
            abilitiesToUse.GetUse();
        }
        public override TaskStatus OnUpdate()
        {

            return TaskStatus.Success;
        }
    }
}

