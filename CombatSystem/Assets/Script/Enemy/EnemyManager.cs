﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] Vector2 timeRangeBetweenAttacks = new Vector2(1, 4);

    public static EnemyManager instance { get; private set; }

    float notAttackingTimer = 2;

    private void Awake()
    {
        instance = this;
    }

    public List<EnemyController> enemiesInRange = new List<EnemyController>();

    public void AddEnemyInRange(EnemyController enemy)
    {
        if (!enemiesInRange.Contains(enemy))
            enemiesInRange.Add(enemy);

    }

    public void RemoveEnemyInRange(EnemyController enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    private void Update()
    {
        if (!enemiesInRange.Any(e => e.IsInState(EnemyStates.Attack)))
        {
            if (notAttackingTimer > 0)
            {
                notAttackingTimer -= Time.deltaTime;
            }
            if (notAttackingTimer <= 0)
            {
                var attackingEnemy = SelectEnemyForAttack();
                attackingEnemy.ChangeState(EnemyStates.Attack);
                notAttackingTimer = Random.Range(timeRangeBetweenAttacks.x, timeRangeBetweenAttacks.y);
            }
        }
    }


    EnemyController SelectEnemyForAttack()
    {
        return enemiesInRange.OrderByDescending(e => e.combatMovementTimer).FirstOrDefault();
    }
}
