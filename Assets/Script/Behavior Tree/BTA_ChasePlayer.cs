using UnityEngine;
using UnityEngine.AI;
namespace BehaviorDesigner.Runtime.Tasks
{
    public class BTA_ChasePlayer : Action
    {
        public NavMeshAgent agent;
        public SharedTransform TargetTransform;
        public Animator animator;

        public override void OnStart()
        {

            agent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            if (TargetTransform.Value == null)
            {
                animator.ResetTrigger("ATK");
                return TaskStatus.Failure;
            }
            if (agent != null)
            {
                agent.SetDestination(TargetTransform.Value.position);
                float speed = agent.velocity.magnitude;
                animator.SetFloat("Speed", speed * 2);
                if (agent.remainingDistance < 3f)
                {
                    // 공격 가능 범위
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Running;
        }
    }
}