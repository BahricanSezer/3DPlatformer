using UnityEngine;

public class DeadlyObject : MonoBehaviour
{
    // Çarpýþma (Collision) anýnda çalýþýr (Katý objeler için: Duvar, Diken vb.)
    private void OnCollisionEnter(Collision collision)
    {
        CheckAndKill(collision.gameObject);
    }

    // Ýçinden geçme (Trigger) anýnda çalýþýr (Lava, Asit havuzu, Lazer vb.)
    private void OnTriggerEnter(Collider other)
    {
        CheckAndKill(other.gameObject);
    }

    // Ortak öldürme fonksiyonu
    void CheckAndKill(GameObject victim)
    {
        // Çarpan þeyin etiketi "Player" mý?
        if (victim.CompareTag("Player"))
        {
            // Oyuncunun üzerindeki Respawn sistemini bul
            RespawnSystem playerRespawn = victim.GetComponent<RespawnSystem>();

            if (playerRespawn != null)
            {
                // ÖLDÜR VE YENÝDEN BAÞLAT
                playerRespawn.Respawn();
            }
        }
    }
}