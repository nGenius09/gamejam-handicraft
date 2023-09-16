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
    public Action<bool, GameMode, GameMode> OnFinishGame { get; private set; }

    ~GameManager()
    {
        OnStartGame = null;
        OnUpdateTime = null;
    }
    
    public void ResetAll()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("0.Intro");
        CurrentMode = GameMode.None;
    }

    public void LoadLobby()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("1.Lobby");
        CurrentMode = GameMode.None;
    }

    public void StartGame(GameMode gameMode)
    {
        SceneManager.LoadScene($"2.{gameMode}", LoadSceneMode.Additive);
        CurrentMode = gameMode;
    }
    
    public void FinishGame(GameMode gameMode, GameMode nextMode)
    {
        var op = SceneManager.UnloadSceneAsync($"2.{gameMode}");
        if (nextMode != GameMode.None)
        {
            StartGame(nextMode);
        }
        else
        {
            CurrentMode = GameMode.None;
        }
    }
    
    public void SetStartGameHandler(Action onStartGame)
    {
        this.OnStartGame += onStartGame;
    }

    public void SetUpdateTimeHandler(Action<float> onUpdateTime)
    {
        this.OnUpdateTime += onUpdateTime;
    }

    public void SetFinishGameHandler(Action<bool, GameMode, GameMode> onFinishGame)
    {
        this.OnFinishGame += onFinishGame;
    }
}

public static class GameModeExtension
{
    public static bool IsFinalMode(this GameManager.GameMode gameMode)
    {
        return gameMode == GameManager.GameMode.Game3;
    }
}