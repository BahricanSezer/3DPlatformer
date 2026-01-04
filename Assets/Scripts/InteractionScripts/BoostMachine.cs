using UnityEngine;
using System.Collections;

public class BoostMachine : MonoBehaviour
{
    [SerializeField] private GameObject uiTextObject;
    [SerializeField] private GameObject feedbackTextObject;
    [SerializeField] private GameObject objectToHide;
    [SerializeField] private GameObject objectToShow;
    [SerializeField] private float jumpBoostAmount = 2f;

    private bool canInteract = false;
    private bool isUsed = false;
    private PlayerController currentPlayer;

    void Start()
    {
        if (uiTextObject != null) uiTextObject.SetActive(false);
        if (feedbackTextObject != null) feedbackTextObject.SetActive(false);
        if (objectToShow != null) objectToShow.SetActive(false);
    }

    void Update()
    {
        if (canInteract && !isUsed && Input.GetKeyDown(KeyCode.E))
        {
            BuyItem();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            currentPlayer = other.GetComponent<PlayerController>();

            if (!isUsed && uiTextObject != null)
                uiTextObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            currentPlayer = null;
            if (uiTextObject != null) uiTextObject.SetActive(false);
        }
    }

    void BuyItem()
    {
        isUsed = true;

        if (currentPlayer != null && currentPlayer.jumpSystem != null)
        {
            currentPlayer.jumpSystem.jumpForce += jumpBoostAmount;
        }

        if (objectToHide != null) objectToHide.SetActive(false);
        if (objectToShow != null) objectToShow.SetActive(true);

        if (uiTextObject != null) uiTextObject.SetActive(false);

        if (feedbackTextObject != null)
        {
            feedbackTextObject.SetActive(true);
            StartCoroutine(HideFeedback());
        }
    }

    IEnumerator HideFeedback()
    {
        yield return new WaitForSeconds(2f);
        if (feedbackTextObject != null)
            feedbackTextObject.SetActive(false);
    }
}