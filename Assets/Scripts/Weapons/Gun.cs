using UnityEngine;

public class Gun : Weapon
{

    [SerializeField]
    private float gunRange = 20f;
    [SerializeField]
    private float gunVerticalRange = 20f;
    [SerializeField]
    private float firerate = 0.5f;
    [SerializeField]
    private float damage = 2f;

    private float nextFireTime = 0f;
    private BoxCollider gunCollider;
    protected override void Start()
    {
        range = gunRange;
        verticalRange = gunVerticalRange;

        base.Start();
    }


    protected override void Update()
    {

        range = gunRange;
        verticalRange = gunVerticalRange;
        base.Update();

        if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
        {
            Fire();
        }
    }

    public override void Fire()
    {
        foreach(var enemy in EnemyManager.Instance.Enemies)
        {
            enemy.TakeDamage(damage);
        }
        
        nextFireTime = Time.time + firerate;
    }

   
}
