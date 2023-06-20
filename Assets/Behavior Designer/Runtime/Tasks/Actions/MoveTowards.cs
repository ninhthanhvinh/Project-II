using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class MoveTowards : Action
    {
        public float speed;
        public float attackRange;
        public SharedTransform target;
        public SharedTransform self;

        NavMeshAgent navMeshAgent;

        public override void OnAwake()
        {
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            // Return a task status of success once we've reached the target
            if (Vector3.SqrMagnitude(self.Value.position - target.Value.position) >= attackRange)
            {
                // We haven't reached the target yet so keep moving towards it
                navMeshAgent.speed = speed;
                navMeshAgent.SetDestination(target.Value.position);
                return TaskStatus.Running;
            }

            navMeshAgent.SetDestination(transform.position);
            return TaskStatus.Success;
        }
    }
}