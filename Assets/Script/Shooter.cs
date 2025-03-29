using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform shootPoint; 
    [SerializeField] private GameObject shootPointPrefab; 
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
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

            
            
            ICanDamage damageable = hit.collider.GetComponent<ICanDamage>();
            if (damageable != null)
            {
                damageable.TakeDamage(10);  
            }
        }
    }
}