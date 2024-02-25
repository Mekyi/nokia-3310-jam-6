using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    [field: SerializeField]
    public Transform LeftPosition {get; private set; }
    
    [field: SerializeField]
    public Transform RightPosition {get; private set; }

    private BoxCollider2D _tileTrigger;

    private void Awake()
    {
        _tileTrigger = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        bool isPastScreen = CheckIfPastScreen();

        if (isPastScreen)
        {
            GroundTileRepeater.Instance.RepeatTile(this);
        }
    }

    private bool CheckIfPastScreen()
    {
        return GroundTileRepeater.Instance.PastScreenTransform.position.x > RightPosition.position.x;
    }
}
