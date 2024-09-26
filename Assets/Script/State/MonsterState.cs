using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public interface IState
{
    void EnterState();
    void ExitState();
    void ExecuteOnUpdate();
}

public class StateBase : IState
{
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void ExecuteOnUpdate() { }
}

public class IdleState : StateBase
{
    private readonly MonsterAI _monster;
    private float wanderTimer = 3f; // NavMesh에서 이동할 시간 간격
    private float timer;
    private NavMeshAgent agent;
    private Animator animator;

    public IdleState(MonsterAI monster)
    {
        _monster = monster;
        timer = wanderTimer; // 타이머 초기화
        agent = _monster.GetComponent<NavMeshAgent>();
        animator = _monster.GetComponent<Animator>();
    }

    public override void EnterState()
    {
    }

    public override void ExitState()
    {
    }

    public override void ExecuteOnUpdate()
    {
        if (_monster.player == null)
        {
            _monster.player = _monster.DetectPlayer();
        }

        if (_monster.player != null)
        {
            _monster.ChangeState(new ChaseState(_monster));
            Debug.Log("ChaseState"); 
            return;
        }

        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(_monster.transform.position, 10f, -1); 
            agent.SetDestination(newPos);
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed*1.5f);

            timer = 0;
        }
    }


    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

}
public class ChaseState : StateBase
{
    private readonly MonsterAI _monster;
    private NavMeshAgent agent;
    private Animator animator;

    public ChaseState(MonsterAI monster)
    {
        _monster = monster;
        agent = _monster.GetComponent<NavMeshAgent>();
        animator = _monster.GetComponent<Animator>();
    }

    public override void EnterState()
    {
    }

    public override void ExecuteOnUpdate()
    {
        if (_monster.player != null)
        {
            if(_monster.player.gameObject.layer!= 3)
            {
                _monster.ChangeState(new IdleState(_monster));
                return;
            }
            agent.SetDestination(_monster.player.position);
            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed*2);

            if (agent.remainingDistance < 3f && !_monster.isAtk)
            {
                _monster.isAtk = true;
                animator.SetTrigger("ATK");
            }

            if (_monster.isAtk && agent.remainingDistance >= 3f)
            {
                _monster.isAtk = false;
            }
        }
    }

    public override void ExitState()
    {
        _monster.player = null;
        Debug.Log("IdleState");
    }
}