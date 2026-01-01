using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Referanslar")]
    public DashSystem dashSystem; // Yeni scripti buraya bağlayacağız
    public Transform cam;

    [Header("Hareket Ayarları")]
    public float moveSpeed = 6f;
    public float jumpForce = 7f;
    public float diveSpeed = 50f;
    public float turnSmoothTime = 0.1f;

    [Header("Yerçekimi Ayarları")]
    public float extraGravity = -20f;

    private Rigidbody rb;
    private bool isGrounded;
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
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)) jumpRequest = true;

        // Sadece tuşa basıldığını kaydediyoruz, stamina kontrolünü fizikte yapacağız
        if (Input.GetMouseButtonDown(1)) diveRequest = true;
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.up * extraGravity, ForceMode.Acceleration);

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveDir = Vector3.zero;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.linearVelocity = new Vector3(moveDir.x * moveSpeed, rb.linearVelocity.y, moveDir.z * moveSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (jumpRequest)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            jumpRequest = false;
        }

        if (diveRequest)
        {
            // Önce DashSystem'e soruyoruz: Stamina var mı?
            if (dashSystem != null && dashSystem.CheckAndConsumeStamina())
            {
                Vector3 dashDirection = transform.forward;

                if (direction.magnitude >= 0.1f)
                {
                    dashDirection = moveDir;
                }

                rb.linearVelocity = dashDirection * diveSpeed + Vector3.up * 3f;
            }

            // Stamina olsa da olmasa da isteği sıfırlıyoruz ki takılı kalmasın
            diveRequest = false;
        }
    }
}