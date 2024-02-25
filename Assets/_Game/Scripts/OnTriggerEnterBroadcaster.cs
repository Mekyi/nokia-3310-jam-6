using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class OnTriggerEnterBroadcaster : MonoBehaviour
{
    public Action<Collider2D, Collider2D> OnTriggerEntered;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEntered?.Invoke(GetComponent<Collider2D>(), other);
    }
}
