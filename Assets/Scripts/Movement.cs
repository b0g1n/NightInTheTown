using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float slopeStickForce = 5f;
    public float groundCheckDistance = 1.1f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 0.1f;
    public Transform cameraHolder;
    public float maxLookAngle = 80f;

    CharacterController controller;
    Vector3 velocity;
    float xRotation;
    Vector3 groundNormal = Vector3.up;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMovement()
    {
        bool grounded = controller.isGrounded;
        UpdateGroundNormal();

        if (grounded && velocity.y < 0f)
            velocity.y = -2f;

        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 inputMove = transform.right * moveX + transform.forward * moveZ;

        // Project movement onto slope
        Vector3 slopeMove = Vector3.ProjectOnPlane(inputMove, groundNormal).normalized;

        controller.Move(slopeMove * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && grounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        if (grounded && velocity.y <= 0f)
            velocity.y = -slopeStickForce;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(Vector3.up * velocity.y * Time.deltaTime);
    }

    void UpdateGroundNormal()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, groundCheckDistance))
        {
            groundNormal = hit.normal;
        }
        else
        {
            groundNormal = Vector3.up;
        }
    }

    void HandleMouseLook()
    {
        if (cameraHolder == null)
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
