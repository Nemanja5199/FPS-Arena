using UnityEngine;

public class ItemDespawn : MonoBehaviour
{
    [Header("Despawn Settings")]
    [SerializeField] private float despawnTime = 10f; 
    [SerializeField] private bool startOnAwake = true; 

    private float timer = 0f;

    void Start()
    {
        if (startOnAwake)
        {
           
            Destroy(gameObject, despawnTime);
        }
    }

   
    public void StartDespawnTimer(float customTime = -1)
    {
        float timeToUse = customTime > 0 ? customTime : despawnTime;
        Destroy(gameObject, timeToUse);
    }

   
}