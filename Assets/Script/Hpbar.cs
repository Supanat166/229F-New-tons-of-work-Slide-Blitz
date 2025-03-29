using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Slider sliderHp;

    [SerializeField] private float currentHp;

    [SerializeField] private float maxHp;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= 0)
        {
            currentHp = 0;
            gameObject.SetActive(false); 
        }
        sliderHp.value = currentHp / maxHp;
    }
    public void takeDamage(float damageAmount)

    {
        currentHp -= damageAmount;
        sliderHp.value = currentHp / maxHp;
    }
    
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Obstacle")) 
        { 
            takeDamage(5);
        }
    }

}

