using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    private static bool isRetrying = false; // ตัวแปรเพื่อตรวจสอบว่าเป็นการ retry หรือไม่

    private bool hasFinished = false;

    private void Awake()
    {
        Time.timeScale = 1; // รีเซ็ตเวลาให้ทำงานปกติทุกครั้งที่โหลดฉากใหม่
    }

    private void Start()
    {
        // ซ่อน UI ตอนเริ่มเกม
        if (winPanel != null)
            winPanel.SetActive(false);

        // รีเซ็ตค่าถ้าเป็นการ retry
        if (isRetrying)
        {
            hasFinished = false;
            isRetrying = false; // รีเซ็ตตัวแปร
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasFinished)
        {
            hasFinished = true;
            if (winPanel != null)
                winPanel.SetActive(true); // แสดง UI

            Time.timeScale = 0; // หยุดเกม
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        isRetrying = true; // ตั้งค่าการ retry
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Credit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Credit");
    }
}