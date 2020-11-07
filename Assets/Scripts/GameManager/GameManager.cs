using IPL;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMb<GameManager>
{
    [SerializeField] private ScoreManager scoreManager;
    public ScoreManager ScoreManager => scoreManager;
    
    protected override void Initialize()
    {

    }

    protected override void Cleanup()
    {
    
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void PlayAgain()
    {
        scoreManager.ResetScore();
        NewGame();
    }

    public void GameOver()
    {
        var ui = FindObjectOfType<Canvas>()?.gameObject;
        if(ui) ui.SetActive(false);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
