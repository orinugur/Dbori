using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 Inspector에서 할당
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform; // 태그를 이용하여 플레이어의 Transform을 찾습니다.
    }


    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            player = GameObject.FindWithTag("Player").transform; // 태그를 이용하여 플레이어의 Transform을 찾습니다.
        }
    }
    
}