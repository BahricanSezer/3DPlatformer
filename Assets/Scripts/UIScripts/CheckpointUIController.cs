using UnityEngine;
using TMPro;

public class CheckpointUIController : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 300f; 
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

        transform.localScale = Vector3.one;

        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        if (uiText != null)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, timer / destroyTime);
            uiText.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }
    }
}