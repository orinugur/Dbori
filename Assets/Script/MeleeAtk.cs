using UnityEngine;
using System.Collections.Generic;

public class MeleeAtk : MonoBehaviour
{
    // Ʈ���ſ� ���� ������Ʈ�� ������ HashSet
    [SerializeField]
    private HashSet<GameObject> attackedObjects = new HashSet<GameObject>();
    public bool isAtk;
    [SerializeField]
    public int Dmg = 20;

    // ������ ó���ϴ� �޼���
    private void HandleAttack(GameObject target)
    {
        // ���⼭ ���� ������ ó���մϴ�.
        Debug.Log(target.name + "��(��) ������ �޾ҽ��ϴ�.");
        HP targetHp = target.GetComponent<HP>();
        if (targetHp != null)
        {
            targetHp.MinusHP(Dmg);
        }
    }

    // Ʈ���ſ� �ӹ����� ���� ȣ��Ǵ� �޼���
    private void OnTriggerStay(Collider other)
    {
        if (!attackedObjects.Contains(other.gameObject)&&isAtk) //isAtk���϶� �浹���� ���ӿ�����Ʈ�� ������ ������Ʈ �迭�� �ִ�����ȸ��
        {

            if (gameObject.transform.parent && other.gameObject == gameObject.transform.parent.gameObject)
            {
                Debug.Log("�θ� ������Ʈ�� �浹�� - ���� ó��");
                return; // �θ� ������Ʈ���� �浹�� ����
            }
            attackedObjects.Add(other.gameObject);//�������� ���� ������Ʈ��� ���ݿ�����Ʈ���ٰ� ����
            HandleAttack(other.gameObject);// �������� ���� ������Ʈ�� ��� ���� ó��
        }
    }
    public void ResetAtk() //������ ������ ȣ��
   {
        attackedObjects.Clear();
    }

}
