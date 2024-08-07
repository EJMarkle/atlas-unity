using UnityEngine;
using UnityEngine.SceneManagement;


/// PlayerController class
public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpForce = 7.0f;
    public LayerMask groundLayer;
    public Transform respawnPoint;

    public float fallThreshold = -10f;
    public float maxSlopeAngle = 45f;

    private Rigidbody rb;
    private bool isGrounded;
    public string mainMenuSceneName = "MainMenu";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }

        // Reset player when if fallen off platforms
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }

        // Calculate movement direction relative to camera
        Vector3 moveDirection = CalculateMoveDirection();

        // Move player
        rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);

        // Rotate player to face movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(newRotation);
        }

        // Jump but no midair jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    /// move realtive to camera
    Vector3 CalculateMoveDirection()
    {
        // get camera's forward and right vectors without vertical component
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0;

        // normalize
        cameraForward.Normalize();
        cameraRight.Normalize();

        // calculate movement direction based on input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = cameraForward * moveZ + cameraRight * moveX;

        // eturn normalized movement direction
        return moveDirection.normalized;
    }

    void OnCollisionStay(Collision collision)
    {
        // Check if the collision object is on Ground layer
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            bool isOnValidGround = false;

            foreach (ContactPoint contact in collision.contacts)
            {
                // Check if the angle between the contact normal and up direction is within acceptable range
                if (Vector3.Angle(contact.normal, Vector3.up) <= maxSlopeAngle)
                {
                    isOnValidGround = true;
                    break;
                }
            }

            isGrounded = isOnValidGround;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if not colliding with objects on Ground layer
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            if (isGrounded)
            {
                isGrounded = false;
                // Debug.Log("Player is not grounded");
            }
        }
    }

    // resets player to RespawnPoint location
    void Respawn()
    {
        transform.position = respawnPoint.position;
        rb.velocity = Vector3.zero;
    }
    void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
