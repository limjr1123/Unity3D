using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 적의 상태를 정의하는 열거형
public enum EnemyStates
{
    Idle,   // 대기 상태
    CombatMovement,  // 추적 상태
    Attack,
    RetreatAfterAttack
}

// 적의 상태를 관리하는 컨트롤러 클래스
public class EnemyController : MonoBehaviour
{
    [field: SerializeField] public float fov { get; private set; } = 180f; // 시야각
    [field: SerializeField] public List<MeleeFighter> targetsInRange { get; private set; } = new List<MeleeFighter>();
    public MeleeFighter target { get; set; } // 현재 타겟
    public float combatMovementTimer { get; set; } = 0f;

    // 상태 머신 인스턴스 (현재 상태 관리)
    public StateMachine<EnemyController> stateMachine { get; private set; }

    // 각 EnemyStates와 해당 State 인스턴스를 매핑하는 딕셔너리
    Dictionary<EnemyStates, State<EnemyController>> stateDict;

    public NavMeshAgent NavAgent { get; private set; }  // NavMeshAgent 컴포넌트 

    public Animator anim { get; private set; } // 애니메이터 컴포넌트

    public MeleeFighter fighter { get; private set; }
    // 초기화 메서드
    private void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();    // NavMeshAgent 컴포넌트 가져오기
        anim = GetComponent<Animator>();    // 애니메이터 컴포넌트 가져오기
        fighter = GetComponent<MeleeFighter>();

        stateDict = new Dictionary<EnemyStates, State<EnemyController>>();  // 상태 딕셔너리 생성 및 상태 할당
        stateDict[EnemyStates.Idle] = GetComponent<IdleState>();     // Idle 상태 할당
        stateDict[EnemyStates.CombatMovement] = GetComponent<CombatMovementState>();   // Chase 상태 할당
        stateDict[EnemyStates.Attack] = GetComponent<AttackState>();
        stateDict[EnemyStates.RetreatAfterAttack] = GetComponent<RetreatAfterAttackState>();   

        // 상태 머신 생성 및 초기 상태를 Idle로 설정
        stateMachine = new StateMachine<EnemyController>(this);
        stateMachine.ChangeState(stateDict[EnemyStates.Idle]);
    }

    // 외부에서 상태를 변경할 때 사용하는 메서드
    public void ChangeState(EnemyStates state)
    {
        stateMachine.ChangeState(stateDict[state]);
    }

    public bool IsInState(EnemyStates states) 
    { 
        return stateMachine.CurrentState == stateDict[states];
    }

    Vector3 prevPos;

    // 매 프레임마다 현재 상태의 Execute 메서드 실행
    private void Update()
    {
        stateMachine.Execute();
        
        var deltaPos = transform.position - prevPos;    // 이전 위치와 현재 위치의 차이 계산
        var velocity = deltaPos / Time.deltaTime;       // 이동 속도 계산

        float forwardSpeed = Vector3.Dot(velocity, transform.forward); // 이동 방향과 속도 벡터의 내적 계산

        // magnitude로 이동속도 벡터의 크기를 가져와서 실제 설정된Speed에 맞게 비율을 계산(0~1)
        anim.SetFloat("forwardSpeed", forwardSpeed / NavAgent.speed, 0.2f, Time.deltaTime); // 애니메이터의 이동 속도 설정

        float angle = Vector3.SignedAngle(transform.forward, velocity, Vector3.up);     // 이동 방향과 현재 방향의 각도 계산
        float strafeSpeed = Mathf.Sin(angle * Mathf.Deg2Rad);
        anim.SetFloat("strafeAmount", strafeSpeed, 0.2f, Time.deltaTime); // 애니메이터의 측면 이동 속도 설정

        prevPos = transform.position; // 현재 위치 저장
    }
}