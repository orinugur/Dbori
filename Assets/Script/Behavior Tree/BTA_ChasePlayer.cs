using UnityEngine.AI;
namespace BehaviorDesigner.Runtime.Tasks
{
    public class BTA_ChasePlayer : Action
    {
        public NavMeshAgent agent;
        public SharedTransform TargetTransform;

        public override void OnStart()
        {
            
            agent = GetComponent<NavMeshAgent>();
        }

        public override TaskStatus OnUpdate()
        {
            if (TargetTransform.Value == null) return TaskStatus.Failure;

            if (agent != null)
            {
                agent.SetDestination(TargetTransform.Value.position);
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