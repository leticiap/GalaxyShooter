using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public bool canTripleShot = false;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _explodePrefab;
    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private GameObject _shieldGO;
    [SerializeField] private GameObject[] _engines;
    public bool isPlayer1;
    private int _hitCount;
    private bool _shield = false;
    private GameManager _gameManager;
    private float _canFire1 = 0.0f;
    private float _canFire2 = 0.0f;
    [SerializeField] private float _speed = 5.0f;
    private UIManager _uiManager;
    private AudioSource _audioSource;
	// Use this for initialization
	void Start ()
    {
        _hitCount = 0;
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (!_gameManager.isCoop)
            transform.position = new Vector3(0, 0, 0);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager != null)
        {
            _uiManager.UpdateLives(_lives, isPlayer1);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isPlayer1)
        {
            Movement(isPlayer1);
            if (Input.GetMouseButtonDown(0))
                Shoot(isPlayer1);
            else if (!_gameManager.isCoop && Input.GetKeyDown(KeyCode.Space))
                Shoot(isPlayer1);
        }
        else
        {
            Movement(isPlayer1);
            if (Input.GetKeyDown(KeyCode.Space))
                Shoot(isPlayer1);
        }
    }

    private void Movement(bool player)
    {
        if (!_gameManager.isCoop)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
        }
        else
        {
            if (player)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                    transform.Translate(Vector3.up * Time.deltaTime * _speed);
                if (Input.GetKey(KeyCode.DownArrow))
                    transform.Translate(Vector3.down * Time.deltaTime * _speed);
                if (Input.GetKey(KeyCode.LeftArrow))
                    transform.Translate(Vector3.left * Time.deltaTime * _speed);
                if (Input.GetKey(KeyCode.RightArrow))
                    transform.Translate(Vector3.right * Time.deltaTime * _speed);
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                    transform.Translate(Vector3.up * Time.deltaTime * _speed);
                if (Input.GetKey(KeyCode.S))
                    transform.Translate(Vector3.down * Time.deltaTime * _speed);
                if (Input.GetKey(KeyCode.A))
                    transform.Translate(Vector3.left * Time.deltaTime * _speed);
                if (Input.GetKey(KeyCode.D))
                    transform.Translate(Vector3.right * Time.deltaTime * _speed);
            }
        }
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
        if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.3f, transform.position.y, 0);
        }
        else if (transform.position.x > 9.5)
        {
            transform.position = new Vector3(-9.3f, transform.position.y, 0);
        }
    }

    private void Shoot(bool player)
    {
        if (player)
        {
          if (Time.time > _canFire1)
          {
              _audioSource.Play();
              _canFire1 = Time.time + _fireRate;
              Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
              if (canTripleShot)
              {
                  Instantiate(_laserPrefab, transform.position + new Vector3(0.55f, -0.25f, 0), Quaternion.identity);
                  Instantiate(_laserPrefab, transform.position + new Vector3(-0.55f, -0.25f, 0), Quaternion.identity);
              }
          }
        }
        else
        {
            if (Time.time > _canFire2)
            {
                _audioSource.Play();
                _canFire2 = Time.time + _fireRate;
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.25f, 0), Quaternion.identity);
                if (canTripleShot)
                {
                    Instantiate(_laserPrefab, transform.position + new Vector3(0.55f, -0.25f, 0), Quaternion.identity);
                    Instantiate(_laserPrefab, transform.position + new Vector3(-0.55f, -0.25f, 0), Quaternion.identity);
                }
            }
        }
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    private IEnumerator SpeedBostRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed = 5.0f;
    }

    public void TripleShotPowerUpOn ()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedPowerUpOn()
    {
        _speed *= 1.5f;
        StartCoroutine(SpeedBostRoutine());
    }

    public void HitEnemy()
    {
        if (!_shield)
        {
            _hitCount++;
            if (_hitCount == 1)
            {
                _engines[0].SetActive(true);
            }
            else if (_hitCount == 2)
            {
                _engines[1].SetActive(true);
            }
            _lives--;
            if (_uiManager != null)
            {
                _uiManager.UpdateLives(_lives, isPlayer1);
            }
        }
        else
        {
            _shield = false;
            _shieldGO.SetActive(false);
        }
        if (_lives < 1)
        {
            Instantiate(_explodePrefab, transform.position, Quaternion.identity);
            _gameManager.CheckGameOver();
            Destroy(this.gameObject);
        }
    }
    public void ShieldOn()
    {
        _shield = true;
        _shieldGO.SetActive(true);
    }
}
