using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    private NavMeshAgent agent;
    public Animator animator;
    public bool isAtk;
    public MeleeAtk MeleeAtk;
    public TrailRenderer atkEp;
    public float speed;
    private IState _curState;
    public float detectionRadius = 5f; // 플레이어 감지 범위
    public LayerMask targetLayerMask; // 플레이어 레이어 마스크

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = null; // 플레이어 초기화
        animator = GetComponent<Animator>();
        isAtk = false;
        ChangeState(new IdleState(this)); // 처음에는 IdleState로 시작
    }

    public void ChangeState(IState newState)
    {
        _curState?.ExitState();
        _curState = newState;
        _curState.EnterState();
    }

    void Update()
    {
        _curState?.ExecuteOnUpdate(); // 현재 상태의 Update 메서드 실행
    }

    public void CanAtk()
    {
        isAtk = false;
    }

    public void CantAtk()
    {
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
        MeleeAtk.isAtk = false;
        atkEp.time = 0f;
        atkEp.enabled = false;
    }

    public Transform DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, targetLayerMask);
        foreach (var hit in hits)
        {
            if (((1 << hit.gameObject.layer) & targetLayerMask) != 0)
            {
                player = hit.transform;
                return hit.transform; 
            }
        }
        return null; 
    }
}