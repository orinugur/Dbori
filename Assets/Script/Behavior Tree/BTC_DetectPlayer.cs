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
        private bool isCoroutineCompleted = false; // �ڷ�ƾ �Ϸ� ���� üũ
        public Animator animator;
        public GameObject Melee;

        public override void OnStart()
        {
            animator.SetTrigger("Saving");
            isAwakeAnimationPlayed = false;
            isCoroutineCompleted = false; // �ڷ�ƾ �Ϸ� ���� �ʱ�ȭ

            if (Melee.activeSelf)
            {
                isWeapon = true;
            }
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
                        if (!Melee.activeSelf && !isAwakeAnimationPlayed)
                        {
                            animator.SetTrigger("Awake");
                            isAwakeAnimationPlayed = true;
                            StartCoroutine(awake()); // �ڷ�ƾ ����
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
                return TaskStatus.Success; // �ڷ�ƾ �Ϸ� �Ŀ� Success ��ȯ
            }

            return TaskStatus.Running; // �ڷ�ƾ�� �Ϸ�� ������ ���
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
            isCoroutineCompleted = true; // �ڷ�ƾ �Ϸ� �÷��� ����
        }
    }
}
