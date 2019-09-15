using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private GameObject _explodePrefab;
    private UIManager _uiManager;
    // Use this for initialization
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -7)
        {
            float randomX = Random.Range(-7, 7);
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
                player.HitEnemy();
            }
            Instantiate(_explodePrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            Laser laser = other.GetComponent<Laser>();
            if (laser != null)
            {
                laser.HitEnemy();
            }
            Instantiate(_explodePrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            if (_uiManager != null)
            {
                _uiManager.UpdateScore();
            }
        }
    }
}
