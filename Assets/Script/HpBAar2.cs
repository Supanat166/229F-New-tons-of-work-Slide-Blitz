using UnityEngine;
using UnityEngine.UI;

public class HpBAar2 : MonoBehaviour
{
    [SerializeField] private Slider SliderHp;
    public float moveSpeed = 5f;
    public float health = 15f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // ถ้าเลือดหมดให้หยุดเคลื่อนไหว
        if (health <= 0) return; 

        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(move * moveSpeed, rb.velocity.y, rb.velocity.z);

       
    }

    public void ReduceSpeed(float amount)
    {
        moveSpeed = Mathf.Max(1f, moveSpeed - amount); // ลดความเร็วแต่ไม่ให้ต่ำกว่า 1
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die(); // เรียกฟังก์ชันตายเมื่อ HP หมด
        }
    }

    private void Die()
    {
        rb.velocity = Vector3.zero; // หยุดการเคลื่อนที่
        gameObject.SetActive(false); // ปิดตัวละครแทนการ Destroy
    }
}