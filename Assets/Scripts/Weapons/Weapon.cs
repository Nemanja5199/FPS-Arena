using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Collider weaponCollider;

    [SerializeField]
    protected float range = 20f;
    [SerializeField]
    protected float verticalRange = 20f;
    [SerializeField]
    protected float boxWidth = 2f;

    protected virtual void Start()
    {
        weaponCollider = GetComponent<Collider>();
        if (weaponCollider == null)
        {
            Debug.LogWarning("Weapon collider not found on " + gameObject.name);
        }
        SetupCollider();
    }

   
    protected virtual void SetupCollider()
    {
        if (weaponCollider is BoxCollider box)
        {
            box.size = new Vector3(boxWidth, verticalRange, range);
            box.center = new Vector3(0, 0, range / 2);
        }
   
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.AddEnemy(enemy);
            }
            else
            {
                Debug.LogError("EnemyManager singleton instance is null!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            if (EnemyManager.Instance != null)
            {
                EnemyManager.Instance.RemoveEnemy(enemy);
            }
            else
            {
                Debug.LogError("EnemyManager singleton instance is null!");
            }
        }
    }

    public virtual void Fire()
    {
      
    }
}
