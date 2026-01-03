using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    [Header("Start Settings")]
    public Transform startPoint; 
    public float fallThreshold = -10f; 

    private Vector3 currentRespawnPosition;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        if (startPoint != null)
        {
            currentRespawnPosition = startPoint.position;
        }
        else
        {
            currentRespawnPosition = transform.position;
        }
    }

    void Update()
    {        
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    public void SetNewCheckpoint(Vector3 newPos)
    {
        currentRespawnPosition = newPos;
        Debug.Log("Checkpoint Al�nd�!"); 
    }

    public void Respawn()
    {
        transform.position = currentRespawnPosition;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.deathClip);
        }

        // Sahnedeki tüm FallingPlatform scriptlerini bul (Gizli olanlar dahil "true")
        FallingPlatform[] platforms = FindObjectsOfType<FallingPlatform>(true);

        // Hepsine tek tek "Kendini sıfırla" emri ver
        foreach (FallingPlatform platform in platforms)
        {
            platform.ResetPlatform();
        }
    }
}