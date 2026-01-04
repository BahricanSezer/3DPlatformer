using UnityEngine;

public class JumpSystem : MonoBehaviour
{
    public float jumpForce = 7f;
    [SerializeField] private GameObject landingParticlePrefab;
    [SerializeField] private Transform feetPosition;

    public void ApplyJump(Rigidbody rb)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFXRandomPitch(AudioManager.instance.jumpClip);
        }
    }

    public void SpawnLandingEffect()
    {
        if (landingParticlePrefab != null)
        {
            GameObject dust = Instantiate(landingParticlePrefab, feetPosition.position, Quaternion.identity);
            dust.transform.rotation = Quaternion.Euler(-90, 0, 0);
            Destroy(dust, 1.5f);
        }
    }
}