using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

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

        // Application.targetFrameRate = 15;
        SetGameSpeed(0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            SetupGame();
        }

        if (IsGameRunning)
        {
            _unscaledGameTimer += Time.unscaledDeltaTime;
            CheckDifficulty();
        }
    }

    private void OnEnable()
    {
        PlayerHitDetector.OnPlayerDeath += OnPlayerDeath;
    }
    
    private void OnDisable()
    {
        PlayerHitDetector.OnPlayerDeath -= OnPlayerDeath;
    }
    
    private void SetupGame()
    {
        SetGameSpeed(1f);
        _buttonPromptText.gameObject.SetActive(false);
        _scoreText.gameObject.SetActive(true);
        _unscaledGameTimer = 0f;
        _currentDifficulty = 0;
        IsGameRunning = true;
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
        SetGameSpeed(0f);
    }

    private void SetGameSpeed(float gameSpeed)
    {
        GameSpeed = gameSpeed;
        Time.timeScale = gameSpeed;
    }
}
