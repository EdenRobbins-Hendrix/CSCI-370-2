using UnityEngine;
using System;
using System.Collections;
public class CoffeeBean : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BasicZombie")) 
        {
            Destroy(other.gameObject); 
            Destroy(gameObject); 
            GameManager.main.IncScore(10);
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
