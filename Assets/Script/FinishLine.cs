using UnityEngine;
using UnityEngine.SceneManagement; // ใช้สำหรับเปลี่ยนฉาก

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject winPanel; // อ้างอิง UI ที่จะแสดง

    private void Start()
    {
        winPanel.SetActive(false); // ซ่อน UI ตอนแรก
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ตรวจว่าตัวละครชนเส้นชัยไหม
        {
            winPanel.SetActive(true); // แสดง UI ผ่านด่าน
            Time.timeScale = 0; // หยุดเวลา
        }
    }

    // ฟังก์ชันสำหรับปุ่ม
    public void NextLevel()
    {
        Time.timeScale = 1; // กลับมาเล่นตามปกติ
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // ไปด่านต่อไป
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