using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{

    public class BTC_DetectPlayer : Conditional
    {
        public float detectionRadius;
        public LayerMask TargetLayerMask;
        //private Transform TargetTransform;
        public SharedTransform TargetTransform;
        private bool isAwakeAnimationPlayed = false; // 애니메이션 재생 여부 체크
        public Animator animator;
        public override void OnStart()
        {
            if(TargetTransform==null)
            animator.SetTrigger("Saving");
        }
        public override TaskStatus OnUpdate()
        {
            // 타겟이 없으면 탐지 로직 실행
            if (TargetTransform.Value == null)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, TargetLayerMask);
                foreach (var hit in hits)
                {
                    if (hit.CompareTag("Player") || hit.CompareTag("Enemy"))
                    {
                        TargetTransform.Value = hit.transform;

                        // 애니메이션 트리거 설정
                        animator.SetTrigger("Awake");
                        isAwakeAnimationPlayed = true; // 애니메이션 재생 상태 설정

                        return TaskStatus.Running; // 성공 상태로 반환하지 않고 대기
                    }
                }
                return TaskStatus.Running; // 타겟이 없으면 대기
            }
            else if (isAwakeAnimationPlayed)
            {
                var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                if (!stateInfo.IsName("Awake")) // Awake 상태가 아니면 Success 반환
                {
                    isAwakeAnimationPlayed = false; // 상태 초기화
                    return TaskStatus.Success; // 성공 반환
                }
                Debug.Log("Awake 애니메이션 재생 중...");
            }
            else if(!isAwakeAnimationPlayed&&TargetTransform.Value!=null)
            {
                return TaskStatus.Success; // 성공 반환
            }
            return TaskStatus.Running; // 진행 중이면 계속 대기
        }

    }

}