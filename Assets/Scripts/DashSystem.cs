using UnityEngine;
using UnityEngine.UI;

public class DashSystem : MonoBehaviour
{
    public Slider staminaSlider;
    public float maxStamina = 100f;
    public float dashCost = 50f;
    public float regenRate = 20f;

    // Barýn ne kadar hýzlý tepki vereceði (Bunu artýrýrsan daha hýzlý smooth olur)
    public float smoothSpeed = 10f;

    private float currentStamina;

    void Start()
    {
        currentStamina = maxStamina;
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
    }

    void Update()
    {
        // 1. Stamina Dolum Mantýðý (Arka Plandaki Matematik)
        if (currentStamina < maxStamina)
        {
            currentStamina += regenRate * Time.deltaTime;
            if (currentStamina > maxStamina) currentStamina = maxStamina;
        }

        // 2. GÖRSEL KISIM (Smooth Geçiþ)
        // Slider'ýn deðeri, gerçek stamina deðerine doðru 'süzülerek' gider.
        if (staminaSlider != null)
        {
            staminaSlider.value = Mathf.Lerp(staminaSlider.value, currentStamina, smoothSpeed * Time.deltaTime);
        }
    }

    public bool CheckAndConsumeStamina()
    {
        if (currentStamina >= dashCost)
        {
            currentStamina -= dashCost;

            // BURAYI SÝLDÝK: staminaSlider.value = currentStamina;
            // Artýk slider'ý anýnda eþitlemiyoruz, Update kýsmýndaki Lerp onu yakalýyor.

            return true;
        }
        return false;
    }
}