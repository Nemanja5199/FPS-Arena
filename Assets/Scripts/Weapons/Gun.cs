using UnityEngine;

public class Gun : Weapon
{

    [SerializeField]
    private float gunRange = 20f;
    [SerializeField]
    private float gunVerticalRange = 20f;


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
    }
      

        //private void OnTriggerEnter(Collider other)
        //{

        //}


        //private void OnTriggerExit(Collider other)
        //{

        //}
    }
