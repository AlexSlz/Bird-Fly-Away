using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class Difficulty : MonoBehaviour
{
    private Spawner _spawner;
    private Score _score;

    [SerializeField]
    private int _maxDifficultyMultiplier = 100;
    private float _currentMultiplier = 0;
    private float _amountBeforeDowngrading;

    private void Start()
    {
        _spawner = GetComponent<Spawner>();
        _score = FindObjectOfType<Score>();
        _score.OnScoreChanged += ChangeDifficulty;
    }

    private void ChangeDifficulty(int score)
    {
        if(_currentMultiplier < _maxDifficultyMultiplier && _amountBeforeDowngrading == 0)
        {
            _currentMultiplier++;
            _spawner.BlockSpeed += (_spawner.MaxSpeed - _spawner.MinSpeed) / _maxDifficultyMultiplier;
            _spawner.GapSize -= (_spawner.MaxGapSize - _spawner.MinGapSize) / _maxDifficultyMultiplier;
        }
        else if(_currentMultiplier >= _maxDifficultyMultiplier - 1 && _amountBeforeDowngrading == 0)
        {
            _amountBeforeDowngrading = Random.Range(3, 9);
        }
        else if (_amountBeforeDowngrading != 0)
        {
            _amountBeforeDowngrading--;
            _currentMultiplier = 0;

            _spawner.BlockSpeed -= 0.5f;
            _spawner.GapSize += 0.5f;
        }
    }

}
