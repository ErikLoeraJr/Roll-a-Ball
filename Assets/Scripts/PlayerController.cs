using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb; 

    // Variable to keep track of collected "PickUp" objects.
    private int count;

    // Movement input values
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 10f;

    // Jump force
    public float jumpForce = 7f;

    // Check if player is on the ground
    private bool isGrounded;

    // Reference to camera transform
    public Transform cameraTransform;

    // UI text component to display count
    public TextMeshProUGUI countText;

    // UI object to display win/lose text
    public GameObject winTextObject;

    // Start is called before the first frame update.
    void Start()
    {
        // Get Rigidbody
        rb = GetComponent<Rigidbody>();

        // Initialize count
        count = 0;

        // Player starts grounded
        isGrounded = true;

        // Update UI
        SetCountText();

        // Hide win text
        winTextObject.SetActive(false);
    }

    // Input movement (WASD / joystick)
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Jump input
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // Physics movement
    private void FixedUpdate() 
    {
        // Camera directions
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Prevent vertical movement influence
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Movement relative to camera
        Vector3 movement = forward * movementY + right * movementX;

        rb.AddForce(movement * speed);
    }

    // Trigger pickups
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);

            count++;

            SetCountText();
        }
    }

    // Update UI text
    void SetCountText() 
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12)
        {
            winTextObject.SetActive(true);

            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    // Collision detection
    private void OnCollisionEnter(Collision collision)
    {
        // Ground detection
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // Enemy detection
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);

            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }
}