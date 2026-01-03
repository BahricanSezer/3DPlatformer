using UnityEngine;
using System.Collections;

public class SnowballLauncher : MonoBehaviour
{
    [Header("Ayarlar")]
    public GameObject snowballPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 3f;
    public float launchForce = 10f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnSnowball();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnSnowball()
    {
        if (snowballPrefab == null || spawnPoint == null) return;

        GameObject ball = Instantiate(snowballPrefab, spawnPoint.position, spawnPoint.rotation);

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Fýrlatýcý nesnenin baktýðý yöne (Z ekseni) doðru fýrlatýr
            rb.AddForce(spawnPoint.forward * launchForce, ForceMode.Impulse);
        }
    }
}