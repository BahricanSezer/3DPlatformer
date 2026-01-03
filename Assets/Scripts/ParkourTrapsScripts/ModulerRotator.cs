using UnityEngine;

public class ModulerRotator : MonoBehaviour
{
    [Header("Dönme Ayarlarý")]
    [Tooltip("Hangi eksende dönecek? (X, Y, Z için 0 veya 1 verin)")]
    public Vector3 rotationAxis = new Vector3(0, 1, 0); // Varsayýlan Y ekseni

    [Tooltip("Dönme hýzý. Ters yöne döndürmek için baþýna - koyun (Örn: -100)")]
    public float rotationSpeed = 50f;

    [Tooltip("Objenin kendi ekseninde mi (Self) yoksa dünya ekseninde mi (World) döneceði")]
    public Space relativeTo = Space.Self;

    void Update()
    {
        // Vector3.up (0,1,0) gibi vektörleri hýz ve zamanla çarparak döndürür
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, relativeTo);
    }
}