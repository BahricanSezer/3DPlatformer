using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float bounceForce = 20f;
    [SerializeField] private AudioSource audioSource;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, bounceForce, rb.linearVelocity.z);

                if (audioSource != null)
                    audioSource.Play();

                PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
                if (pc != null && pc.animator != null)
                {
                    pc.animator.SetTrigger("Jump");
                }
            }
        }
    }
}