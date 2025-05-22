using UnityEngine;

/// <summary>
/// 적이 공격 후 일정 거리만큼 뒤로 물러나는(후퇴) 상태를 담당하는 클래스입니다.
/// </summary>
public class RetreatAfterAttackState : State<EnemyController>
{
    [SerializeField] float backwardWalkSpeed = 1.5f;    // 후퇴 시 이동 속도
    [SerializeField] float distanceToRetreat = 3f;    // 후퇴를 멈출 거리(타겟과의 최소 거리)


    // 상태 소유자(EnemyController) 참조
    EnemyController enemy;

    /// <summary>
    /// 상태 진입 시 소유자(EnemyController) 저장
    /// </summary>
    public override void Enter(EnemyController owner)
    {
        enemy = owner;
    }

    /// <summary>
    /// 매 프레임 호출: 타겟과의 거리가 충분히 멀어지면 CombatMovement 상태로 전환,
    /// 그렇지 않으면 타겟 반대 방향으로 후퇴 이동
    /// </summary>
    public override void Execute()
    {
        // 타겟과의 거리가 충분히 멀어지면 전투 이동 상태로 전환
        if (Vector3.Distance(enemy.transform.position, enemy.target.transform.position) >= distanceToRetreat)
        {
            enemy.ChangeState(EnemyStates.CombatMovement);
            return;
        }

        // 타겟 방향 벡터 계산
        var vecToTarget = enemy.target.transform.position - enemy.transform.position;
        // 타겟 반대 방향(뒤로) 이동
        enemy.NavAgent.Move(-vecToTarget.normalized * backwardWalkSpeed * Time.deltaTime);

        // y축 회전만 고려하여 타겟 방향으로 부드럽게 회전
        vecToTarget.y = 0f;
        Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(vecToTarget), 500 * Time.deltaTime);
    }
}