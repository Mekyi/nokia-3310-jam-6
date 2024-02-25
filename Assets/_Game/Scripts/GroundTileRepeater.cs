using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTileRepeater : MonoBehaviour
{
    public static GroundTileRepeater Instance { get; private set; }
    
    [field: SerializeField]
    public Transform PastScreenTransform { get; private set; }
    
    [SerializeField] 
    private GroundTile _firstTile;
    
    [SerializeField] 
    private GroundTile _secondTile;

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
    }
    
    private void Start()
    {
        MoveTileAfterTargetTile(_secondTile, _firstTile);
    }

    public void RepeatTile(GroundTile groundTile)
    {
        var isFirstTile = groundTile == _firstTile;

        if (isFirstTile)
        {
            MoveTileAfterTargetTile(_firstTile, _secondTile);
        }
        else
        {
            MoveTileAfterTargetTile(_secondTile, _firstTile);
        }
    }

    private void MoveTileAfterTargetTile(GroundTile tileToMove, GroundTile otherTile)
    {
        var distance =  otherTile.LeftPosition.position - otherTile.RightPosition.position;
        var absDistance = new Vector3(Mathf.Abs(distance.x), distance.y, distance.z);

        tileToMove.transform.position = otherTile.transform.position + absDistance;
    }
}
