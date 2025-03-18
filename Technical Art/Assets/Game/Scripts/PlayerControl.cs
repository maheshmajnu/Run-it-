using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpForce = 7f;
    public float descendSpeed = 10f;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 2f;

    [Header("References")]
    public Rigidbody rb;
    public Transform playerCamera;

    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleJumpAndDescend();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal"); // A (-1) | D (1)
        float moveZ = Input.GetAxis("Vertical");   // W (1)  | S (-1)

        float speed = (Input.GetKey(KeyCode.LeftShift) && moveZ > 0) ? sprintSpeed : walkSpeed;
        moveDirection = transform.right * moveX + transform.forward * moveZ;

        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        playerCamera.localRotation *= Quaternion.Euler(-mouseY, 0, 0);
    }

    void HandleJumpAndDescend()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.X) && !isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -descendSpeed, rb.linearVelocity.z);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
