using UnityEngine;

public class JumpColorPlatform : MonoBehaviour
{
    [Header("Renk Ayarý")]
    public Color targetColor = Color.cyan; // Zýplayýnca hangi renk olsun?

    private Renderer meshRenderer;
    private bool isPlayerOnTop = false;
    private bool hasChanged = false; // Sadece bir kere deðiþsin diye kilit

    void Start()
    {
        // Objenin üzerindeki Renderer bileþenini al (Rengi bu deðiþtirir)
        meshRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Þartlar:
        // 1. Oyuncu üstünde mi? (isPlayerOnTop)
        // 2. Daha önce rengi deðiþmemiþ mi? (!hasChanged)
        // 3. Boþluk tuþuna basýldý mý?
        if (isPlayerOnTop && !hasChanged && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        if (meshRenderer != null)
        {
            // Materyalin ana rengini hedef renkle deðiþtir
            meshRenderer.material.color = targetColor;

            // Kilidi kapat, artýk zýplasa da renk deðiþmeyecek
            hasChanged = true;
        }
    }

    // Oyuncu platforma bastýðý an
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTop = true;
        }
    }

    // Oyuncu platformdan ayrýldýðý (zýpladýðý veya indiði) an
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnTop = false;
        }
    }
}