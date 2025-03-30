using UnityEngine;
using UnityEngine.UI;


using UnityEngine;
using UnityEngine.UI;

public class HpBar2 : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 15f;
    private float maxHealth = 15f;

    private void Start()
    {
        UpdateHealthBar();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) // ชนสิ่งกีดขวาง
        {
          
            TakeDamage(5);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, maxHealth); // ป้องกันค่าติดลบ
        UpdateHealthBar();

        if (healthAmount <= 0)
        {
            Die();
        }
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, maxHealth); // ป้องกันเกิน Max
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = healthAmount / maxHealth;
        }
    }

    private void Die()
    {
       
        gameObject.SetActive(false); // ปิดตัวละครเมื่อ HP หมด
    }
}