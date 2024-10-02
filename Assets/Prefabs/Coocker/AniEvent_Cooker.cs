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
        BTA_Attack attackTask = GetComponent<BTA_Attack>(); // BTA_Attack ������Ʈ ��������
        if (attackTask != null)
        {
            attackTask.EndAtk(); // EndAtk ȣ��
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
