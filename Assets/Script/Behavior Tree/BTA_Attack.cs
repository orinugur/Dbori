using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class BTA_Attack : Action
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
            // 초기화
            attackFinished = false;
            agent = GetComponent<NavMeshAgent>();

            // 타겟이 유효하지 않으면 리턴
            if (TargetTransform.Value == null ||
                (TargetTransform.Value.gameObject.layer != 3 && TargetTransform.Value.gameObject.layer != 7))
            {
                TargetTransform.Value = null;
                return;
            }


            // 타겟 위치로 이동
            agent.SetDestination(TargetTransform.Value.position);
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed * 2);
        }

        // 공격 상태 여부 체크
        public override TaskStatus OnUpdate()
        {

            if (TargetTransform != null)
            {
                // 남은 거리가 3 이하일 때 공격 시작
                if (agent.remainingDistance <= 3f && !isAtk)
                {
                    StartAtk();
                }

                // 공격이 끝났다면 성공 상태 반환
                if (attackFinished)
                {
                    isAtk = false;
                    MeleeAtk.isAtk = false;
                    return TaskStatus.Success;
                }
                // 공격 중이면 Running 상태 유지
                return TaskStatus.Running;
            }
            return TaskStatus.Running;
        }

        // 공격 시작
        public void StartAtk()
        {
            MeleeAtk.ResetAtk(); // 공격 초기화
            isAtk = true;
            attackFinished = false; // 공격 완료 상태 초기화
            MeleeAtk.isAtk = true;
            animator.SetTrigger("ATK");
            StartCoroutine(EndAtkk());
            // 애니메이션 이벤트로 EndAtk 호출
        }

        // 코루틴 대기 대신 애니메이션 이벤트로 공격 종료 처리
        IEnumerator EndAtkk()
        {
            yield return new WaitForSecondsRealtime(1f);
            EndAtk();
        }
        public void EndAtk()
        {
            attackFinished = true; // 공격 완료 상태 설정
            Debug.Log("EndAtk");
        }

        // 공격 중지 신호
        public void CanAtk()
        {
            isAtk = false;
        }

        public void CantAtk()
        {
            isAtk = false;
        }
    }
}
