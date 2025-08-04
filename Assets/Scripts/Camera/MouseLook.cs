using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float smoothing = 1f;

    private float yaw;
    private float smoothedMousePos;
    private float currentMousePos;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        GetInput();
        ModifyInput();
        ApplyRotation();
    }

    void GetInput()
    {
        currentMousePos = Input.GetAxisRaw("Mouse X");
    }

    void ModifyInput()
    {
   
        smoothedMousePos = Mathf.Lerp(smoothedMousePos, currentMousePos, 1f / smoothing);
    }

    void ApplyRotation()
    {
        yaw += smoothedMousePos * sensitivity;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}
