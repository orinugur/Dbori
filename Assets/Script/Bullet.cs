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
        if (!attackedObjects.Contains(collision.gameObject)) //isAtk중일때 충돌중인 게임오브젝트가 공격한 오브젝트 배열에 있는지조회함
        {
            attackedObjects.Add(collision.gameObject);//공격하지 않은 오브젝트라면 공격오브젝트에다가 담음
            HandleAttack(collision.gameObject);// 공격하지 않은 오브젝트일 경우 공격 처리
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
