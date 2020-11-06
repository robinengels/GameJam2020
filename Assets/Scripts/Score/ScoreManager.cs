using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float _currentScore;
    private int _hiScore;
    private static readonly string HI_SCORE = "HiScore";
    public Action<int> OnScoreChange;
    public Action<int> OnHiScoreChange;
    
    public int Score => (int) _currentScore;
    public int HiScore => _hiScore;

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

    public void ResetScore()
    {
        _currentScore = 0f;
        OnScoreChange(Score);
    }
    
    public void IncreaseScore(float value)
    {
        _currentScore += value;
        OnScoreChange(Score);
        if (_currentScore > _hiScore)
        {
            _hiScore = Score;
            OnHiScoreChange(_hiScore);
        }
    }
}