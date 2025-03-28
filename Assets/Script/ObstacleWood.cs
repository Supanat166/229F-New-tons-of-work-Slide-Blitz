using UnityEngine;

public class ObstacleWood : MonoBehaviour, ICanDamage
{
    [SerializeField] private float health = 20f;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
