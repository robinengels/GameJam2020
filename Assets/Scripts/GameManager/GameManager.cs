using Pooling;
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
        ObjectPool.ResetPools();
        SceneManager.LoadScene("Scene Robin 2");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        scoreManager.ResetScore();
        NewGame();
    }

    public void GameOver()
    {
        var ui = GameObject.FindWithTag("UI");
        ui?.SetActive(false);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
