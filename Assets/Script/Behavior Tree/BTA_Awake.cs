using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class BTA_Awake : Action
    {
        public MeleeAtk MeleeAtk;
        public SharedTransform TargetTransform;
        public bool isAtk;
        private NavMeshAgent agent;
        public Animator animator;
        private bool attackFinished;
        public LayerMask TargetLayerMask;

        public override void OnStart()
        {
            animator.SetTrigger("Awake");

        }
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}
