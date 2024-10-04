using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class BTC_DetectPlayer : Conditional
    {
        public float detectionRadius;
        public LayerMask TargetLayerMask;
        public SharedTransform TargetTransform;
        private bool isAwakeAnimationPlayed = false;
        private bool isWeapon = false;
        private bool isCoroutineCompleted = false; // 코루틴 완료 여부 체크
        public Animator animator;
        public GameObject Melee;

        public override void OnStart()
        {
            animator.SetTrigger("Saving");
            isAwakeAnimationPlayed = false;
            isCoroutineCompleted = false; // 코루틴 완료 여부 초기화

            if (Melee.activeSelf)
            {
                isWeapon = true;
            }
        }

        public override TaskStatus OnUpdate()
        {
            Debug.Log("DP");

            // 타겟이 없으면 탐지 로직 실행
            if (TargetTransform.Value == null)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, TargetLayerMask);

                foreach (var hit in hits)
                {
                    Debug.Log("DP1");
                    if (((1 << hit.gameObject.layer) & TargetLayerMask) != 0)
                    {
                        TargetTransform.Value = hit.transform;

                        // 애니메이션 트리거 설정
                        if (!Melee.activeSelf && !isAwakeAnimationPlayed)
                        {
                            animator.SetTrigger("Awake");
                            isAwakeAnimationPlayed = true;
                            StartCoroutine(awake()); // 코루틴 시작
                            return TaskStatus.Running;
                        }
                        else if (Melee.activeSelf)
                        {
                            isCoroutineCompleted= true;
                            isWeapon = true;
                            return TaskStatus.Success;
                        }
                    }
                }
                //return TaskStatus.Running;
            }

            if (isCoroutineCompleted && isWeapon)
            {
                return TaskStatus.Success; // 코루틴 완료 후에 Success 반환
            }

            return TaskStatus.Running; // 코루틴이 완료될 때까지 대기
        }

        private IEnumerator awake()
        {
            //yield return new WaitForSecondsRealtime(3f);
            yield return new WaitForSeconds(3f);
            //if (Melee.activeSelf)
            //{
                isWeapon = true;
            //}
            Debug.Log("weaponawake");
            isCoroutineCompleted = true; // 코루틴 완료 플래그 설정
        }
    }
}
