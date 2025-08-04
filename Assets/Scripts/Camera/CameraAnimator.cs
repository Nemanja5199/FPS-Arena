using UnityEngine;

public class CameraAnimator : MonoBehaviour
{
   
    private const string IS_WALKING = "isWalking";


    [SerializeField]
    private PlayerMovment playerMovement;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }
    }
    void Update()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALKING, playerMovement.IsWalking());
    }
}
