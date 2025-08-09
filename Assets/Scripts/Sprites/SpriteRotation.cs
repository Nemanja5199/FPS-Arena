using UnityEngine;

public class SpriteRotation : MonoBehaviour
{


    [SerializeField]
    private Transform traget;
    void Start()
    {
        traget = FindFirstObjectByType<PlayerMovment>().transform;
    }

   
    void Update()
    {
        transform.LookAt(traget);
    }
}
