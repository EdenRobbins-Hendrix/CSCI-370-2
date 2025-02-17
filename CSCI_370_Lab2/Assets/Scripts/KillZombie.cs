using UnityEngine;

public class KillZombie : MonoBehaviour {

    void Start() {
        
    }

    void Update() {
        
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Destroy(col.gameObject);
        Destroy(gameObject);
    }
}