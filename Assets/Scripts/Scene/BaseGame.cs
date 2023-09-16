using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class BaseGame : MonoBehaviour
{
    [SerializeField] private GameManager.GameMode gameMode;
    [SerializeField] private GameManager.GameMode nextMode;
    [SerializeField] protected int limitTime;
    protected float gameTime { get; private set; }

    protected void SetTime(float gameTime)
    {
        this.gameTime = gameTime;
    }

    protected virtual void StartGame()
    {
        gameTime = 0;
        GameManager.Instance.OnStartGame?.Invoke();
    }

    protected virtual void FinishGame()
    {
        GameManager.Instance.OnFinishGame?.Invoke();
    }

    protected virtual int GetResult()
    {
        return 0;
    }

    protected virtual bool UpdateGame()
    {
        GameManager.Instance.OnUpdateTime?.Invoke(1 - gameTime / limitTime);
        return true;
    }

    void Awake()
    {
        StartGame();
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        
        UpdateGame();
    }
    
    /// <summary>
    /// For Test
    /// </summary>
    public void OnClickFinish()
    {
        GameManager.Instance.FinishGame(gameMode, nextMode);
    }
}