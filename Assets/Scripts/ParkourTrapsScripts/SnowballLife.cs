using UnityEngine;

public class SnowballLife : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}