using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private string prepended;
    [SerializeField] private bool useForHiScore;
    [SerializeField] private TMP_Text _tmpText;

    
    private void Awake()
    {
    }

    private void OnEnable()
    {
        if (useForHiScore)
        {
            UpdateText(GameManager.Instance.ScoreManager.HiScore);
            GameManager.Instance.ScoreManager.OnHiScoreChange += UpdateText;
        }
        else
        {
            UpdateText(GameManager.Instance.ScoreManager.Score);
            GameManager.Instance.ScoreManager.OnScoreChange += UpdateText;
        }
    }

    private void OnDestroy()
    {
        if(useForHiScore)
        {
            if (!(GameManager.Instance.ScoreManager is null))
                GameManager.Instance.ScoreManager.OnHiScoreChange -= UpdateText;
        }
        else if (!(GameManager.Instance.ScoreManager is null))
            GameManager.Instance.ScoreManager.OnScoreChange -= UpdateText;
    }

    private void UpdateText(int value)
    {
        _tmpText.text = prepended + value;
    }
}
