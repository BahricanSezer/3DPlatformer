using UnityEngine;
using System.Collections;

public class SnowballLauncher : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject snowballPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float launchForce = 10f;

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
            // ÖNEMLÝ:Fýrlatýcý nesnenin baktýðý yöne doðru fýrlatýr
            rb.AddForce(spawnPoint.forward * launchForce, ForceMode.Impulse);
        }
    }
}