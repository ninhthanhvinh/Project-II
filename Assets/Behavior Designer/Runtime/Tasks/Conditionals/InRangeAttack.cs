using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorDesigner.Runtime.Tasks
{
    public class InRangeAttack : Conditional
    {
        public SharedTransform target;
        public SharedTransform self;
        public float attackRange;
        public override TaskStatus OnUpdate()
        {
            if (Vector3.Distance(target.Value.position, self.Value.position) < attackRange)
                return TaskStatus.Success;
            return TaskStatus.Failure;
        }
    }
}

