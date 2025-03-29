using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float health = 100f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        // ถ้าตัวละครเลือดหมดให้หยุดการเคลื่อนไหว
        if (health <= 0) return; 

        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector3(move * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
    }

    public void ReduceSpeed(float amount)
    {
        moveSpeed = Mathf.Max(1f, moveSpeed - amount); // ลดความเร็วแต่ไม่ให้ต่ำกว่า 1
        Debug.Log("Speed Reduced: " + moveSpeed);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die(); // เรียกฟังก์ชันตายเมื่อเลือดหมด
        }
    }

    private void Die()
    {
        Debug.Log("Character is dead!");

        // หยุดการเคลื่อนที่
        rb.linearVelocity = Vector3.zero;

        // ทำให้ตัวละครหายไป
        Destroy(gameObject); // ตัวละครหายไปจากเกมเมื่อเลือดหมด

        // หยุดเวลาในเกม
        Time.timeScale = 0; // หยุดเวลา
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            rb.angularVelocity = Vector3.zero;
            // ให้แน่ใจว่าตัวละครไม่หมุน
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f); // ล็อคหมุนแค่แกน Y
        }
    }

}