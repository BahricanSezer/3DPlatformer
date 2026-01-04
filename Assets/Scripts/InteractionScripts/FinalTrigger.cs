using UnityEngine;
using System.Collections;

public class FinalTrigger : MonoBehaviour
{
    [SerializeField] private GameObject uiTextObject;
    //public ParticleSystem[] particleEffects;
    [SerializeField] private float displayDuration = 5f;

    void Start()
    {
        if (uiTextObject != null) uiTextObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /*foreach (ParticleSystem ps in particleEffects)
            {
                if (ps != null) ps.Play();
            }*/

            if (uiTextObject != null)
            {
                uiTextObject.SetActive(true);
                StopAllCoroutines();
                StartCoroutine(HideTextRoutine());
            }
        }
    }

    IEnumerator HideTextRoutine()
    {
        yield return new WaitForSeconds(displayDuration);
        if (uiTextObject != null) uiTextObject.SetActive(false);
    }
}