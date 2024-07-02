using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class HP : MonoBehaviour
{
    [SerializeField]
    public int hP;

    public string Type;
    public void Awake()
    {
        Type= LayerMask.LayerToName(gameObject.layer);
        Debug.Log(Type);
    }
    private void Start() //레이어의 타입에따라 체력을부여
    {
        switch (Type)
        {
            case "Enemy":
                hP = 50;
                break;

            case "Player":
                hP = 100;
                break;

            default:
                break;
        }
    }

    public void MinusHP(int Dmg) //체력감소
    {
        hP-=Dmg;
        if(hP <= 0)
        {
            switch(Type)
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

    public void Deadmonster() //몬스터사망시
    {
        gameObject.GetComponent<MonsterAI>().enabled = false;
        gameObject.GetComponent<Collider>().isTrigger = true;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<Animator>().enabled=false;
    }
    public void DeadPlayer()//플레이어 사망시
    {
        gameObject.GetComponent<PlayerMove>().enabled = false;
        gameObject.GetComponent<PlayerInput>().enabled = false;
        gameObject.GetComponent<CharacterController>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
        //transform.GetChild(0).GetComponent<Rigidbody>().AddForce(Vector3.forward,*5f,ForceMode();
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);

        //transform.GetChild(3).gameObject.transform.position += new Vector3(0f, 2f, -1.5f);
        //transform.GetChild(3).gameObject.transform.SetParent(transform.GetChild(0));
    }
}
