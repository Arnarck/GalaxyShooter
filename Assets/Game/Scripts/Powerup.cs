using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.5f;
    [SerializeField]
    private int _powerupID;//0 to Triple Shot, 1 to Speed Boost and 2 to Shield.
    [SerializeField]
    private AudioClip _clip;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //Access the player
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                if (_powerupID == 0)
                {
                    //enable triple shot
                    player.TripleShotPowerupOn();
                }
                else if (_powerupID == 1)
                {
                    player.SpeedBoostPowerupOn();
                }
                else if (_powerupID == 2)
                {
                    player.ShieldBoostPowerOn();
                }
                else
                {
                    Debug.Log("Powerup not found.");
                }

            }
            //Instantiate a audio clip in the world space. That's why the audio isn't destroyed when the powerup is destroyed. 
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            //Destroy ourself
            Destroy(this.gameObject);
        }
    }
}
