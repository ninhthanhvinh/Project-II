using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class WithinSight : Conditional
    {
        public float fieldOfViewAngle;
        public string targetTag;
        public SharedTransform target;

        private Transform[] possibleTargets;

        public override void OnAwake()
        {
            var targets = GameObject.FindGameObjectsWithTag(targetTag);
            possibleTargets = new Transform[targets.Length];
            for (int i = 0; i < targets.Length; ++i)
            {
                possibleTargets[i] = targets[i].transform;
            }
        }

        public override TaskStatus OnUpdate()
        {
            for (int i = 0; i < possibleTargets.Length; ++i)
            {
                if (WithinSights(possibleTargets[i], fieldOfViewAngle))
                {
                    target.Value = possibleTargets[i];
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Failure;
        }

        public bool WithinSights(Transform targetTransform, float fieldOfViewAngle)
        {
            Vector3 direction = targetTransform.position - transform.position;
            return (Vector3.Angle(direction, transform.forward) < fieldOfViewAngle);
        }


    }
}

