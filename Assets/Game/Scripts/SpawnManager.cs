using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;

    private GameManager _gameManager;
    private float currentTime = 0f;

    public float spawnTime = 3f;

    private void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (spawnTime < 0.5f)
        {
            spawnTime = 0.5f;
        }

        if (!_gameManager.gameOver && currentTime > spawnTime)
        {
            EnemySpawnRoutine();
            PowerupSpawnRoutine();
            currentTime = 0;
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private void EnemySpawnRoutine()
    {
        Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7.7f, 7.7f), transform.position.y, 0), Quaternion.identity);
    }

    private void PowerupSpawnRoutine()
    {
        int randomPowerup = Random.Range(0, 3);
        Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-8f, 8f), transform.position.y, 0), Quaternion.identity);
    }
}
