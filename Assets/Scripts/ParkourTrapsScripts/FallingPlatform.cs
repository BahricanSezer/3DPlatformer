using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;
    public float destroyDelay = 2f;

    private Rigidbody rb;
    private bool isTriggered = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        // Oyun ba��nda nerede duruyorsa oray� haf�zaya at
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isTriggered && collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
            StartCoroutine(FallRoutine());
        }
    }

    IEnumerator FallRoutine()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false; // D���� ba�las�n
        yield return new WaitForSeconds(destroyDelay);

        // ARTIK YOK ETM�YORUZ, G�ZL�YORUZ
        gameObject.SetActive(false);
    }

    // Bu komut gelince platform eski haline d�necek
    public void ResetPlatform()
    {
        // 1. D��me i�lemi s�r�yorsa durdur
        StopAllCoroutines();

        // 2. Platformu tekrar g�r�n�r yap
        gameObject.SetActive(true);

        // 3. Ba�lang��taki yerine ve duru�una ���nla
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // 4. Fizik ayarlar�n� s�f�rla (tekrar havada kilitli kals�n)
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // 5. Tetiklenmeyi s�f�rla
        isTriggered = false;
    }
}