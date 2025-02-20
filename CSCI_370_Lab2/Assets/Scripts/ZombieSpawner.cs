using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ZombieSpawner : MonoBehaviour {

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 1f;
    [SerializeField] private float timeBetweenWaves = 5f;

 
 
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private void Awake() {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    [Header("WaveInfo")]
    [SerializeField] private int currentwave = 1;
    [SerializeField] private float timeSinceLastSpawn;
    [SerializeField] private int enemiesAlive;
    [SerializeField] private int enemiesLeftToSpawn;
    [SerializeField] private bool isSpawning = false;


    public GameObject BasicZombie;

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "TowerDefense") {
            GameManager.main.spawning = true;
            GameManager.main.startpoint = GameObject.Find("StartPoint").transform;
            GameManager.main.path[0] = GameObject.Find("Point").transform;
            GameManager.main.path[1] = GameObject.Find("Point (1)").transform;
            GameManager.main.path[2] = GameObject.Find("Point (2)").transform;
            GameManager.main.path[3] = GameObject.Find("Point (3)").transform;
            GameManager.main.path[4] = GameObject.Find("Point (4)").transform;
            GameManager.main.path[5] = GameObject.Find("Point (5)").transform;
            GameManager.main.path[6] = GameObject.Find("Point (6)").transform;
            GameManager.main.path[7] = GameObject.Find("Point (7)").transform;
            GameManager.main.path[8] = GameObject.Find("Point (8)").transform;
            GameManager.main.path[9] = GameObject.Find("Point (9)").transform;
            GameManager.main.path[10] = GameObject.Find("Point (10)").transform;
            GameManager.main.path[11] = GameObject.Find("Point (11)").transform;
            GameManager.main.path[12] = GameObject.Find("Point (12)").transform;
            GameManager.main.path[13] = GameObject.Find("Point (13)").transform;
            GameManager.main.path[14] = GameObject.Find("Point (14)").transform;
            GameManager.main.path[15] = GameObject.Find("Point (15)").transform;
            GameManager.main.path[16] = GameObject.Find("Point (16)").transform;
            GameManager.main.path[17] = GameObject.Find("Point (17)").transform;
            GameManager.main.path[18] = GameObject.Find("EndPoint").transform;
        if (currentwave == 1){
            Start();
        }
        }
    }

    void Start() {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave() {
        if (GameManager.main.spawning){
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();}
    }

    public void GameOver(){
        GameManager.main.resetGame();
        currentwave = 1;
    }

    private int EnemiesPerWave() {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentwave, 0.75f));
    }

    private void EnemyDestroyed() {
        enemiesAlive--;
    }


    void Update() {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0) {
            SpawnZombies();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0){
            EndWave();
        }

        if (GameManager.main.health <= 0){
            GameOver();
        }
    }

    private void EndWave() {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentwave++;
        if (GameManager.main.health > 0) {
            GameManager.main.changeSceneInShop();
        }
    }

    private void SpawnZombies() {
            Instantiate(BasicZombie, GameManager.main.startpoint.position, Quaternion.identity);
    }

}