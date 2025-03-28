using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject shootPointPrefab;
    [SerializeField] private GameObject hitPointPrefab;
    
    void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        RaycastHit hit;
        
        Debug.DrawRay(shootPoint.position,  Vector3.forward * 30f, Color.red);

        if (Physics.Raycast(shootPoint.position, Vector3.forward, out hit, 30f))
        {
            Debug.DrawRay(shootPoint.position,  Vector3.forward * hit.distance, Color.green);
            Debug.Log("hit" + hit.collider.name);
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject fireObj = Instantiate(shootPointPrefab, shootPoint.position, Quaternion.identity);
                GameObject fireObj2 = Instantiate(hitPointPrefab, hit.point, Quaternion.identity);
                
                Destroy( fireObj, 1);
                Destroy( fireObj2, 1);

                ICanDamage damageable = hit.collider.GetComponent<ICanDamage>();
                if (damageable != null)
                {
                    damageable.TakeDamage(10);  
                }
            }
        }
    }
    
}
