using UnityEngine;

public class DeadlyObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        CheckAndKill(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckAndKill(other.gameObject);
    }

    void CheckAndKill(GameObject victim)
    {
        if (victim.CompareTag("Player"))
        {
            RespawnSystem playerRespawn = victim.GetComponent<RespawnSystem>();

            if (playerRespawn != null)
            {
                playerRespawn.Respawn();
            }
        }
    }
}