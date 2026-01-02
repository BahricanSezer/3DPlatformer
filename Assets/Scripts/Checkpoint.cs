using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("UI Ayarlarý")]
    public GameObject uiTextPrefab; // Canvas uyumlu prefab gelecek

    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated && other.CompareTag("Player"))
        {
            RespawnSystem playerRespawn = other.GetComponent<RespawnSystem>();

            if (playerRespawn != null)
            {
                playerRespawn.SetNewCheckpoint(transform.position);
                isActivated = true;

                ShowUIFeedback();
            }

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.checkpointClip);
            }
        }
    }

    void ShowUIFeedback()
    {
        if (uiTextPrefab != null)
        {
            // Sahnedeki Canvas'ý bul (Genelde ismi "Canvas" olur)
            GameObject canvas = GameObject.Find("Canvas");

            if (canvas != null)
            {
                // Prefabý Canvas'ýn çocuðu olarak oluþtur (Yoksa ekranda görünmez)
                GameObject textObj = Instantiate(uiTextPrefab, canvas.transform);

                // Konumunu ayarla (0,0 yaparak ekranýn ortasýna veya prefabýn ayarlý yerine koyar)
                // Eðer ekranýn biraz üstünde çýksýn istersen prefab ayarýndan Y deðerini artýrabilirsin.
                RectTransform rect = textObj.GetComponent<RectTransform>();
                if (rect != null)
                {
                    rect.anchoredPosition = new Vector2(0, 100); // Ekranýn ortasýndan biraz yukarýda baþlasýn
                }
            }
            else
            {
                Debug.LogError("Sahnedeki 'Canvas' bulunamadý! Lütfen Canvas ismini kontrol et.");
            }
        }
    }
}