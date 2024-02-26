using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class RawImagePaletteChangeListener : MonoBehaviour
{
    [FormerlySerializedAs("_colorPaletteSwarMaterial")] [SerializeField] 
    private Material _colorPaletteSwapMaterial;
    
    private RawImage _rawImage;
    
    private static readonly int Color1 = Shader.PropertyToID("_Color1");
    private static readonly int Color2 = Shader.PropertyToID("_Color2");

    private void OnEnable()
    {
        _rawImage = GetComponent<RawImage>();
        _rawImage.material = _colorPaletteSwapMaterial;
        ColorPaletteSwapper.OnColorPaletteChanged += OnColorPaletteChanged;
    }
    private void OnDisable()
    {
        ColorPaletteSwapper.OnColorPaletteChanged -= OnColorPaletteChanged;
    }
    
    private void OnColorPaletteChanged(ColorPalette colorPalette)
    {
        _rawImage.material.SetColor(Color1, colorPalette.PrimaryColor);
        _rawImage.material.SetColor(Color2, colorPalette.SecondaryColor);
    }
}
