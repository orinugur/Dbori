using UnityEngine;
using System.Collections.Generic;

public class MeleeAtk : MonoBehaviour
{
    // Ʈ���ſ� ���� ������Ʈ�� ������ HashSet
    private HashSet<GameObject> attackedObjects = new HashSet<GameObject>();
    public bool isAtk;

    // ������ ó���ϴ� �޼���
    private void HandleAttack(GameObject target)
    {
        // ���⼭ ���� ������ ó���մϴ�.
        Debug.Log(target.name + "��(��) ������ �޾ҽ��ϴ�.");
    }

    // Ʈ���ſ� �ӹ����� ���� ȣ��Ǵ� �޼���
    private void OnTriggerStay(Collider other)
    {
        if (!attackedObjects.Contains(other.gameObject)&&isAtk) //isAtk���϶� �浹���� ���ӿ�����Ʈ�� ������ ������Ʈ �迭�� �ִ�����ȸ��
        {
            attackedObjects.Add(other.gameObject);//�������� ���� ������Ʈ��� ���ݿ�����Ʈ���ٰ� ����
            HandleAttack(other.gameObject);// �������� ���� ������Ʈ�� ��� ���� ó��
        }
    }
    public void ResetAtk() //������ ������ ȣ��
    {
        attackedObjects.Clear();
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (attackedObjects.Contains(other.gameObject))
    //    {
    //        attackedObjects.Remove(other.gameObject);
    //        Debug.Log(other.gameObject.name + "��(��) Ʈ���Ÿ� ������ϴ�.");
    //    }
    //}
}
