using UnityEngine;
using UnityEngine.SceneManagement; // ใช้สำหรับการเปลี่ยนฉาก
using UnityEngine.UI; // ใช้สำหรับการเข้าถึง UI ของ Unity

public class MainMenu : MonoBehaviour
{
    // กำหนดปุ่มที่ใช้ในเมนู
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditButton;

    private void Start()
    {
        // ตรวจสอบว่าปุ่มมีการตั้งค่าใน Inspector หรือไม่
        if (playButton != null)
        {
            playButton.onClick.AddListener(PlayGame); // กดปุ่ม Play
        }
        if (creditButton != null)
        {
            creditButton.onClick.AddListener(ShowCredits); // กดปุ่ม Credit
        }
    }

    // ฟังก์ชันที่ทำงานเมื่อกดปุ่ม Play
    private void PlayGame()
    {
        // โหลดฉากที่ชื่อว่า "GameScene"
        SceneManager.LoadScene("Level1"); // คุณสามารถใส่ชื่อฉากที่ต้องการเล่น
    }

    // ฟังก์ชันที่ทำงานเมื่อกดปุ่ม Credit
    private void ShowCredits()
    {
        // โหลดฉากที่ชื่อว่า "CreditScene"
        SceneManager.LoadScene("Credit"); // เปลี่ยนไปที่ฉาก Credit
    }
}