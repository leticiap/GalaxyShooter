using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Image livesImageDisplay;
    public Image livesImageDisplay2;
    public GameObject titleScreen;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Sprite[] _livesSprite;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private bool _isCoop;
    private int _score = 0;
    private int _bestScore = 0;

    private void Start()
    {
        if (_isCoop)
        {
            _bestScore = PlayerPrefs.GetInt("HighScoreCoop");
            _bestScoreText.text = "Best: " + _bestScore;
        }
        else
        {
            _bestScore = PlayerPrefs.GetInt("HighScoreSingle");
            _bestScoreText.text = "Best: " + _bestScore;
        }
    }


    public void UpdateLives(int currentLives, bool player)
    {
        _scoreText.text = "Score: " + _score;
        if (player)
        {
            livesImageDisplay.sprite = _livesSprite[currentLives];
        }
        else
            livesImageDisplay2.sprite = _livesSprite[currentLives];

    }
    public void UpdateScore()
    {
        _score += 10;
        _scoreText.text = "Score: " + _score;
    }


    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        if (_score > _bestScore)
        {
            _bestScore = _score;
            if (_isCoop)
                PlayerPrefs.SetInt("HighScoreCoop", _bestScore);
            else
                PlayerPrefs.SetInt("HighScoreSingle", _bestScore);
        }
        _bestScoreText.text = "Best: " + _bestScore;
        _score = 0;
    }
    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
    }

    public void ResumePlay()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
