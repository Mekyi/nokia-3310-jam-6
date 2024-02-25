using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : MonoBehaviour
{
    private float _speed = 1;

    private void Start()
    {
        _speed = GameManager.Instance.SpriteSpeed;
    }

    private void Update()
    {
        var position = transform.position;
        
        var newPosition = Time.timeScale == 0f
                ? new Vector3(position.x, position.y, position.z)
                : new Vector3(position.x, position.y, position.z) + Vector3.left * _speed;

        transform.position = newPosition;
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
        this.enabled = false;
    }
}
