using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] 
    private Image healthFillImage;
    
    [SerializeField]
    private int maxHealth = 100;
    private int health;

    [Header("Armor Settings")]
    [SerializeField]
    private int maxArmor = 50;
    private int armor;
    [SerializeField]
    private Image ArmorFillImage;

    [Header("Damage Reduction")]
    [SerializeField]
    [Range(0f, 0.9f)]
    private float armorReductionPercent = 0.5f; 


    [Header("Debug")]
    [SerializeField]
    private bool enableDebugLogs = true;

    void Start()
    {


        health = maxHealth;
        armor = maxArmor;


        UpdateHealthBar();
        UpdateArrmorBar();

        if (enableDebugLogs)
        {
            Debug.Log($"Player initialized - Health: {health}/{maxHealth}, Armor: {armor}/{maxArmor}");
            Debug.Log($"Armor provides {armorReductionPercent * 100}% damage reduction");
        }
    }

    void Update()
    {
        if (health > 0)
        {
            // Test damage (you can remove these in final build)
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                DamagePlayer(20);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DamagePlayer(5);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                DamagePlayer(15);
            }
        }
    }

    public void DamagePlayer(int damage)
    {
        if (enableDebugLogs)
        {
            Debug.Log($"=== TAKING {damage} DAMAGE ===");
        }

        if (armor > 0)
        {

            armor -= (int)(damage * armorReductionPercent);
            health -= (int)(damage * (1f - armorReductionPercent));
            UpdateHealthBar();
            UpdateArrmorBar();


            if (enableDebugLogs)
            {
                Debug.Log("Current Health : " + health +"Current Arrmor : " + armor);
                if (armor == 0)
                {
                    Debug.Log("  *** ARMOR DESTROYED! ***");
                }
            }
        }
        else
        {

            health = Mathf.Max(health - damage, 0);


            UpdateHealthBar();
            UpdateArrmorBar();

            if (enableDebugLogs)
            {
                Debug.Log($"  No armor protection - {damage} damage to health");
                Debug.Log($"  Health: {health}/{maxHealth}");
            }
        }

     
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        if (enableDebugLogs)
        {
            Debug.Log("=== PLAYER DIED ===");
        }

        // Add your death logic here:
        // - Play death animation
        // - Show game over screen
        // - Respawn player
        // - etc.
    }


    public int GetHealth() => health;
    public int GetMaxHealth() => maxHealth;
    public int GetArmor() => armor;
    public int GetMaxArmor() => maxArmor;

    public float GetHealthPercent() => (float)health / maxHealth;
    public float GetArmorPercent() => (float)armor / maxArmor;

    public bool IsAlive() => health > 0;
    public bool HasArmor() => armor > 0;


    public void HealPlayer(int amount)
    {
        int oldHealth = health;
        health = Mathf.Min(maxHealth, health + amount);
        int actualHealing = health - oldHealth;
        UpdateHealthBar();

        if (enableDebugLogs && actualHealing > 0)
        {
            Debug.Log($"Healed {actualHealing} health - Now: {health}/{maxHealth}");
        }
    }

    public void RestoreArmor(int amount)
    {
        int oldArmor = armor;
        armor = Mathf.Min(maxArmor, armor + amount);
        int actualRestore = armor - oldArmor;
        UpdateArrmorBar();

        if (enableDebugLogs && actualRestore > 0)
        {
            Debug.Log($"Restored {actualRestore} armor - Now: {armor}/{maxArmor}");
        }
    }


    public int CurrentHealth()
    {
        return health;
    }

    public int CurrentArrmor()
    {
        return armor; 
    }


    private void UpdateHealthBar()
    {
        healthFillImage.fillAmount = (float)health / maxHealth;
    }

    private void UpdateArrmorBar()
    {
        ArmorFillImage.fillAmount = (float)armor / maxArmor;
    }
}
    
