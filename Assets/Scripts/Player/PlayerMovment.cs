using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float gravity = -10f;

    private CharacterController characterController;
    private Vector2 inputVector;
    private float verticalVelocity;
    private bool isWalking;

    [SerializeField]
    private Animator camAni;

  

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        MovePlayer();
   
       
    }

    void GetInput()
    {
        inputVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            inputVector.y = 1;
        if (Input.GetKey(KeyCode.S))
            inputVector.y = -1;
        if (Input.GetKey(KeyCode.A))
            inputVector.x = -1;
        if (Input.GetKey(KeyCode.D))
            inputVector.x = 1;

        inputVector = inputVector.normalized;
    }

    void MovePlayer()
    {
        
        if (characterController.isGrounded)
        {
            verticalVelocity = -2f; 
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        
        Vector3 moveDirection = new Vector3(inputVector.x, verticalVelocity, inputVector.y);
        isWalking = inputVector != Vector2.zero;
        characterController.Move(moveDirection * speed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

}