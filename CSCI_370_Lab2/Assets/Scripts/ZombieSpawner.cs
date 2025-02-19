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


    private int currentwave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;


    public GameObject BasicZombie;

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