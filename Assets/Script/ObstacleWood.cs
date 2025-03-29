using UnityEngine;

public class ObstacleWood : MonoBehaviour, ICanDamage
{
    [SerializeField] private float health = 20f;
    [SerializeField] private GameObject hitEffectPrefab; // เปลี่ยนเป็น GameObject

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}