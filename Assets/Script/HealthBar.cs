using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float maxHealth = 15f;
    private float currentHealth;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Gradient colorGradient;

    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth;
    }

    // ฟังก์ชันอัปเดต health bar โดยรับ currentHealth และ maxHealth จากตัวละคร
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;

        // แสดงผลค่าของ Health ที่อัปเดตใน HealthBar
        healthText.text = "Health: " + currentHealth;

        // คำนวณค่าสัดส่วนของเลือดที่เหลือ
        float targetFillAmount = currentHealth / maxHealth;
        healthBarFill.fillAmount = targetFillAmount;

        // เปลี่ยนสีของ health bar ตามค่าสัดส่วนของเลือด
        healthBarFill.color = colorGradient.Evaluate(targetFillAmount);
    }

    // ฟังก์ชันเรียกใช้เมื่อเลือดลดลงหรือเพิ่มขึ้น
    public void UpdateHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        healthText.text = "Health: " + currentHealth;
        UpdateHealthBar(currentHealth, maxHealth);
    }
}

