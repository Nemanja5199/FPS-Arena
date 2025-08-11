using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    [Header("Drop Settings")]
    [SerializeField]
    private List<GameObject> dropItems = new List<GameObject>();
    [Range(0f, 1f)]
    [SerializeField]
    private float dropChance = 0.5f;

    [Header("Drop Configuration")]
    [SerializeField]
    private Vector3 dropOffset = new Vector3(0, 0.5f, 0); 
    [SerializeField]
    private float dropForce = 5f;
    [SerializeField]
    private bool addDropEffect = true;


    [Header("Despawn Settings")]
    [SerializeField]
    private float despawnTime = 10f; 
    [SerializeField]
    private float warningTime = 3f;
    [SerializeField]
    private bool enableDespawn = true;

    [Header("Debug")]
    [SerializeField]
    private bool debugMode = true;

    public void DropLoot()
    {
        if (debugMode)
        {
            Debug.Log($"[EnemyDrops] DropLoot called on {gameObject.name}");
            Debug.Log($"[EnemyDrops] Drop items count: {dropItems.Count}");
            Debug.Log($"[EnemyDrops] Drop chance: {dropChance}");
        }

     
        if (dropItems == null || dropItems.Count == 0)
        {
            if (debugMode)
                Debug.LogWarning($"[EnemyDrops] No items in drop list for {gameObject.name}!");
            return;
        }

       
        dropItems.RemoveAll(item => item == null);

        if (dropItems.Count == 0)
        {
            if (debugMode)
                Debug.LogWarning($"[EnemyDrops] All items in drop list were null for {gameObject.name}!");
            return;
        }

   
        float randomRoll = Random.value;
        if (debugMode)
            Debug.Log($"[EnemyDrops] Random roll: {randomRoll} (needs to be <= {dropChance} to drop)");

        if (randomRoll > dropChance)
        {
            if (debugMode)
                Debug.Log($"[EnemyDrops] Drop chance failed. No loot dropped.");
            return;
        }


        int randomIndex = Random.Range(0, dropItems.Count);
        GameObject chosenItem = dropItems[randomIndex];

        if (chosenItem == null)
        {
            if (debugMode)
                Debug.LogError($"[EnemyDrops] Chosen item at index {randomIndex} is null!");
            return;
        }

        if (debugMode)
            Debug.Log($"[EnemyDrops] Dropping item: {chosenItem.name}");

  
        Vector3 dropPosition = transform.position + dropOffset;
        GameObject droppedItem = Instantiate(chosenItem, dropPosition, Quaternion.identity);

        if (droppedItem == null)
        {
            if (debugMode)
                Debug.LogError($"[EnemyDrops] Failed to instantiate {chosenItem.name}!");
            return;
        }

        if (debugMode)
            Debug.Log($"[EnemyDrops] Successfully spawned {droppedItem.name} at {dropPosition}");

     
        if (addDropEffect)
        {
            AddDropEffect(droppedItem);
        }
    }

    private void AddDropEffect(GameObject droppedItem)
    {
   
        Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomDirection = new Vector3(
                Random.Range(-1f, 1f),
                1f,
                Random.Range(-1f, 1f)
            ).normalized;

            rb.AddForce(randomDirection * dropForce, ForceMode.Impulse);

            if (debugMode)
                Debug.Log($"[EnemyDrops] Added pop effect to dropped item");
        }

  
        Rigidbody2D rb2d = droppedItem.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            Vector2 randomDirection = new Vector2(
                Random.Range(-1f, 1f),
                1f
            ).normalized;

            rb2d.AddForce(randomDirection * dropForce, ForceMode2D.Impulse);

            if (debugMode)
                Debug.Log($"[EnemyDrops] Added 2D pop effect to dropped item");
        }
    }

    
    [ContextMenu("Test Drop")]
    private void TestDrop()
    {
        Debug.Log("[EnemyDrops] Testing drop system...");
        DropLoot();
    }

   
    private void OnValidate()
    {
        if (dropItems != null && dropItems.Count > 0)
        {
            for (int i = 0; i < dropItems.Count; i++)
            {
                if (dropItems[i] == null)
                {
                    Debug.LogWarning($"[EnemyDrops] Drop item at index {i} is null on {gameObject.name}");
                }
            }
        }
    }
}