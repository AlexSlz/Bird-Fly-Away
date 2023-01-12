using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public delegate void EventDelegate(int score);

    public event EventDelegate OnScoreChanged;

    private int _score = 0;
    private static Score _instance;
    public static Score Instance { get { return _instance; } }

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

    public void Add(int count)
    {
        _score += count;
        Debug.Log(_score);
        OnScoreChanged?.Invoke(_score);
    }
}
