using UnityEngine;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // เวลาในการแสดงเครดิตก่อนที่จะเปลี่ยนไปหน้าเมนู (ในหน่วยวินาที)
    public float timeBeforeReturnToMenu = 5f;

    // ฟังก์ชันเริ่มต้นจะถูกเรียกเมื่อเริ่ม Scene
    void Start()
    {
        // เรียกฟังก์ชัน GoToMenu หลังจากเวลาที่กำหนด
        Invoke("GoToMenu", timeBeforeReturnToMenu);
    }

    // ฟังก์ชันที่จะถูกเรียกเมื่อถึงเวลาแล้ว
    void GoToMenu()
    {
        // เปลี่ยนฉากไปยังหน้าเมนูที่ชื่อว่า "MainMenu"
        SceneManager.LoadScene("MainMenu");
    }
}

