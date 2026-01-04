using UnityEngine;
using UnityEngine.UI;

public class DashSystem : MonoBehaviour
{
    [Header("Stamina Settings")]
    public Slider staminaSlider;
    public float maxStamina = 100f;
    public float dashCost = 50f;
    public float regenRate = 20f;
    public float smoothSpeed = 10f;

    [Header("Dash UI Effect")]
    public Image dashWindImage; 
    public float windEffectFadeSpeed = 2f;

    private float currentStamina;

    void Start()
    {
        currentStamina = maxStamina;
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }

        if (dashWindImage != null)
        {
            SetImageAlpha(0f);
        }
    }

    void Update()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += regenRate * Time.deltaTime;
            if (currentStamina > maxStamina) currentStamina = maxStamina;
        }

        if (staminaSlider != null)
        {
            staminaSlider.value = Mathf.Lerp(staminaSlider.value, currentStamina, smoothSpeed * Time.deltaTime);
        }

        if (dashWindImage != null && dashWindImage.color.a > 0)
        {           
            float newAlpha = dashWindImage.color.a - (windEffectFadeSpeed * Time.deltaTime);
            SetImageAlpha(newAlpha);
        }
    }

    public bool CheckAndConsumeStamina()
    {
        if (currentStamina >= dashCost)
        {
            currentStamina -= dashCost;

            if (dashWindImage != null)
            {
                SetImageAlpha(1f);
            }

            return true;
        }
        return false;
    }

    private void SetImageAlpha(float alpha)
    {
        Color color = dashWindImage.color;
        color.a = Mathf.Clamp01(alpha); 
        dashWindImage.color = color;
    }
}