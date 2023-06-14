using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class UseAbilities : Action
    {
        public Abilities abilitiesToUse;

        public override void OnStart()
        {
            abilitiesToUse.GetUse();
        }
        public override TaskStatus OnUpdate()
        {

            return TaskStatus.Success;
        }
    }
}

