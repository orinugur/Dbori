using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private HashSet<GameObject> attackedObjects = new HashSet<GameObject>();

    [SerializeField]
    public int Dmg = 20;
    private void OnCollisionEnter(Collision collision)
    {
        if (!attackedObjects.Contains(collision.gameObject)) //isAtk���϶� �浹���� ���ӿ�����Ʈ�� ������ ������Ʈ �迭�� �ִ�����ȸ��
        {
            attackedObjects.Add(collision.gameObject);//�������� ���� ������Ʈ��� ���ݿ�����Ʈ���ٰ� ����
            HandleAttack(collision.gameObject);// �������� ���� ������Ʈ�� ��� ���� ó��
        }
    }
    public void HandleAttack(GameObject target)
    {
        HP targetHP = target.GetComponent<HP>();
        if (targetHP != null)
        {
            targetHP.MinusHP(Dmg);
        }
    }
}
