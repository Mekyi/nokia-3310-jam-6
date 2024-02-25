using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float _spawnInterval = 5f;
    
    [SerializeField] 
    private List<GameObject> _enemyPrefabs = new List<GameObject>();

    private float _spawnTimer;

    private void Awake()
    {
        _spawnTimer = _spawnInterval;
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0f)
        {
            SpawnRandomEnemy();
            _spawnTimer = _spawnInterval;
        }
    }

    private void SpawnRandomEnemy()
    {
        var randomEnemy = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];

        Instantiate(randomEnemy, transform);
    }
}
