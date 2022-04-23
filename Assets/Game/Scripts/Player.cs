using System.Collections;
using System.Collections.Generic;
using UnityEngine;//Permite uma classe herdar o MonoBehaviour

public class Player : MonoBehaviour
{
    public bool canTripleShot = false;
    public bool canSpeedUp = false;
    public bool shieldIsActive = false;

    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.25f;
    [SerializeField]
    private int speed = 1;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject[] _engineFailure;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private float _canFire = 0.0f;
    private int lives = 3;
    private AudioSource _audioSource;
    private int engine;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();

        transform.position = new Vector3(0, 0, 0);
        _gameManager.playerIsAlive = true;

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }
        else
        {
            Debug.Log("UIManager not found.");
        }

        //engine = Random.Range(0, 2);
    }

    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            if (canTripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.95f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (canSpeedUp)
        {
            transform.Translate(new Vector3(horizontalInput * speed * 1.5f * Time.deltaTime, verticalInput * speed * 1.5f * Time.deltaTime, 0));
        }
        else
        {
            transform.Translate(new Vector3(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime, 0));
        }

        if (transform.position.x >= 8.2f)
        {
            transform.position = new Vector3(8.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -8.2f)
        {
            transform.position = new Vector3(-8.2f, transform.position.y, 0);
        }

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
        //transform.Translate(new Vector3(1, 0, 0));
    }

    public void TripleShotPowerupOn()
    {
        if (canTripleShot)
        {
            StopCoroutine("TripleShotPowerDownRoutine");
        }

        canTripleShot = true;
        StartCoroutine("TripleShotPowerDownRoutine");
    }

    public void SpeedBoostPowerupOn()
    {
        if (canSpeedUp)
        {
            StopCoroutine("SpeedBoostPowerDownRoutine");
        }

        canSpeedUp = true;
        StartCoroutine("SpeedBoostPowerDownRoutine");
    }

    public void ShieldBoostPowerOn()
    {
        shieldIsActive = true;
        _shieldGameObject.SetActive(true);
    }

    public void Loselife()
    {
        if (shieldIsActive)
        {
            shieldIsActive = false;
            _shieldGameObject.SetActive(false);
        }
        else
        {
            lives--;
            _uiManager.UpdateLives(lives);

            if (lives == 2)
            {
                engine = Random.Range(0, 2);
                _engineFailure[engine].SetActive(true);
            }
            else if (lives == 1)
            {
                if (!_engineFailure[0].activeInHierarchy)
                {
                    _engineFailure[0].SetActive(true);
                }
                else
                {
                    _engineFailure[1].SetActive(true);
                }
            }
            else if (lives < 1)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _gameManager.gameOver = true;
                _uiManager.ShowtitleScreen();
                _gameManager.playerIsAlive = false;
                Destroy(this.gameObject);
            }
        }
    }

    public void gainLife() {
        if (lives < 3)
        {
            lives++;
            _uiManager.UpdateLives(lives);
            if (lives == 2)
            {
                engine = Random.Range(0, 2);
                _engineFailure[engine].SetActive(false);
            }
            if (lives == 3)
            {
                if (_engineFailure[0].activeInHierarchy)
                {
                    _engineFailure[0].SetActive(false);
                }
                else
                {
                    _engineFailure[1].SetActive(false);
                }
            }
        }
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(7.0f);

        canTripleShot = false;
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        canSpeedUp = false;
    }
}
