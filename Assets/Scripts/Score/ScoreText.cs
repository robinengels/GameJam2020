using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private string prepended;
    [SerializeField] private bool useForHiScore;
    [SerializeField] private TMP_Text tmpText;

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
        if(tmpText)
            tmpText.text = prepended + value;
    }
}
