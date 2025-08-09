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



    private float nextFireTime = 0f;
    [SerializeField]
    private float gunShootRadius = 20f;
    [SerializeField]
    private int maxAmmo = 100;
    [SerializeField]
    private int ammo = 20;



    [SerializeField]
    private LayerMask raycastLayerMask;

    [SerializeField]
    private LayerMask enemyLayerMask;
    





    protected override void Start()
    {
        range = gunRange;
        verticalRange = gunVerticalRange;
        base.Start();
    }

    protected  void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
        {
            Fire();
        }

    }

    public override void Fire()
    {
      
        if (ammo <= 0) {

            Debug.Log("I need more buletsssss");
            return;
               
        }

        ammo = Mathf.Max(0, ammo - 1);

        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunShootRadius, enemyLayerMask);
        foreach(var enemyColider in enemyColliders)
        {
            enemyColider.GetComponent<EnemyAi>().isAggro = true;
        }

        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();

        foreach (var enemy in EnemyManager.Instance.Enemies)
        {
            var dir = (enemy.transform.position - transform.position).normalized;

            if (Physics.Raycast(transform.position, dir, out RaycastHit hit, gunRange * 1.5f, raycastLayerMask))
            {
                if (hit.transform.root == enemy.transform)
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




    public int GetMaxAmmo() => maxAmmo;
    public int GetAmmo() => ammo;

    public void AddAmmo(int amount)
    {
        int oldAmmo = ammo;
        ammo = Mathf.Min(maxAmmo, ammo + amount);
        int actualAmmo = ammo - oldAmmo;

        Debug.Log($"Added {actualAmmo} ammo - Now: {ammo}/{maxAmmo}");
    }

}