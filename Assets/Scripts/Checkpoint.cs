using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject uiTextPrefab;

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
            GameObject canvas = GameObject.Find("Canvas");

            if (canvas != null)
            {
                GameObject textObj = Instantiate(uiTextPrefab, canvas.transform);
                RectTransform rect = textObj.GetComponent<RectTransform>();
                if (rect != null)
                {
                    rect.anchoredPosition = new Vector2(0, 100); 
                }
            }
            else
            {
                Debug.LogError("Canvasý bulamadýmssssss");
            }
        }
    }
}