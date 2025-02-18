using UnityEngine;
using System;
using System.Collections;
public class CoffeeBean : MonoBehaviour
{

    public GameObject blood;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BasicZombie")) 
        {
            Destroy(other.gameObject); 
            Destroy(gameObject); 
            ZombieSpawner.onEnemyDestroy.Invoke();
            GameManager.main.IncScore(10);
            GameObject go = Instantiate(blood);
            go.transform.position = transform.position;
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
