using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameRunning { get; private set; }
    
    public float GameSpeed { get; private set; } = 1;

    public float SpriteSpeed { get; private set; } = 5;
    
    public float Score { get; private set; } = 0;

    [SerializeField] 
    private TextMeshProUGUI _scoreText;
    
    [SerializeField] 
    private TextMeshProUGUI _buttonPromptText;

    [SerializeField] 
    private float _pointsPerSecond = 100f;

    [SerializeField] 
    private List<float> _speedDifficultyModifiers = new List<float>() { 0.5f, 0.75f, 1f };

    [SerializeField] 
    private float _difficultyChangeInterval = 20f;

    private int _currentDifficulty = 0;

    private float _unscaledGameTimer = 0;

    private bool _gameRestarting = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        Application.targetFrameRate = 15;
        SetGameSpeed(0f);
    }

    private void Update()
    {
        if (IsGameRunning)
        {
            _unscaledGameTimer += Time.unscaledDeltaTime;
            CheckDifficulty();
            SetScore();
        }
        else if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)) && !_gameRestarting)
        {
            StartRound();
        }
    }

    private void OnEnable()
    {
        PlayerHitDetector.OnPlayerDeath += OnPlayerDeath;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        PlayerHitDetector.OnPlayerDeath -= OnPlayerDeath;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        IsGameRunning = false;
        _unscaledGameTimer = 0f;
        _currentDifficulty = 0;
        _buttonPromptText.gameObject.SetActive(true);
    }

    private void StartRound()
    {
        IsGameRunning = true;
        SetGameSpeed(1f);
        _buttonPromptText.gameObject.SetActive(false);
        _scoreText.gameObject.SetActive(true);
    }
    
    private void CheckDifficulty()
    {
        var nextDifficultyTime = (_currentDifficulty + 1) * _difficultyChangeInterval;

        if (_unscaledGameTimer >= nextDifficultyTime)
        {
            _currentDifficulty++;
            float difficultyModifier =
                _speedDifficultyModifiers[Mathf.Clamp(
                    _currentDifficulty,
                    0,
                    _speedDifficultyModifiers.Count - 1)];

            Time.timeScale = 1 * difficultyModifier;
        }
    }

    private void OnPlayerDeath()
    {
        _gameRestarting = true;
        IsGameRunning = false;
        SetGameSpeed(0f);
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.GameOver, transform.position);
        StartCoroutine(InitiateRestartGame());
    }

    private void SetGameSpeed(float gameSpeed)
    {
        GameSpeed = gameSpeed;
        Time.timeScale = gameSpeed;
    }

    private void SetScore()
    {
        var newScoreText = Mathf.RoundToInt(_unscaledGameTimer * 10f).ToString("D5");

        _scoreText.text = $"<mspace=6>{newScoreText}</mspace>";
    }

    private IEnumerator InitiateRestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        _scoreText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _gameRestarting = false;
    }
}
