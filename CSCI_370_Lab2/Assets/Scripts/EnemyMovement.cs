using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 3f;

    private Transform target;
    private int pathIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameManager.main.path[pathIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f) {
            pathIndex++;

            if (pathIndex == GameManager.main.path.Length){
                ZombieSpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                GameManager.main.IncHealth(10);
                return;
        } else {
            target = GameManager.main.path[pathIndex];
        }
        }
    }

    private void FixedUpdate() {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
    }
}
