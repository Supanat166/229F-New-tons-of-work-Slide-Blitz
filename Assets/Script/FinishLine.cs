using UnityEngine;
using UnityEngine.SceneManagement; // ใช้สำหรับเปลี่ยนฉาก

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject winPanel; // อ้างอิง UI ที่จะแสดง

    private bool hasFinished = false; // ใช้เพื่อป้องกันการเปิด UI ซ้ำ

    private void Start()
    {
        winPanel.SetActive(false); // ซ่อน UI ตอนแรก
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFinished) // ตรวจว่าตัวละครชนเส้นชัยไหม
        {
            hasFinished = true; // ตั้งค่าว่าเสร็จสิ้นแล้ว
            winPanel.SetActive(true); // แสดง UI
            Time.timeScale = 0; // หยุดเวลาในเกม
        }
    }

    // ฟังก์ชันสำหรับปุ่มต่าง ๆ
    public void NextLevel()
    {
        Time.timeScale = 1; // กลับมาเล่นตามปกติ
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // ไปด่านถัดไป
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu"); // ไปที่หน้าเมนูหลัก (ใส่ชื่อ Scene ให้ตรง)
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // โหลดด่านเดิมอีกครั้ง
    }
}