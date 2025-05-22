using UnityEngine;
using Random = UnityEngine.Random;

public enum AICombatStates
{
    Idle,
    Chase,
    Circling
}

public class CombatMovementState : State<EnemyController>
{
    [SerializeField] float circlingSpeed = 2f;
    [SerializeField] float distanceToStand = 3f;
    [SerializeField] float adjustDistanceThreshold = 1f;
    [SerializeField] Vector2 idleTimeRange = new Vector2(2, 5);
    [SerializeField] Vector2 circlingTimeRange = new Vector2(3, 6);


    float timer;
    int circlingDir = 1; // 1: 시계방향, -1: 반시계방향
    AICombatStates state;
    EnemyController enemy;

    public override void Enter(EnemyController owner)
    {
        enemy = owner;
        enemy.NavAgent.stoppingDistance = distanceToStand; // NavMeshAgent의 정지 거리 설정
        enemy.combatMovementTimer = 0f; //타이머 초기화
    }
    public override void Execute()
    {
        if (Vector3.Distance(enemy.target.transform.position, enemy.transform.position) > distanceToStand + adjustDistanceThreshold) // 타겟과의 거리 체크
        {
            StartChase();
        }

        if (state == AICombatStates.Idle)
        {
            if (timer <= 0)
            {
                if (Random.Range(0, 2) == 0)
                {
                    StartIdle();
                }
                else
                {
                    StartCircling();
                }
            }
        }
        else if (state == AICombatStates.Chase)
        {
            if (Vector3.Distance(enemy.target.transform.position, enemy.transform.position) <= distanceToStand + 0.03f)
            {
                StartIdle();
                return;
            }
            enemy.NavAgent.SetDestination(enemy.target.transform.position); // 타겟의 위치로 NavMeshAgent의 목적지 설정
        }
        else if (state == AICombatStates.Circling)
        {
            if (timer <= 0)
            {
                StartIdle();
                return;
            }

            Debug.Log(circlingSpeed * circlingDir);
            //// 타겟의 위치를 기준으로 회전
            //transform.RotateAround(enemy.target.transform.position, Vector3.up, circlingSpeed * circlingDir * Time.deltaTime);

            var vecToTarget = enemy.transform.position - enemy.target.transform.position;
            //Quaternion * Vector3 = Vector3
            var rotatePos = Quaternion.Euler(0, circlingSpeed * circlingDir * Time.deltaTime, 0) * vecToTarget;

            enemy.NavAgent.Move(rotatePos - vecToTarget);
            enemy.transform.rotation = Quaternion.LookRotation(-rotatePos); // 타겟을 바라보도록 회전

        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        enemy.combatMovementTimer += Time.deltaTime;

    }

    private void StartCircling()
    {
        state = AICombatStates.Circling;

        enemy.NavAgent.ResetPath(); 

        timer = Random.Range(circlingTimeRange.x, circlingTimeRange.y);

        circlingDir = Random.Range(0, 2) == 0 ? 1 : -1;

        //enemy.anim.SetBool("circling", true);
        //enemy.anim.SetFloat("circlingDir", circlingDir);
    }

    private void StartIdle()
    {
        state = AICombatStates.Idle;
        timer = Random.Range(idleTimeRange.x, idleTimeRange.y);
        enemy.anim.SetBool("combatMode", true);
        //enemy.anim.SetBool("circling", false);
    }

    void StartChase()
    {
        state = AICombatStates.Chase;
        enemy.anim.SetBool("combatMode", false);
        //enemy.anim.SetBool("circling", false);
    }

    public override void Exit()
    {
        enemy.combatMovementTimer = 0;
    }
}
