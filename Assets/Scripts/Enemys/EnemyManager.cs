using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager Instance;
    [SerializeField]private List<Enemy> enemies = new List<Enemy>();

    public List<Enemy> Enemies => enemies;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void AddEnemy(Enemy enemy)
    {
        if (enemy == null)
        {
            Debug.LogError("Attempted to add a null enemy.");
            return;
        }

        enemies.Add(enemy);
       
    }

    public void RemoveEnemy(Enemy enemy)
    {
        if (enemy == null)
        {
            Debug.LogError("Attempted to remove a null enemy.");
            return;
        }

        enemies.Remove(enemy);
       
    }
}
