using UnityEngine;
using static ItemPickUp;

public class ItemPickUp : MonoBehaviour
{

    public enum ItemType
    {
        Health,
        Armor,
        Ammo
    }

    [SerializeField]
    private int ammount;


    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField]
    private ItemType itemType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){

            bool pickedUp = false;
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (itemType == ItemType.Health )
            {
               
                if(ph.CurrentHealth() < ph.GetMaxHealth())
                {
                    ph.HealPlayer(ammount);
                    pickedUp = true;
                }
            } else if (itemType == ItemType.Armor)
            {

                if (ph.CurrentArrmor() < ph.GetMaxArmor())
                {
                    ph.RestoreArmor(ammount);
                    pickedUp = true;
                }
            }
            else if(itemType == ItemType.Ammo)
            {
                Gun gun = other.GetComponentInChildren<Gun>();

                if (gun != null)
                {
                    if(gun.GetAmmo() < gun.GetMaxAmmo()) 
                        {
                            gun.AddAmmo(ammount);
                            pickedUp = true;
                        }
                }
                else
                {
                    Debug.Log("Gun Not Found");
                }
            }
            else
            {
                Debug.LogError("Item type Dosent Exist");
            }

            if (pickedUp)
            {
                Destroy(gameObject);
            }
        }
    }


    public ItemType GetItemType()
    {
        return itemType;
    }
}
