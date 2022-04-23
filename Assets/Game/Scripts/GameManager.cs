using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public bool playerIsAlive = false;
    public GameObject player;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private Player _player;
    private int scoreToChangeDifficulty = 0;
    private int scoreToChangeLives = 0;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(player, Vector3.zero, Quaternion.identity);
            gameOver = false;
            _uiManager.HideTitleScreen();
            scoreToChangeDifficulty = 0;
            _spawnManager.spawnTime = 5f;
            scoreToChangeLives = 0;
        }

        if (_uiManager.score >= scoreToChangeDifficulty + 50 && _spawnManager.spawnTime > 0.5f)
        {
            SpawnUpdate();
        }

        if (playerIsAlive)
        {
            _player = GameObject.Find("Player(Clone)").GetComponent<Player>();
            if (_uiManager.score >= scoreToChangeLives + 500)
            {
                _player.gainLife();
                scoreToChangeLives += 500;
            }
        }

    }

    private void SpawnUpdate()
    {
        if (_spawnManager.spawnTime > 0.75f)
        {
            scoreToChangeDifficulty += 50;
            _spawnManager.spawnTime -= 0.25f;
        }
        else if (_spawnManager.spawnTime > 0.5f)
        {
            scoreToChangeDifficulty += 50;
            _spawnManager.spawnTime -= 0.05f;
        }
    }
}
