using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform orientation;
    public Transform camerPos;
    public Rigidbody rb;
    private CapsuleCollider capsuleCollider;

    [Header("Movement")]
    public float movementSpeed = 7f;
    public float groundDrag = 5f;

    private Vector3 movementDirection;
    private Vector3 movementInputDirection;

    [Header("Jump")]
    public float jumpForce = 5f;
    public float jumpCoolDown;
    public float airDrag = 1f;
    public LayerMask groundMask;
    private bool canJump = true;
    private float playerHeight = 2f;
    private bool onGround;

    [Header("Sprint")]
    public float sprintMultiplier = 2f;

    [Header("Crouch")]
    public float crouchFactor = 0.5f;
    private bool canGetUp = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        capsuleCollider =  GetComponentInChildren<CapsuleCollider>();
    }

    void Update()
    {
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask);
        canGetUp = !Physics.Raycast(transform.position, Vector3.up, playerHeight * 0.5f + 0.2f, groundMask);
        rb.linearDamping = airDrag;
        if (onGround)
        {
            rb.linearDamping = groundDrag;
        }
        SpeedControl();
    }

    public void Fire()
    {
        Debug.Log("Fire at will");
    }


    public void getMoveDirection(Vector2 movementInput)
    {
        movementInputDirection = movementInput;
    }

    void SpeedControl()
    {
        Vector3 velocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (rb.linearVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = velocity.normalized * movementSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    void FixedUpdate()
    {
        movementDirection = orientation.forward * movementInputDirection.y + orientation.right * movementInputDirection.x;
        rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
    }

    public void Jump()
    {
        if (canJump && onGround)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    private void ResetJump()
    {
        canJump = true;
    }

    public void Sprint()
    {
        movementSpeed *= sprintMultiplier;
    }
    public void StopSprint()
    {
        movementSpeed /= sprintMultiplier;
    }

    public void Crouch()
    {
        camerPos.position = new Vector3(camerPos.position.x, camerPos.position.y - playerHeight * crouchFactor, camerPos.position.z);
        capsuleCollider.height *= crouchFactor;
        capsuleCollider.center = new Vector3(capsuleCollider.center.x, playerHeight * 0.5f - crouchFactor, capsuleCollider.center.z);
    }
    public void StandUp()
    {
        if (canGetUp)
        {
            camerPos.position = new Vector3(camerPos.position.x, camerPos.position.y + playerHeight * crouchFactor, camerPos.position.z);
            capsuleCollider.center = new Vector3(capsuleCollider.center.x, 0, capsuleCollider.center.z);
            capsuleCollider.height /= crouchFactor;
        }
    }
}
