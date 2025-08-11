using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class PlayerMovment : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] 
    private float speed = 5f;
    [SerializeField] 
    private float gravity = -10f;
    [SerializeField] 
    private float momentum = 5f;
    [SerializeField] 
    private Transform cameraTransform;
    [SerializeField] 
    private Animator camAni;

    [Header("Idle Damage Settings")]
    [SerializeField]
    private bool enableIdleDamage = true;
    [SerializeField] 
    private float idleTimeBeforeDamage = 10f; 
    [SerializeField] 
    private int idleDamage = 10; 
    [SerializeField] 
    private float damageCooldown = 2f; 

    private CharacterController characterController;
    private Vector2 inputVector;
    private float verticalVelocity;
    private bool isWalking;

    // Idle tracking
    private float idleTimer = 0f;
    private float lastDamageTime = 0f;
    private bool isIdle = false;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        MovePlayer();

        if (enableIdleDamage)
        {
            CheckIdleDamage();
        }
    }

    void GetInput()
    {
        Vector2 rawInput = Vector2.zero;
        bool isAnyKeyPressed = false;

        if (Input.GetKey(KeyCode.W)) { rawInput.y += 1; isAnyKeyPressed = true; }
        if (Input.GetKey(KeyCode.S)) { rawInput.y -= 1; isAnyKeyPressed = true; }
        if (Input.GetKey(KeyCode.A)) { rawInput.x -= 1; isAnyKeyPressed = true; }
        if (Input.GetKey(KeyCode.D)) { rawInput.x += 1; isAnyKeyPressed = true; }
        if (Input.GetKey(KeyCode.Escape)) { SceneManager.LoadScene("MainMenu"); }

        rawInput = rawInput.normalized;
        inputVector = Vector2.Lerp(inputVector, rawInput, momentum * Time.deltaTime);
        isWalking = isAnyKeyPressed;

        // Reset idle timer if moving
        if (isAnyKeyPressed)
        {
            idleTimer = 0f;
            isIdle = false;
        }

        
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
    }

    void CheckIdleDamage()
    {
        // If not walking, increment idle timer
        if (!isWalking)
        {
            idleTimer += Time.deltaTime;

            // Check if we've been idle long enough
            if (idleTimer >= idleTimeBeforeDamage)
            {
                if (!isIdle)
                {
                    isIdle = true;
                    Debug.LogWarning("Player is idle! Taking damage!");
                }

                // Apply damage at intervals
                if (Time.time - lastDamageTime >= damageCooldown)
                {
                    ApplyIdleDamage();
                    lastDamageTime = Time.time;
                }
            }
        }
    }

    void ApplyIdleDamage()
    {
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.MissPenelty(idleDamage);
            Debug.Log($"Player took {idleDamage} damage for being idle!");

           //TODO ADD indication
        }
        else
        {
            Debug.LogError("PlayerHealth component not found!");
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsIdle()
    {
        return isIdle;
    }

    public float GetIdleTime()
    {
        return idleTimer;
    }
}