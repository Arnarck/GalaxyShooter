using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyExplosionPrefab;
    [SerializeField]
    private AudioClip _clip;

    private UIManager _uiManager;
    private float _speed = 6f;
    //private bool turnRight = true;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //O código abaixo pode ser utilizado num futuro "powerup" de inimigos, o qual permitirá a eles que se movimentem no eixo X.
        /*if (transform.position.x >= 7.7f)
        {
            transform.position = new Vector3(7.7f, transform.position.y, 0);
            turnRight = false;
        }
        else if (transform.position.x <= -7.7f)
        {
            transform.position = new Vector3(-7.7f, transform.position.y, 0);
            turnRight = true;
        }

        if (turnRight)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }*/

        if (transform.position.y <= -7)
        {
            float randomX = Random.Range(-7.7f, 7.7f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Loselife();
            }
        }
        else if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject);
            _uiManager.UpdateScore();
        }

        Instantiate(_EnemyExplosionPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
        Destroy(this.gameObject);
    }
}
