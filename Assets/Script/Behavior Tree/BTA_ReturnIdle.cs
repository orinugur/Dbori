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
            // �ʱ�ȭ

            // Ÿ���� ��ȿ���� ������ ����
            if (TargetTransform.Value == null || TargetTransform.Value.gameObject.layer != 3)
            {
                TargetTransform.Value = null;
                return;
            }
        }

        // ���� ���� ���� üũ
        public override TaskStatus OnUpdate()
        {
            // Ÿ���� ������ ����
            if (TargetTransform.Value == null)
            {
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }

    }
}
