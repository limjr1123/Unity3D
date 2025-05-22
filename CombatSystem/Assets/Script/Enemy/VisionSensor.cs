using UnityEngine;

public class VisionSensor : MonoBehaviour
{
    [SerializeField] EnemyController enemy;
    private void OnTriggerEnter(Collider other)
    {
        var fighter = other.GetComponent<MeleeFighter>();

        if (fighter != null)
        {
            enemy.targetsInRange.Add(fighter);
            EnemyManager.instance.AddEnemyInRange(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var fighter = other.GetComponent<MeleeFighter>();

        if (fighter != null)
        {
            enemy.targetsInRange.Remove(fighter);
            EnemyManager.instance.RemoveEnemyInRange(enemy);
        }
    }
}
