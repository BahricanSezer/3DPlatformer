using UnityEngine;

public class DanceSystem : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        // Tuþlara basýlýnca ilgili dans ID'sini gönder
        if (Input.GetKeyDown(KeyCode.Alpha1)) StartDance(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) StartDance(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) StartDance(3);
        if (Input.GetKeyDown(KeyCode.Alpha4)) StartDance(4);
        if (Input.GetKeyDown(KeyCode.Alpha5)) StartDance(5);

        // Eðer oyuncu hareket tuþlarýna basarsa dansý iptal et (ID'yi 0 yap)
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            animator.SetInteger("DanceID", 0);
        }

        // Zýplama veya Dash atýnca da dans iptal olsun
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
        {
            animator.SetInteger("DanceID", 0);
        }
    }

    void StartDance(int id)
    {
        animator.SetInteger("DanceID", id);
    }
}