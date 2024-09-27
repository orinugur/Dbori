using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{

    public class BTC_DetectPlayer : Conditional
    {
        public float detectionRadius;
        public LayerMask TargetLayerMask;
        //private Transform TargetTransform;
        public SharedTransform TargetTransform;
        private bool isAwakeAnimationPlayed = false; // �ִϸ��̼� ��� ���� üũ
        public Animator animator;
        public override void OnStart()
        {
            if(TargetTransform==null)
            animator.SetTrigger("Saving");
        }
        public override TaskStatus OnUpdate()
        {
            // Ÿ���� ������ Ž�� ���� ����
            if (TargetTransform.Value == null)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, TargetLayerMask);
                foreach (var hit in hits)
                {
                    if (hit.CompareTag("Player") || hit.CompareTag("Enemy"))
                    {
                        TargetTransform.Value = hit.transform;

                        // �ִϸ��̼� Ʈ���� ����
                        animator.SetTrigger("Awake");
                        isAwakeAnimationPlayed = true; // �ִϸ��̼� ��� ���� ����

                        return TaskStatus.Running; // ���� ���·� ��ȯ���� �ʰ� ���
                    }
                }
                return TaskStatus.Running; // Ÿ���� ������ ���
            }
            else if (isAwakeAnimationPlayed)
            {
                var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                if (!stateInfo.IsName("Awake")) // Awake ���°� �ƴϸ� Success ��ȯ
                {
                    isAwakeAnimationPlayed = false; // ���� �ʱ�ȭ
                    return TaskStatus.Success; // ���� ��ȯ
                }
                Debug.Log("Awake �ִϸ��̼� ��� ��...");
            }
            else if(!isAwakeAnimationPlayed&&TargetTransform.Value!=null)
            {
                return TaskStatus.Success; // ���� ��ȯ
            }
            return TaskStatus.Running; // ���� ���̸� ��� ���
        }

    }

}