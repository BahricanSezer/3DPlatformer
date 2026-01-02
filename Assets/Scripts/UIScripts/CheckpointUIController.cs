using UnityEngine;
using TMPro;

public class CheckpointUIController : MonoBehaviour
{
    [Header("Ayarlar")]
    public float moveSpeed = 300f; // Hýzý artýrdýk (Piksel/Saniye)
    public float destroyTime = 2f;

    private TextMeshProUGUI uiText;
    private float timer;
    private Color startColor;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();

        if (uiText != null)
        {
            startColor = uiText.color;
        }

        // Boyutunun düzgün olduðundan emin ol
        transform.localScale = Vector3.one;

        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        // 1. Yukarý Doðru Kayma
        // Vector3.up (0,1,0) ile hýzý ve zamaný çarpýp pozisyona ekliyoruz.
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // 2. Fade Out (Yavaþça Silinme)
        if (uiText != null)
        {
            timer += Time.deltaTime;
            // Son 1 saniyede silinsin istiyorsan burayý oynayabilirsin
            float alpha = Mathf.Lerp(1f, 0f, timer / destroyTime);
            uiText.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }
    }
}