using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHitDetector : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    
    [SerializeField] 
    private OnTriggerEnterBroadcaster _runColliderTrigger;
    
    [SerializeField] 
    private OnTriggerEnterBroadcaster _slideColliderTrigger;
    private LayerMask _enemyLayerMask;

    private void OnEnable()
    {
        _runColliderTrigger.OnTriggerEntered += OnTriggerEntered;
        _slideColliderTrigger.OnTriggerEntered += OnTriggerEntered;
    }
    
    private void OnDisable()
    {
        _runColliderTrigger.OnTriggerEntered -= OnTriggerEntered;
        _slideColliderTrigger.OnTriggerEntered -= OnTriggerEntered;
    }

    private void OnTriggerEntered(Collider2D trigger, Collider2D collider)
    {
        OnPlayerDeath?.Invoke();
        Debug.Log("Death");
    }
}
