using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    public float moveSpeed = 7f; // ความเร็วเริ่มต้น
    public float maxHealth = 30f; // จำนวนเลือดสูงสุดของตัวละคร
    private float currentHealth;

    private Rigidbody rb;

    // อ้างอิงถึง HealthBar
    private HealthBar healthBar;

    // Power-up variables
    public float speedBoost = 15f;  // ความเร็วพิเศษ
    public float speedBoostDuration = 5f; // ระยะเวลาของความเร็วพิเศษ
    private float speedBoostCooldown = 10f; // เวลา cooldown ของสกิล
    private float speedBoostTime = 0f; // เวลาที่ใช้สกิล
    private bool canUseSpeedBoost = true; // ใช้เพื่อตรวจสอบว่าคูลดาวน์เสร็จหรือยัง

    private bool isSpeedBoostActive = false; // สถานะการเปิดใช้สกิล

    // อ้างอิง UI GameOver Panel
    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;

        // หา HealthBar ใน Scene และทำการเชื่อมต่อ
        healthBar = FindObjectOfType<HealthBar>();  // หา HealthBar ใน Scene

        // เริ่มต้น Health Bar
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth); // เรียกใช้ฟังก์ชันอัปเดต HealthBar
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // ซ่อน UI GameOver ตั้งแต่เริ่มเกม
        }
    }

    private void Update()
    {
        // ถ้าตัวละครเลือดหมดให้หยุดการเคลื่อนไหว
        if (currentHealth <= 0) return;

        // ใช้สกิลความเร็วเมื่อกดปุ่ม E
        if (Input.GetKeyDown(KeyCode.E) && canUseSpeedBoost)
        {
            ActivateSpeedBoost();
        }

        // ถ้าใช้ SpeedBoost ให้เพิ่มความเร็ว
        if (isSpeedBoostActive)
        {
            // ลดเวลาเมื่อสกิลกำลังใช้งาน
            speedBoostTime -= Time.deltaTime;

            if (speedBoostTime <= 0)
            {
                DeactivateSpeedBoost();
            }
        }

        float move = Input.GetAxis("Horizontal");

        // หากไม่ใช่ Power-up ให้ใช้ความเร็วปกติ
        if (!isSpeedBoostActive)
        {
            rb.linearVelocity = new Vector3(move * moveSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        }
        else
        {
            rb.linearVelocity = new Vector3(move * speedBoost, rb.linearVelocity.y, rb.linearVelocity.z);  // ใช้ความเร็วสูงสุด
        }

        // จำกัดมุมการหมุนของแกน X และล็อคแกน Y, Z
        LimitRotation();
    }

    // ฟังก์ชันเปิดใช้ SpeedBoost
    private void ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;
        speedBoostTime = speedBoostDuration; // กำหนดเวลาให้เป็นระยะเวลาของสกิล
        canUseSpeedBoost = false; // ไม่ให้ใช้สกิลซ้ำทันที
        moveSpeed = speedBoost; // เปลี่ยนความเร็วเป็นความเร็วพิเศษ

        // เริ่มคูลดาวน์
        Invoke("ResetCooldown", speedBoostCooldown);  // ตั้งเวลาเพื่อรีเซ็ตคูลดาวน์
    }

    // ฟังก์ชันปิดการใช้ SpeedBoost
    private void DeactivateSpeedBoost()
    {
        isSpeedBoostActive = false;
        moveSpeed = 7f;  // กลับไปใช้ความเร็วปกติ
    }

    // ฟังก์ชันรีเซ็ตคูลดาวน์
    private void ResetCooldown()
    {
        canUseSpeedBoost = true; // เปิดใช้งานสกิลได้อีกครั้ง
    }

    // ฟังก์ชันในการลดเลือด
    public void TakeDamage(float amount)
    {
        if (!isSpeedBoostActive) // ถ้าไม่ใช่ SpeedBoost ก็โดนดาเมจ
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ป้องกันไม่ให้เลือดต่ำกว่า 0

            // อัปเดต Health Bar ทุกครั้งที่เลือดลด
            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(currentHealth, maxHealth); // อัปเดตค่า Health Bar
            }

            if (currentHealth <= 0)
            {
                Die(); // เรียกฟังก์ชันตายเมื่อเลือดหมด
            }
        }
    }

    // ฟังก์ชันเมื่อเลือดหมด
    private void Die()
    {
        Debug.Log("Character is dead!");

        // หยุดการเคลื่อนไหว
        rb.linearVelocity = Vector3.zero;

        // แสดง UI Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // แสดง UI GameOver
        }

        // หยุดเวลาในเกม
        Time.timeScale = 0; // หยุดเวลา
    }

    // ฟังก์ชันลดความเร็ว
    public void ReduceSpeed(float amount)
    {
        moveSpeed = Mathf.Max(1f, moveSpeed - amount); // ลดความเร็วแต่ไม่ให้ต่ำกว่า 1
        Debug.Log("Speed Reduced: " + moveSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // หากไม่ได้ใช้ SpeedBoost ก็จะโดนดาเมจ
            if (!isSpeedBoostActive)
            {
                TakeDamage(5f);  // ตั้งค่าให้ชนสิ่งกีดขวางแล้วเสียเลือด
            }

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
