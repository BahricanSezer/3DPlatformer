using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [Header("UI Referansý")]
    public GameObject uiTextObject; // "Press E" yazýsý

    [Header("Ýþlemler")]
    public GameObject[] objectsToDestroy; // Tamamen yok olacaklar
    public GameObject[] objectsToDisable; // Gizlenecekler (Active False)
    public GameObject[] objectsToEnable;  // Görünür olacaklar (Active True)

    private bool canInteract = false;

    void Start()
    {
        // Oyun baþlayýnca yazýyý gizle
        if (uiTextObject != null)
            uiTextObject.SetActive(false);
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            PerformInteraction();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            if (uiTextObject != null)
                uiTextObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            if (uiTextObject != null)
                uiTextObject.SetActive(false);
        }
    }

    void PerformInteraction()
    {
        // 1. Objeleri Yok Et
        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null) Destroy(obj);
        }

        // 2. Objeleri Kapat (Active False)
        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null) obj.SetActive(false);
        }

        // 3. Objeleri Aç (Active True)
        foreach (GameObject obj in objectsToEnable)
        {
            if (obj != null) obj.SetActive(true);
        }

        // UI Yazýsýný kapat
        if (uiTextObject != null)
            uiTextObject.SetActive(false);

        // Etkileþim bittiði için bu trigger objesini de yok et (Tek kullanýmlýk)
        Destroy(gameObject);
    }
}