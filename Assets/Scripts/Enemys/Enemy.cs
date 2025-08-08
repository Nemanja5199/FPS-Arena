using JetBrains.Annotations;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float enemyHealth = 2f;
    [SerializeField]
    private GameObject gunhitEffect;
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
        if (gunhitEffect != null)
        {
            Instantiate(gunhitEffect, transform.position, Quaternion.identity);
        }
        enemyHealth -= damage;
        Debug.Log($"Enemy {name} took {damage} damage. Remaining health: {enemyHealth}");
       

    }
}
