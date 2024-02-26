using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Palette", menuName = "Scriptable Objects/New Color Palette")]
public class ColorPalette : ScriptableObject
{
    [field: SerializeField] 
    public Color PrimaryColor { get; private set; }
    
    [field: SerializeField] 
    public Color SecondaryColor { get; private set; }
}
