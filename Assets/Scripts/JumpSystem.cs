using UnityEngine;

public class JumpSystem : MonoBehaviour
{
    public float jumpForce = 7f;
    public GameObject landingParticlePrefab;
    public Transform feetPosition;

    public void ApplyJump(Rigidbody rb)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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