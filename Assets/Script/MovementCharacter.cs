using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float health = 15f;  // เลือดของตัวละคร
    public float maxHealth = 15f; // จำนวนเลือดสูงสุดของตัวละคร
    private Rigidbody rb;

    // อ้างอิงถึง HealthBar
    private HealthBar healthBar;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // หา HealthBar ใน Scene และทำการเชื่อมต่อ
        healthBar = FindObjectOfType<HealthBar>();  // หา HealthBar ใน Scene

        // เริ่มต้น Health Bar
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(health, maxHealth); // เรียกใช้ฟังก์ชันอัปเดต HealthBar
        }
    }

    private void Update()
    {
        // ถ้าตัวละครเลือดหมดให้หยุดการเคลื่อนไหว
        if (health <= 0) return;

        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector3(move * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        // จำกัดมุมการหมุนของแกน X และล็อคแกน Y, Z
        LimitRotation();
    }

    public void ReduceSpeed(float amount)
    {
        moveSpeed = Mathf.Max(1f, moveSpeed - amount); // ลดความเร็วแต่ไม่ให้ต่ำกว่า 1
        Debug.Log("Speed Reduced: " + moveSpeed);
    }

    // ฟังก์ชันในการลดเลือด
    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth); // ป้องกันไม่ให้เลือดต่ำกว่า 0

        // อัปเดต Health Bar ทุกครั้งที่เลือดลด
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(health, maxHealth); // อัปเดตค่า Health Bar
        }

        if (health <= 0)
        {
            Die(); // เรียกฟังก์ชันตายเมื่อเลือดหมด
        }
    }

    // ฟังก์ชันเมื่อเลือดหมด
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
            LimitRotation(); // จำกัดมุมการหมุนทุกครั้งที่ชนสิ่งกีดขวาง
        }
    }

    // จำกัดมุมการหมุนของตัวละคร
    private void LimitRotation()
    {
        Vector3 currentRotation = transform.eulerAngles;

        // แปลงค่า Rotation ให้อยู่ในช่วง -180 ถึง 180
        float clampedX = Mathf.Clamp((currentRotation.x > 180) ? currentRotation.x - 360 : currentRotation.x, -15f, 15f);

        // ล็อคแกน Y และ Z ไว้ที่ 0 องศา
        float lockedY = 0f;
        float lockedZ = 0f;

        // เซ็ตค่าการหมุนกลับไปที่ตัวละคร โดยล็อคแกน Y และ Z
        transform.rotation = Quaternion.Euler(clampedX, lockedY, lockedZ);
    }
}
