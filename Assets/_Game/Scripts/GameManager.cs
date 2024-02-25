using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    
    
    public float GameSpeed { get; private set; } = 1;

    public float SpriteSpeed { get; private set; } = 1;

    
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
        Time.timeScale = 0f;
    }
}
