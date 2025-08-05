using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float gravity = -10f;
    [SerializeField]
    private float momentum = 5f;


    [SerializeField] private Transform cameraTransform;

    private CharacterController characterController;
    private Vector2 inputVector;
    private float verticalVelocity;
    private bool isWalking;

    [SerializeField]
    private Animator camAni;




    void Awake()
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
    
        Vector2 rawInput = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) rawInput.y += 1;
        if (Input.GetKey(KeyCode.S)) rawInput.y -= 1;
        if (Input.GetKey(KeyCode.A)) rawInput.x -= 1;
        if (Input.GetKey(KeyCode.D)) rawInput.x += 1;

        rawInput = rawInput.normalized;

       
        inputVector = Vector2.Lerp(inputVector, rawInput, momentum * Time.deltaTime);
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


        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

      
        Vector3 horizontalMove = forward * inputVector.y + right * inputVector.x;

       
        Vector3 moveDirection = horizontalMove * speed;
        moveDirection.y = verticalVelocity;

       
        characterController.Move(moveDirection * Time.deltaTime);

      
        isWalking = inputVector != Vector2.zero;
    }

    public bool IsWalking()
    {
        return isWalking;
    }

}