using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    private int currentwave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn = 10;
    private bool isSpawning = false;


    public GameObject BasicZombie;

    void Start() {
        InvokeRepeating("SpawnZombies", 1.5f, 3.0f);
    }


    void Update() {
    }

    void SpawnZombies() {
            Instantiate(BasicZombie);
    }

}