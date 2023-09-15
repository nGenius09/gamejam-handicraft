using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public enum GameMode
    {
        None,
        Game1,
        Game2,
    }
    public static GameManager Instance { get; } = new GameManager();

    public GameMode CurrentMode { get; private set; }
    
    /// <summary>
    /// For Test
    /// </summary>
    public void LoadIntro()
    {
        SceneManager.LoadScene("0.Intro");
        CurrentMode = GameMode.None;
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene("1.Lobby");
        CurrentMode = GameMode.None;
    }

    public void StartGame(GameMode gameMode)
    {
        SceneManager.LoadScene($"2.{gameMode}", LoadSceneMode.Additive);
        CurrentMode = gameMode;
    }
    
    public void FinishGame(GameMode gameMode)
    {
        var op = SceneManager.UnloadSceneAsync($"2.{gameMode}");
        CurrentMode = GameMode.None;
    }
}