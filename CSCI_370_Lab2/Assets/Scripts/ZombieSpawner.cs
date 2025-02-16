using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    public GameObject BasicZombie;

    void Start() {
        InvokeRepeating("SpawnZombies", 2.0f, 1.5f);
    }


    void Update() {
        
    }

    void SpawnZombies() {
            Instantiate(BasicZombie);
    }

}