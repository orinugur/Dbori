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
            // �ʱ�ȭ
            attackFinished = false;
            agent = GetComponent<NavMeshAgent>();

           StopAllCoroutines();
            // Ÿ���� ��ȿ���� ������ ����
            if (TargetTransform.Value == null ||
                (TargetTransform.Value.gameObject.layer != 3 && TargetTransform.Value.gameObject.layer != 7))
            {
                TargetTransform.Value = null;
                return;
            }


            // Ÿ�� ��ġ�� �̵�
            //agent.SetDestination(TargetTransform.Value.position);
            //float speed = agent.velocity.magnitude;
            //animator.SetFloat("Speed", speed * 2);
        }

        // ���� ���� ���� üũ
        public override TaskStatus OnUpdate()
        {
            if (TargetTransform != null)
            {
                // �÷��̾ NavMesh ���� �ۿ� �ִ��� Ȯ��
                if (!agent.isOnNavMesh)
                {
                    Debug.LogWarning("NavMeshAgent�� NavMesh �������� ������ϴ�.");
                    StopAllCoroutines();
                    return TaskStatus.Failure; // �Ǵ� ������ ���� ��ȯ
                }

                // ���� �Ÿ��� 3 ������ �� ���� ����
                if (agent.remainingDistance <= 3f && !isAtk)
                {
                    StartAtk();
                }
                else if (agent.remainingDistance >= 3f)
                {
                    agent.SetDestination(TargetTransform.Value.position);
                    float speed = agent.velocity.magnitude;
                    animator.SetFloat("Speed", speed * 2);
                }

                // ������ �����ٸ� ���� ���� ��ȯ
                if (attackFinished)
                {
                    isAtk = false;
                    MeleeAtk.isAtk = false;
                    return TaskStatus.Success;
                }

                // ���� ���̸� Running ���� ����
                return TaskStatus.Running;
            }
            else
                animator.ResetTrigger("ATK");
                return TaskStatus.Failure;
        }

        // ���� ����
        public void StartAtk()
        {
            MeleeAtk.ResetAtk(); // ���� �ʱ�ȭ
            isAtk = true;
            attackFinished = false; // ���� �Ϸ� ���� �ʱ�ȭ
            MeleeAtk.isAtk = true;
            animator.SetTrigger("ATK");
            //agent.SetDestination(TargetTransform.Value.position);
            //float speed = agent.velocity.magnitude;
            //animator.SetFloat("Speed", speed * 2);
            StartCoroutine(EndAtkk());
            // �ִϸ��̼� �̺�Ʈ�� EndAtk ȣ��
        }

        // �ڷ�ƾ ��� ��� �ִϸ��̼� �̺�Ʈ�� ���� ���� ó��
        IEnumerator EndAtkk()
        {
            yield return new WaitForSecondsRealtime(1f);
            EndAtk();
        }
        public void EndAtk()
        {
            attackFinished = true; // ���� �Ϸ� ���� ����
            Debug.Log("EndAtk");
        }

        // ���� ���� ��ȣ
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
