using UnityEngine;

public class CoffeeBean : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BasicZombie")) 
        {
            Destroy(other.gameObject); 
            Destroy(gameObject); 
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
