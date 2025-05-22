using System.Collections;
using UnityEngine;

public class AttackState : State<EnemyController>
{
    bool isAttacking;
    [SerializeField] float attackDistance = 1f;
    EnemyController enemy;

    public override void Enter(EnemyController owner)
    {
        enemy = owner;
        enemy.NavAgent.stoppingDistance = attackDistance;
    }

    public override void Execute()
    {
        if (isAttacking)
            return;

        enemy.NavAgent.SetDestination(enemy.target.transform.position);

        if (Vector3.Distance(enemy.target.transform.position, enemy.transform.position) <= attackDistance + 0.03f)
        {
            StartCoroutine(Attack(Random.Range(0, enemy.fighter.Attacks.Count + 1)));
        }

    }

    IEnumerator Attack(int comboCount = 1)
    {
        isAttacking = true;
        enemy.anim.applyRootMotion = true;

        enemy.fighter.TryToAttack();
        Debug.Log("Attack:" + comboCount);
        for (int i = 1; i < comboCount; i++)
        {
            yield return new WaitUntil(() => enemy.fighter.attackState == EnumAttackState.Cooldown);
            enemy.fighter.TryToAttack();
        }

        yield return new WaitUntil(() => enemy.fighter.attackState == EnumAttackState.Idle);

        enemy.anim.applyRootMotion = false;
        isAttacking = false;

        enemy.ChangeState(EnemyStates.RetreatAfterAttack);

    }

    public override void Exit()
    {
        enemy.NavAgent.ResetPath();
    }
}
