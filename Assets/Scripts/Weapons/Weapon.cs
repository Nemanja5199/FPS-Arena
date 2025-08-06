using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected BoxCollider weaponCollider;

    [SerializeField]
    protected float range;
    [SerializeField]
    protected float verticalRange;


    protected virtual void Start()
    {
        weaponCollider = GetComponent<BoxCollider>();
        UpdateCollider();
    }

    protected virtual void Update()
    {
        UpdateCollider();
    }

    protected void UpdateCollider()
    {
        if (weaponCollider != null)
        {
            weaponCollider.size = new Vector3(1, verticalRange, range);
            weaponCollider.center = new Vector3(0, 0, range / 2);
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


    public virtual void Fire() { }
    
        
}
