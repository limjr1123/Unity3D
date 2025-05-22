using UnityEngine;

public class IdleState : State<EnemyController>
{
    EnemyController enemy;

    public override void Enter(EnemyController owner)
    {
        enemy = owner;
    }

    public override void Execute()
    {
        foreach (var target in enemy.targetsInRange)
        {
            var vecToTarget = target.transform.position - enemy.transform.position;
            float angle = Vector3.Angle(transform.forward, vecToTarget);

            if(angle <= enemy.fov / 2)
            {
                enemy.target = target;
                Debug.Log("State변경 => Combat");
                enemy.ChangeState(EnemyStates.CombatMovement);
                break;
            }
        }
    }

    public override void Exit()
    {
        
    }   
}
