using UnityEngine;
using UnityEngine.UI;

public class DashSystem : MonoBehaviour
{
    [Header("Stamina Ayarlarý")]
    public Slider staminaSlider;
    public float maxStamina = 100f;
    public float dashCost = 50f;
    public float regenRate = 20f;
    public float smoothSpeed = 10f;

    [Header("Dash Rüzgar Efekti")]
    public Image dashWindImage; // Oluþturduðumuz Image'ý buraya atayacaðýz
    public float windEffectFadeSpeed = 2f; // Efektin kaybolma hýzý

    private float currentStamina;

    void Start()
    {
        currentStamina = maxStamina;
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }

        // Baþlangýçta efekti tamamen görünmez yapalým
        if (dashWindImage != null)
        {
            SetImageAlpha(0f);
        }
    }

    void Update()
    {
        // 1. Stamina Dolum Mantýðý
        if (currentStamina < maxStamina)
        {
            currentStamina += regenRate * Time.deltaTime;
            if (currentStamina > maxStamina) currentStamina = maxStamina;
        }

        // 2. Slider Smooth Geçiþ
        if (staminaSlider != null)
        {
            staminaSlider.value = Mathf.Lerp(staminaSlider.value, currentStamina, smoothSpeed * Time.deltaTime);
        }

        // --- YENÝ: Rüzgar Efekti Fade Out (Yavaþça Yok Olma) ---
        if (dashWindImage != null && dashWindImage.color.a > 0)
        {
            // Alpha deðerini zamanla azalt
            float newAlpha = dashWindImage.color.a - (windEffectFadeSpeed * Time.deltaTime);
            SetImageAlpha(newAlpha);
        }
        // -------------------------------------------------------
    }

    public bool CheckAndConsumeStamina()
    {
        if (currentStamina >= dashCost)
        {
            currentStamina -= dashCost;

            // --- YENÝ: Dash Baþarýlýysa Efekti Göster ---
            if (dashWindImage != null)
            {
                SetImageAlpha(1f); // Alpha'yý 1 yap, anýnda görünür olsun
            }
            // -------------------------------------------

            return true;
        }
        return false;
    }

    // Image'ýn alpha deðerini kolayca ayarlamak için yardýmcý fonksiyon
    private void SetImageAlpha(float alpha)
    {
        Color color = dashWindImage.color;
        color.a = Mathf.Clamp01(alpha); // Deðeri 0 ile 1 arasýnda tut
        dashWindImage.color = color;
    }
}