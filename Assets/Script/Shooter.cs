using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform shootPoint; 
    [SerializeField] private GameObject shootPointPrefab; 
    [SerializeField] private AudioClip shootSound;  // เสียงที่ต้องการใช้เมื่อยิง
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Shooting();
        }
    }

    void Shooting()
    {
        RaycastHit hit;

        Debug.DrawRay(shootPoint.position, shootPoint.forward * 30f, Color.red);

        if (Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 30f))
        {
            Debug.DrawRay(shootPoint.position, shootPoint.forward * hit.distance, Color.green);
            Debug.Log("Hit: " + hit.collider.name);
            
            GameObject fireObj = Instantiate(shootPointPrefab, shootPoint.position, Quaternion.identity);
            Destroy(fireObj, 1);

            // เล่นเสียงเมื่อยิง
            if (shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
            
            ICanDamage damageable = hit.collider.GetComponent<ICanDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(10);  
            }
        }
    }
}