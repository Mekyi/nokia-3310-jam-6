using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public float GameSpeed { get; private set; } = 1;

    public float SpriteSpeed { get; private set; } = 1;
    
    public float Score { get; private set; } = 0;

    [SerializeField] 
    private TextMeshProUGUI _scoreText;
    
    [SerializeField] 
    private TextMeshProUGUI _buttonPromptText;

    [SerializeField] 
    private float _pointsPerSecond = 100f;

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
            SetGameSpeed(1f);
            _buttonPromptText.gameObject.SetActive(false);
            _scoreText.gameObject.SetActive(true);
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
