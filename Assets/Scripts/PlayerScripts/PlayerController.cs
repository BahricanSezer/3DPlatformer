using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Referanslar")]
    public DashSystem dashSystem;
    public JumpSystem jumpSystem;
    public Transform cam;
    public Animator animator; // Animator'ı buraya sürükleyeceksin

    [Header("Hareket Ayarları")]
    public float moveSpeed = 6f;
    public float diveSpeed = 50f;
    public float turnSmoothTime = 0.1f;

    [Header("Yerçekimi Ayarları")]
    public float extraGravity = -20f;

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
        // Eğer inspector'dan atamazsan otomatik bulsun
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space)) jumpRequest = true;
        if (Input.GetMouseButtonDown(1)) diveRequest = true;

        // --- ANİMASYON GÜNCELLEMELERİ (Update içinde yapılmalı) ---
        UpdateAnimations();
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

        CheckGroundStatus();

        if (jumpRequest)
        {
            if (isGrounded)
            {
                if (jumpSystem != null) jumpSystem.ApplyJump(rb);
                animator.SetTrigger("Jump"); // Zıplama animasyonu tetikle
            }
            jumpRequest = false;
        }

        if (diveRequest)
        {
            if (dashSystem != null && dashSystem.CheckAndConsumeStamina())
            {
                animator.SetTrigger("Dash"); // Dash animasyonu tetikle

                Vector3 dashDirection = transform.forward;
                if (direction.magnitude >= 0.1f)
                {
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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (!wasGrounded && isGrounded)
        {
            if (jumpSystem != null) jumpSystem.SpawnLandingEffect();
            // İniş animasyonu için trigger gerekmez, IsGrounded true olunca BlendTree'ye döner
        }
    }

    void UpdateAnimations()
    {
        if (animator == null) return;

        // 1. Yatay Hız Hesaplama (Y eksenini yoksayarak)
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        float currentSpeed = horizontalVelocity.magnitude;

        // 2. Blend Tree için Speed parametresi (0 ile 1 arası geçiş yapsın diye DampTime kullanıyoruz)
        // 0.1f, animasyonun ne kadar sürede geçiş yapacağıdır (Smoothness)
        animator.SetFloat("Speed", currentSpeed, 0.1f, Time.deltaTime);

        // 3. Havada olma durumu
        animator.SetBool("IsGrounded", isGrounded);

        // 4. Dikey Hız (Zıplamadan Düşüşe geçiş için)
        animator.SetFloat("VerticalVelocity", rb.linearVelocity.y);
    }
}