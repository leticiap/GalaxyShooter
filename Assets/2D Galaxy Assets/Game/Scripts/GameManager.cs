using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;
    [SerializeField] GameObject _coop;
    public GameObject spawnManager;
    private GameObject _coopPlayers;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    public bool isCoop = false;
    [SerializeField] private GameObject _pauseMenu;
    private int _deaths; // used to control coop ending game
	// Use this for initialization
	void Start ()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (gameOver)
        {
            _deaths = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(spawnManager, Vector3.zero, Quaternion.identity);
                _spawnManager = GameObject.Find("SpawnManager(Clone)").GetComponent<SpawnManager>();
                if (!isCoop)
                    Instantiate(player, Vector3.zero, Quaternion.identity);
                else
                {
                    Instantiate(_coop, Vector3.zero, Quaternion.identity);
                    _coopPlayers = GameObject.Find("Coop Players(Clone)");
                }
                gameOver = false;
                _uiManager.HideTitleScreen();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
	}

    public void CheckGameOver ()
    {
        if (isCoop)
        {
            _deaths++;
            if (_deaths >= 2)
            {
                if (_coopPlayers != null)
                    Destroy(_coopPlayers.gameObject);
                gameOver = true;
                if (_spawnManager != null)
                    _spawnManager.DestroySpawnManager();
                if (_uiManager != null)
                    _uiManager.ShowTitleScreen();
            }
        }
        else
        {
            gameOver = true;
            if (_spawnManager != null)
                _spawnManager.DestroySpawnManager();
            if (_uiManager != null)
                _uiManager.ShowTitleScreen();
        }
    }
}
