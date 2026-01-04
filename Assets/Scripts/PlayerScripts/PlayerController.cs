using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Referanslar")]
    public DashSystem dashSystem;
    public JumpSystem jumpSystem;
    public Transform cam;
    public Animator animator;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float diveSpeed = 50f;
    [SerializeField] private float turnSmoothTime = 0.1f;

    [Header("Gravity Settings")]
    [SerializeField] private float extraGravity = -20f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool wasGrounded;
    private float turnSmoothVelocity;

    private float horizontal;
    private float vertical;
    private bool jumpRequest;
    private bool diveRequest;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cam == null) cam = Camera.main.transform;
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)) jumpRequest = true;
        if (Input.GetMouseButtonDown(1)) diveRequest = true;

        UpdateAnimations();
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.up * extraGravity, ForceMode.Acceleration);

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float currentY = rb.linearVelocity.y;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Vector3 targetVelocity = moveDir.normalized * moveSpeed;

            rb.linearVelocity = new Vector3(targetVelocity.x, currentY, targetVelocity.z);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, currentY, 0);
        }

        CheckGroundStatus();

        if (jumpRequest)
        {
            if (isGrounded)
            {
                if (jumpSystem != null) jumpSystem.ApplyJump(rb);
                animator.SetTrigger("Jump");
            }
            jumpRequest = false;
        }

        if (diveRequest)
        {
            if (dashSystem != null && dashSystem.CheckAndConsumeStamina())
            {
                animator.SetTrigger("Dash");
                Vector3 dashDirection = transform.forward;
                if (direction.magnitude >= 0.1f)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                    dashDirection = moveDir;
                }
                rb.linearVelocity = dashDirection * diveSpeed + Vector3.up * 3f;
            }
            diveRequest = false;
        }
    }

    void CheckGroundStatus()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 1.3f);

        if (!wasGrounded && isGrounded)
        {
            if (jumpSystem != null) jumpSystem.SpawnLandingEffect();
        }
    }

    void UpdateAnimations()
    {
        if (animator == null) return;
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        float currentSpeed = horizontalVelocity.magnitude;
        animator.SetFloat("Speed", currentSpeed, 0.1f, Time.deltaTime);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("VerticalVelocity", rb.linearVelocity.y);
    }
}