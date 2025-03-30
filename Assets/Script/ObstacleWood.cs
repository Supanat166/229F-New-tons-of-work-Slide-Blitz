using UnityEngine;

public class ObstacleWood : MonoBehaviour, ICanDamage
{
    [SerializeField] private float health = 20f;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private float speedReduction = 1f;
    [SerializeField] private float damageAmount = 5f; // เพิ่มตัวแปรความเสียหาย
    [SerializeField] private AudioClip hitSound;  // เสียงเมื่อโดนยิง
    [SerializeField] private AudioClip destroySound;  // เสียงเมื่อถูกทำลาย
    private AudioSource audioSource;  // ตัวจัดการเสียง

    void Start()
    {
        // หา AudioSource ที่ติดอยู่กับ GameObject นี้
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  // ถ้าไม่มี ก็สร้างใหม่
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        // เล่นเสียงเมื่อโดนยิง
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (hitEffectPrefab != null)
        {
            GameObject woodObj = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(woodObj, 1);
        }

        if (health <= 0)
        {
            // เล่นเสียงเมื่อถูกทำลาย
            if (destroySound != null)
            {
                audioSource.PlayOneShot(destroySound);
            }

            Destroy(gameObject); // ทำลายสิ่งกีดขวางเมื่อพลังชีวิตหมด
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MovementCharacter player = collision.gameObject.GetComponent<MovementCharacter>();
            if (player != null)
            {
                player.ReduceSpeed(speedReduction); // ลดความเร็วของตัวละคร
                player.TakeDamage(damageAmount); // ลดเลือดของตัวละคร
            }

            Destroy(gameObject); // ทำลายสิ่งกีดขวางเมื่อชน
        }
    }
}