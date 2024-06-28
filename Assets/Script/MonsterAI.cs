using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� Inspector���� �Ҵ�
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform; // �±׸� �̿��Ͽ� �÷��̾��� Transform�� ã���ϴ�.
    }


    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            player = GameObject.FindWithTag("Player").transform; // �±׸� �̿��Ͽ� �÷��̾��� Transform�� ã���ϴ�.
        }
    }
    
}