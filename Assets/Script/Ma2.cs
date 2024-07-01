using UnityEngine;
using System.Collections.Generic;

public class AttackTrigger : MonoBehaviour
{
    // Ʈ���ſ� ���� ������Ʈ�� ������ HashSet
    public HashSet<GameObject> attackedObjects = new HashSet<GameObject>();

    // ������ ó���ϴ� �޼���
    public void HandleAttack(GameObject target)
    {
        // ���⼭ ���� ������ ó���մϴ�.
        Debug.Log(target.name + "��(��) ������ �޾ҽ��ϴ�.");
    }

    // Ʈ���ſ� �ӹ����� ���� ȣ��Ǵ� �޼���
    private void OnTriggerStay(Collider other)
    {
        if (!attackedObjects.Contains(other.gameObject))
        {
            // �������� ���� ������Ʈ�� ��� ���� ó��
            attackedObjects.Add(other.gameObject);
            HandleAttack(other.gameObject);
        }
    }

    // �ʿ信 ���� Ʈ���ſ��� ���� ������Ʈ�� �����ϴ� �޼���
    private void OnTriggerExit(Collider other)
    {
        if (attackedObjects.Contains(other.gameObject))
        {
            attackedObjects.Remove(other.gameObject);
            Debug.Log(other.gameObject.name + "��(��) Ʈ���Ÿ� ������ϴ�.");
        }
    }
}
