using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _currentScore;
    private int _hiScore;
    private static readonly string HI_SCORE = "HiScore";
    
    private void Awake()
    {
        _currentScore = 0;
        _hiScore = PlayerPrefs.GetInt(HI_SCORE);
    }

    private void OnDisable()
    {
        Save();
        _currentScore = 0;
    }

    private void Save()
    {
        PlayerPrefs.SetInt(HI_SCORE, _hiScore);
    }

    public void IncreaseScore(int value)
    {
        _currentScore += value;
        if (_currentScore > _hiScore)
            _hiScore = _currentScore;
    }
}