using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private Score _score;
    private TMP_Text _scoreView;

    private void Awake()
    {
        _scoreView = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if(_score != null)
        _score.OnScoreChanged += SetScore;
    }
    private void SetScore(int score)
    {
        _scoreView.text = $"{score}";
    }
}
