using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPaletteSwapper : MonoBehaviour
{
    public static ColorPaletteSwapper Instance { get; private set; }

    public static event Action<ColorPalette> OnColorPaletteChanged;

    public ColorPalette CurrentColorPalette { get; private set; }

    [SerializeField] 
    private List<ColorPalette> _colorPalettes = new List<ColorPalette>();
    
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
        
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeColorPalette(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeColorPalette(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeColorPalette(2);
        }
    }

    private void ChangeColorPalette(int index)
    {
        var colorPalette = _colorPalettes[Mathf.Clamp(index, 0, _colorPalettes.Count)];
        CurrentColorPalette = colorPalette;
        OnColorPaletteChanged?.Invoke(colorPalette);
    }
}
