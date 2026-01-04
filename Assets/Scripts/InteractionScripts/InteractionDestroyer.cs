using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [Header("UI Text")]
    public GameObject uiTextObject;

    [Header("Yapilanlar")]
    [SerializeField] private GameObject[] objectsToDestroy;
    [SerializeField] private GameObject[] objectsToDisable;
    [SerializeField] private GameObject[] objectsToEnable;  

    private bool canInteract = false;

    void Start()
    {
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
        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null) Destroy(obj);
        }

        foreach (GameObject obj in objectsToDisable)
        {
            if (obj != null) obj.SetActive(false);
        }

        foreach (GameObject obj in objectsToEnable)
        {
            if (obj != null) obj.SetActive(true);
        }
        if (uiTextObject != null)
            uiTextObject.SetActive(false);

        Destroy(gameObject);
    }
}