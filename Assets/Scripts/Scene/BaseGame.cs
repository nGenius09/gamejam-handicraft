using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class BaseGame : MonoBehaviour
{
    [FormerlySerializedAs("gameName")] [SerializeField] private GameManager.GameMode gameMode;
    private float gameTime;

    protected virtual void StartGame()
    {
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
        GameManager.Instance.OnUpdateTime?.Invoke(1 - gameTime / 10f);
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
        GameManager.Instance.FinishGame(gameMode);
    }
}