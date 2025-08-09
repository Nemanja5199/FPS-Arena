using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    private int maxHealth;
    private int health;

    void Start()
    {
        health = maxHealth;
    }


    void Update()
    {
        if (health > 0)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                DamagePlayer(20);
                Debug.Log("Player Damaged health: " + health);
            }
        }
    }


    public void DamagePlayer(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            health = 0; 
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("Player Died!");
       
    }

}
