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
<<<<<<< HEAD
        if (collision.gameObject.CompareTag("Obstacle")) // ชนสิ่งกีดขวาง
=======
        // ถ้าเลือดหมดให้หยุดเคลื่อนไหว
        if (health <= 0) return; 

        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector3(move * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

       
    }

    public void ReduceSpeed(float amount)
    {
        moveSpeed = Mathf.Max(1f, moveSpeed - amount); // ลดความเร็วแต่ไม่ให้ต่ำกว่า 1
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
>>>>>>> a784d8c4a3485c8cb2840f41b8a04a36fd301ce6
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
<<<<<<< HEAD
       
        gameObject.SetActive(false); // ปิดตัวละครเมื่อ HP หมด
=======
        rb.linearVelocity = Vector3.zero; // หยุดการเคลื่อนที่
        gameObject.SetActive(false); // ปิดตัวละครแทนการ Destroy
>>>>>>> a784d8c4a3485c8cb2840f41b8a04a36fd301ce6
    }
}