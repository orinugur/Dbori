using UnityEngine;
using System.Collections.Generic;

public class MeleeAtk : MonoBehaviour
{
    // 트리거에 들어온 오브젝트를 추적할 HashSet
    [SerializeField]
    private HashSet<GameObject> attackedObjects = new HashSet<GameObject>();
    public bool isAtk;
    [SerializeField]
    public int Dmg = 20;

    // 공격을 처리하는 메서드
    private void HandleAttack(GameObject target)
    {
        // 여기서 공격 로직을 처리합니다.
        Debug.Log(target.name + "이(가) 공격을 받았습니다.");
        HP targetHp = target.GetComponent<HP>();
        if (targetHp != null)
        {
            targetHp.MinusHP(Dmg);
        }
    }

    // 트리거에 머무르는 동안 호출되는 메서드
    private void OnTriggerStay(Collider other)
    {
        if (!attackedObjects.Contains(other.gameObject)&&isAtk) //isAtk중일때 충돌중인 게임오브젝트가 공격한 오브젝트 배열에 있는지조회함
        {

            if (gameObject.transform.parent && other.gameObject == gameObject.transform.parent.gameObject)
            {
                Debug.Log("부모 오브젝트와 충돌함 - 예외 처리");
                return; // 부모 오브젝트와의 충돌은 무시
            }
            attackedObjects.Add(other.gameObject);//공격하지 않은 오브젝트라면 공격오브젝트에다가 담음
            HandleAttack(other.gameObject);// 공격하지 않은 오브젝트일 경우 공격 처리
        }
    }
    public void ResetAtk() //공격이 끝날시 호출
   {
        attackedObjects.Clear();
    }

}
