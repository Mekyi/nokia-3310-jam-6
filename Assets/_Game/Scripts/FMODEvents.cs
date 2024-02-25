using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("SFX")]

    [field: SerializeField]
    public EventReference Jump { get; private set; }

    [field: SerializeField]
    public EventReference GameOver { get; private set; }

    [field: SerializeField]
    public EventReference MainMenu { get; private set; }
    
    public static FMODEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        Instance = this;
    }
}