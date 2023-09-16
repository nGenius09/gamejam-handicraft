using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    public enum GameMode
    {
        None,
        Game1,
        Game2,
        Game3,
    }
    public static GameManager Instance { get; } = new GameManager();

    public GameMode CurrentMode { get; private set; }
    
    public Action OnStartGame { get; private set; }
    public Action<float> OnUpdateTime { get; private set; }
    public Action OnFinishGame { get; private set; }

    ~GameManager()
    {
        OnStartGame = null;
        OnUpdateTime = null;
    }
    
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
    
    public void SetStartGameHandler(Action onStartGame)
    {
        this.OnStartGame += onStartGame;
    }

    public void SetUpdateTimeHandler(Action<float> onUpdateTime)
    {
        this.OnUpdateTime += onUpdateTime;
    }

    public void SetFinishGameHandler(Action onFinishGame)
    {
        this.OnFinishGame += onFinishGame;
    }
}