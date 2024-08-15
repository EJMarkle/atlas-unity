using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Player controller class
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpForce = 7.0f;
    public LayerMask groundLayer;
    public Transform respawnPoint;

    public float fallThreshold = -10f;
    public float maxSlopeAngle = 45f;

    private Rigidbody rb;
    public bool isGrounded { get; private set; }
    private float airTime;
    public float splatLimit;

    public string mainMenuSceneName = "MainMenu";

    // Movement states
    public bool isIdling { get; private set; }
    public bool isRunning { get; private set; }
    public bool isJumping { get; private set; }
    public bool isSplat { get; private set; }

    // inits
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        airTime = 0f;
        SetState(isIdling: true);
    }

    // controls
    void Update()
    {
        // Return to main menu with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
        
        // reset player of below fall threshold
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

        // Handle Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            SetState(isJumping: true);
        }

        // Determine movement state
        if (isGrounded)
        {
            if (moveDirection.magnitude > 0)
            {
                SetState(isRunning: true);
            }
            else if (isSplat)
            {
                SetState(isSplat: true);
            }
            else
            {
                SetState(isIdling: true);
            }
        }
        else
        {
            airTime += Time.deltaTime;

            if (airTime >= splatLimit)
            {
                SetState(isSplat: true);
            }
        }
    }

    Vector3 CalculateMoveDirection()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = cameraForward * moveZ + cameraRight * moveX;

        return moveDirection.normalized;
    }

    void OnCollisionStay(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            bool isOnValidGround = false;

            foreach (ContactPoint contact in collision.contacts)
            {
                if (Vector3.Angle(contact.normal, Vector3.up) <= maxSlopeAngle)
                {
                    isOnValidGround = true;
                    break;
                }
            }

            if (isOnValidGround)
            {
                isGrounded = true;
                airTime = 0f;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            if (isGrounded)
            {
                isGrounded = false;
            }
        }
    }

    // reset to respawn point
    void Respawn()
    {
        transform.position = respawnPoint.position;
        rb.velocity = Vector3.zero;
    }

    // loads main menu
    void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    // move state logging and setting
    private void SetState(bool isIdling = false, bool isRunning = false, bool isJumping = false, bool isSplat = false)
    {
        if (this.isIdling != isIdling)
        {
            Debug.Log("State Change: Idling = " + isIdling);
        }
        if (this.isRunning != isRunning)
        {
            Debug.Log("State Change: Running = " + isRunning);
        }
        if (this.isJumping != isJumping)
        {
            Debug.Log("State Change: Jumping = " + isJumping);
        }
        if (this.isSplat != isSplat)
        {
            Debug.Log("State Change: Splat = " + isSplat);
        }

        this.isIdling = isIdling;
        this.isRunning = isRunning;
        this.isJumping = isJumping;
        this.isSplat = isSplat;
    }
}
