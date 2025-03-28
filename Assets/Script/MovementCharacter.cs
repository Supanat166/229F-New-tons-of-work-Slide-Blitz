using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    public float moveSpeed = 5f;

    private void Update()
    {
        float move = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * move * moveSpeed * Time.deltaTime);
        
        transform.rotation = Quaternion.identity; 
    }
}