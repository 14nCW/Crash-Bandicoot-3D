using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Camera")]
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;
    [SerializeField] private Transform cam;

    [Header("Movement")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;

    [Header("Misc")]
    [SerializeField] private Transform groundDetector;
    [SerializeField] private LayerMask groundLayer;

    public static bool isMoving;
    Vector3 velocity;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        bool jump = Input.GetKey(KeyCode.Space);

        bool isGrounded = Physics.Raycast(groundDetector.position, Vector3.down, 0.1f, groundLayer);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            isMoving = true;
        } else {
            isMoving = false;
        }

        if (jump && isGrounded) {
            Jump(jumpForce);
            isMoving = true;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump(float force) {
        velocity.y = Mathf.Sqrt(force * -2f * gravity);
    }
}
