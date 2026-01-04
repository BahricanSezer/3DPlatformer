using UnityEngine;

public class BumperObstacle : MonoBehaviour
{
    [Header("Force Settings")]
    [SerializeField] private float pushForce = 15f; 

    [Header("SFX")]
    [SerializeField] private AudioClip bumpSound; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                Vector3 pushDirection = (collision.transform.position - transform.position).normalized;

                pushDirection.y = 0;
                pushDirection.Normalize();

                playerRb.linearVelocity = Vector3.zero;

                playerRb.AddForce(pushDirection * pushForce, ForceMode.Impulse);

                if (AudioManager.instance != null && bumpSound != null)
                {
                    AudioManager.instance.PlaySFXRandomPitch(bumpSound);
                }
            }
        }
    }
}