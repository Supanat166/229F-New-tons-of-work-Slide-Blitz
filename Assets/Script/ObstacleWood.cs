using UnityEngine;

public class ObstacleWood : MonoBehaviour, ICanDamage
{
    [SerializeField] private float health = 20f;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private float speedReduction = 1f;
    [SerializeField] private float damageAmount = 5f; // เพิ่มตัวแปรความเสียหาย

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        if (health <= 0)
        {
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