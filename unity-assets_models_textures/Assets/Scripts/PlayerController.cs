using UnityEngine;


/// PlayerController class
public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpForce = 7.0f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    // Get Rigidbody component
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Update movement vector from input
        Vector3 move = new Vector3(moveX, 0, moveZ).normalized * speed;

        // Move player
        rb.MovePosition(transform.position + move * Time.deltaTime);

        // Jump but no midair jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Check if the collision object is on Ground layer
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            if (!isGrounded)
            {
                isGrounded = true;
                // Debug.Log("Player is grounded");
            }
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
}
