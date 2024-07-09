using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 Inspector에서 할당
    private NavMeshAgent agent;
    public Animator animator;
    public bool isAtk;
    public MeleeAtk MeleeAtk;
    public TrailRenderer atkEp;
    public float speed;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform; // 태그를 이용하여 플레이어의 Transform을 찾습니다.
        //AnimatorController controller = GetComponent<AnimatorController>();
        animator = GetComponent<Animator>();
        isAtk= false;   
    }


    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
            speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);

        }
        else
        {
            player = GameObject.FindWithTag("Player").transform; // 태그를 이용하여 플레이어의 Transform을 찾습니다.
        }
        //Debug.Log(agent.destination);
        //Debug.Log(agent.remainingDistance);
        if (agent.remainingDistance < 3 && isAtk == false)
        {
            isAtk = true;
            animator.SetTrigger("ATK");
            
        }
    }
    public void CanAtk()
    {
        //animator.SetBool("isAtk", false);
        isAtk = false;
    }
    public void CantAtk()
    {
        //animator.SetBool("isAtk", true);
        isAtk = false;
    }
    public void StartAtk()
    {
        MeleeAtk.ResetAtk();
        atkEp.enabled = true;
        atkEp.time = 0.2f;
        MeleeAtk.isAtk = true;
    }
    public void EndAtk()
    {
        MeleeAtk.isAtk=false;
        
        atkEp.time = 0f;
        atkEp.enabled = false;
    }
    
}