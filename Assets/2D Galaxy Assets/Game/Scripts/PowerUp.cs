using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    private float _speed = 3.0f;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private int _poweupID; //0 = triple shot 1 = speed 2 = shield
	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            if (player != null)
            {
                // enable triple shot
                if (_poweupID == 0)
                {
                    player.TripleShotPowerUpOn();
                }
                // enable speed boost
                else if (_poweupID == 1)
                {
                    player.SpeedPowerUpOn();
                }
                // enable shield
                else if (_poweupID == 2)
                {
                    player.ShieldOn();
                }
            }
            Destroy(this.gameObject);
        }
    }
}
