using UnityEngine;
using UnityEngine.UI;

// This is a workaround for the GameOver scene since it being loaded additively causes
// problems with the SingletonMb pattern.
public class GameOverMenuButtonsInitializer : MonoBehaviour
{
    [SerializeField] private Button playAgain;
    [SerializeField] private Button quitToMenu;

    private void Awake()
    {
        playAgain.onClick.AddListener(GameManager.Instance.NewGame);
        quitToMenu.onClick.AddListener(GameManager.Instance.MainMenu);
    }
}
