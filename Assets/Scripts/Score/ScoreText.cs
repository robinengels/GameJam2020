using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TMP_Text _tmpText;
    
    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        UpdateText(GameManager.Instance.ScoreManager.Score);
        GameManager.Instance.ScoreManager.OnScoreChange += UpdateText;
    }

    private void UpdateText(int value)
    {
        _tmpText.text = value.ToString();
    }
}
