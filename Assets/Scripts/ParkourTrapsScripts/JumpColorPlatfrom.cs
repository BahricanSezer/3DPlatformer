using UnityEngine;

public class JumpColorPlatform : MonoBehaviour
{
    [Header("Color Setting")]
    [SerializeField] private Color targetColor = Color.cyan; 

    private Renderer meshRenderer;
    private bool isPlayerOnTop = false;
    private bool hasChanged = false; 

    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isPlayerOnTop && !hasChanged && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = targetColor;

            hasChanged = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTop = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTop = false;
        }
    }
}