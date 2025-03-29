using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float health = 15f;
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

        // จำกัดมุมการหมุนของแกน X และล็อคแกน Y, Z
        LimitRotation();
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
            LimitRotation(); // จำกัดมุมการหมุนทุกครั้งที่ชนสิ่งกีดขวาง
        }
    }

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