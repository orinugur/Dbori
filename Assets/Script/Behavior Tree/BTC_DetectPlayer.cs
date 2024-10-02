using System.Collections;
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
        public GameObject Melee;
        public override void OnStart()
        {
            animator.SetTrigger("Saving");
                                isAwakeAnimationPlayed = false; // ���� �ʱ�ȭ
        }
        public override TaskStatus OnUpdate()
        {

            Debug.Log("DP");
            // Ÿ���� ������ Ž�� ���� ����
            if (TargetTransform.Value == null)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, TargetLayerMask);

                foreach (var hit in hits)
                {
                    Debug.Log("DP1");
                    if (((1 << hit.gameObject.layer) & TargetLayerMask) != 0)
                    {
                        TargetTransform.Value = hit.transform;

                        // �ִϸ��̼� Ʈ���� ����
                        if (!Melee.activeSelf)
                        {
                            animator.SetTrigger("Awake");
                            isAwakeAnimationPlayed = true; // �ִϸ��̼� ��� ���� ����
                            return TaskStatus.Running; // ���� ���·� ��ȯ���� �ʰ� ���
                        }
                        else
                        {
                            return TaskStatus.Success;
                        }
                    }
                }
                return TaskStatus.Running; // Ÿ���� ������ ���
            }
            else if (isAwakeAnimationPlayed)
            {
                var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                if (!stateInfo.IsName("Awake")) // Awake ���°� �ƴϸ� Success ��ȯ
                {

                    return TaskStatus.Success; // ���� ��ȯ

                }
                Debug.Log("Awake �ִϸ��̼� ��� ��...");
            }
            //else if(!isAwakeAnimationPlayed&&TargetTransform.Value!=null)
            //{
            //    return TaskStatus.Success; // ���� ��ȯ
            //}
            return TaskStatus.Running; // ���� ���̸� ��� ���
        }

        IEnumerator awake()
        {
            yield return new WaitForSecondsRealtime(1f);
        }
    }
    

}