using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class HP : MonoBehaviour
{
    [SerializeField]
    public int hP;

    public string Type;
    public TextMeshProUGUI HpUI;
    public void Awake()
    {
        Type= LayerMask.LayerToName(gameObject.layer);
        Debug.Log(Type);
        
    }
    private void Start() //레이어의 타입에따라 체력을부여
    {
        switch (Type)
        {
            //case "Enemy":
            //    hP = 50;
            //    break;

            case "Player":
                hP = 100;
                SetUi(hP);
                break;

            default:
                break;
        }
    }

    public void MinusHP(int Dmg) //체력감소
    {
        hP-=Dmg;
        switch (Type)
        {
            case "Player":
                SetUi(hP);
                break;
            default:
                break;
        }
        if (hP <= 0)
        {
            switch(Type)
            {
                case "Enemy":
                    Deadmonster();
                    break;

                case "Player":
                    DeadPlayer();
                    break;
                case "Cooker":
                    DeadCooker();
                    break;

                default:
                    break;
            }
        }
    }
    public void PlusHP(int point) //체력감소
    {
        hP += point;
        switch (Type)
        {
            case "Player":
                SetUi(hP);
                break;
            default:
                break;
        }
        if (hP <= 0)
        {
            switch (Type)
            {
                case "Enemy":
                    Deadmonster();
                    break;

                case "Player":
                    DeadPlayer();
                    break;

                default:
                    break;
            }
        }
    }

    public void SetUi(int hP)
    {
        HpUI.text = "HP : " + hP;
    }
    public void Deadmonster() //몬스터사망시
    {
        gameObject.GetComponent<MonsterAI>().enabled = false;
        gameObject.GetComponent<Collider>().isTrigger = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<Animator>().enabled=false;
        gameObject.layer = 0;
        enabled = false;
    }
    public void DeadCooker()
    {
        gameObject.GetComponent<BehaviorTree>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<Collider>().isTrigger = true;
        gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Dead");
        gameObject.layer = 0;
        enabled = false;

    }
    public void DeadPlayer()//플레이어 사망시
    {
        gameObject.GetComponent<PlayerMove>().enabled = false;
        gameObject.GetComponent<PlayerInput>().enabled = false;
        gameObject.GetComponent<CharacterController>().enabled = false;
        gameObject.layer = 0; 
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.layer = 0; 
        }
        transform.GetChild(0).gameObject.SetActive(true);
        //transform.GetChild(0).GetComponent<Rigidbody>().AddForce(Vector3.forward,*5f,ForceMode();
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);
        RoomManager.Instance.FailExit();

        enabled = false;

        //transform.GetChild(3).gameObject.transform.position += new Vector3(0f, 2f, -1.5f);
        //transform.GetChild(3).gameObject.transform.SetParent(transform.GetChild(0));
    }

}
