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
    private float bigDamage = 2f;
    [SerializeField]
    private float smallDamage = 1f;
    [SerializeField]
    private LayerMask raycastLayerMask;

    private float nextFireTime = 0f;

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
        foreach (var enemy in EnemyManager.Instance.Enemies)
        {
            var dir = (enemy.transform.position - transform.position).normalized;

            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, gunRange * 1.5f, raycastLayerMask))
            {
                if (hit.transform == enemy.transform)
                {
                    float dist = Vector3.Distance(transform.position, enemy.transform.position);

                    if (dist > range * 0.5f)
                    {
                        enemy.TakeDamage(smallDamage);
                    }
                    else
                    {
                        enemy.TakeDamage(bigDamage);
                    }
                }
            }
        }

        nextFireTime = Time.time + firerate;
    }
}