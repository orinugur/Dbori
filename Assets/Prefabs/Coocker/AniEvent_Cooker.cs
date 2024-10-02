using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEvent_Cooker : MonoBehaviour
{
    public GameObject meleeWeapon;
    // Start is called before the first frame update
    void EndATK()
    {
        BTA_Attack attackTask = GetComponent<BTA_Attack>(); // BTA_Attack 컴포넌트 가져오기
        if (attackTask != null)
        {
            attackTask.EndAtk(); // EndAtk 호출
        }

    }
    public void Weapon()
    {
        if(meleeWeapon != null)
        {
            meleeWeapon.SetActive(true);
        }
        
    }


}
