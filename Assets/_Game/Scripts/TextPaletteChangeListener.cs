using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextPaletteChangeListener : MonoBehaviour
{
    private TextMeshProUGUI _uiText;
    
    private void OnEnable()
    {
        _uiText = GetComponent<TextMeshProUGUI>();
        ColorPaletteSwapper.OnColorPaletteChanged += OnColorPaletteChanged;
        _uiText.color = ColorPaletteSwapper.Instance.CurrentColorPalette?.SecondaryColor ?? _uiText.color;
    }
    
    private void OnDisable()
    {
        ColorPaletteSwapper.OnColorPaletteChanged -= OnColorPaletteChanged;
    }

    private void OnColorPaletteChanged(ColorPalette colorPalette)
    {
        _uiText.color = colorPalette.SecondaryColor;
    }
}
