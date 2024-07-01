using UnityEngine;
using System.Collections.Generic;

public class AttackTrigger : MonoBehaviour
{
    // 트리거에 들어온 오브젝트를 추적할 HashSet
    public HashSet<GameObject> attackedObjects = new HashSet<GameObject>();

    // 공격을 처리하는 메서드
    public void HandleAttack(GameObject target)
    {
        // 여기서 공격 로직을 처리합니다.
        Debug.Log(target.name + "이(가) 공격을 받았습니다.");
    }

    // 트리거에 머무르는 동안 호출되는 메서드
    private void OnTriggerStay(Collider other)
    {
        if (!attackedObjects.Contains(other.gameObject))
        {
            // 공격하지 않은 오브젝트일 경우 공격 처리
            attackedObjects.Add(other.gameObject);
            HandleAttack(other.gameObject);
        }
    }

    // 필요에 따라 트리거에서 나간 오브젝트를 제거하는 메서드
    private void OnTriggerExit(Collider other)
    {
        if (attackedObjects.Contains(other.gameObject))
        {
            attackedObjects.Remove(other.gameObject);
            Debug.Log(other.gameObject.name + "이(가) 트리거를 벗어났습니다.");
        }
    }
}
