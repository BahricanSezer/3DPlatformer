using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 0.5f;
    [SerializeField] private float destroyDelay = 2f;

    private Rigidbody rb;
    private bool isTriggered = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

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

        yield return new WaitForSeconds(destroyDelay);

        gameObject.SetActive(false);
    }

    public void ResetPlatform()
    {
        StopAllCoroutines();

        gameObject.SetActive(true);

        transform.position = initialPosition;
        transform.rotation = initialRotation;

        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        isTriggered = false;
    }
}