using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class BTA_ReturnIdle : Action
    {
        public SharedTransform TargetTransform;
        public Animator animator;

        public override void OnStart()
        {
            // 초기화

            // 타겟이 유효하지 않으면 리턴
            if (TargetTransform.Value == null || TargetTransform.Value.gameObject.layer != 3)
            {
                TargetTransform.Value = null;
                return;
            }
        }

        // 공격 상태 여부 체크
        public override TaskStatus OnUpdate()
        {
            // 타겟이 없으면 실패
            if (TargetTransform.Value == null)
            {
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }

    }
}
