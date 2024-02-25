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
        
        var newPosition = new Vector3(position.x, position.y, position.z) + Vector3.left * _speed;

        transform.position = newPosition;
    }
}
