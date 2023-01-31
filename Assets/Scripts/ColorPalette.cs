using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ColorPalette", menuName = "ColorPalette/CreateNew")]
public class ColorPalette : ScriptableObject
{
    [SerializeField]    
    private Color _playerColor;
    public Color PlayerColor => _playerColor;

    [SerializeField]
    private Color _backgroundColor;
    public Color BackgroundColor => _backgroundColor;
    [SerializeField]
    private Color _blockColor;
    public Color BlockColor => _blockColor;

    [SerializeField]
    private Color _interfaceColor;
    public Color InterfaceColor => _interfaceColor;

}
