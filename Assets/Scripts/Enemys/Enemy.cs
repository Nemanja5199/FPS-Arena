using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float enemyHealth = 2f;
    void Start()
    {
        
    }

  
    void Update()
    {
       if(enemyHealth <= 0)
        {

            Debug.Log($"Enemy {name} has been defeated.");
            Destroy(gameObject);
            EnemyManager.Instance.RemoveEnemy(this);
        }
    }

    public void TakeDamage(float damage)
    {
               enemyHealth -= damage;
        Debug.Log($"Enemy {name} took {damage} damage. Remaining health: {enemyHealth}");
        if (enemyHealth <= 0)
        {
           
        }
    }
}
