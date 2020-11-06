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
        LoadScene("Level");
    }

    public void PlayAgain()
    {
        NewGame();
    }

    public void GameOver()
    {
        LoadScene("GameOver");
    }

    public void MainMenu()
    {
        LoadScene("MainMenu");
    }

    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }
}
