using UnityEngine;
using UnityEngine.UI; // สำหรับการใช้งาน Slider

public class HealthChange: MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;  // อ้างอิงถึงส่วน Fill ของ Slider
    [SerializeField] private Color fullHealthColor = Color.green;  // สีเมื่อ Health เต็ม
    [SerializeField] private Color lowHealthColor = Color.red;  // สีเมื่อ Health ต่ำ

    private void Start()
    {
        if (healthSlider != null)
        {
            // ตั้งค่าเริ่มต้น
            healthSlider.value = healthSlider.maxValue; // เริ่มเกมให้ Health เต็ม
        }
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (healthSlider != null)
        {
            // อัปเดตค่าของ Slider ตามค่า Health
            healthSlider.value = currentHealth;

            // คำนวณเปอร์เซ็นต์ของ Health ที่เหลือ
            float healthPercentage = currentHealth / maxHealth;

            // ใช้ Lerp เพื่อปรับสีของ Fill จากสีแดงไปเขียวตามเปอร์เซ็นต์ของ Health
            fillImage.color = Color.Lerp(lowHealthColor, fullHealthColor, healthPercentage);
        }
    }
}