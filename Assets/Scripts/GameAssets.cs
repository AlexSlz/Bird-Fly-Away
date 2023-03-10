using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _instance;
    public static GameAssets Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        Camera.main.backgroundColor = ColorPalette.BackgroundColor;
    }

    [SerializeField]
    private ColorPalette _colorPalette;
    public ColorPalette ColorPalette => _colorPalette;

}
