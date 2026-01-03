using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;

    private Vector3 targetPos;

    void Start()
    {
        if (pointB != null)
        {
            targetPos = pointB.position;
        }
        else
        {
            targetPos = transform.position;
        }
    }

    void FixedUpdate()
    {
        if (pointA == null || pointB == null) return;

        if (Vector3.Distance(transform.position, pointA.position) < 0.1f)
        {
            targetPos = pointB.position;
        }
        else if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
        {
            targetPos = pointA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            Vector3 currentScale = collision.transform.localScale;
            collision.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}