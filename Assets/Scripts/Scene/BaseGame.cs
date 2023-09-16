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
    public bool IsPlaying { get; private set; }
    protected Data _curGameData;

    protected void SetTime(float gameTime)
    {
        this.gameTime = gameTime;
    }

    protected virtual void StartGame()
    {
        gameTime = 0;
        IsPlaying = true;
        GameManager.Instance.OnStartGame?.Invoke();
        
        if (gameMode != GameManager.GameMode.None)
        {
            int datanumber = AccountManager.Instance.collections.Count > 5 ?
        UnityEngine.Random.Range(0, 6) : AccountManager.Instance.collections.Count;
            _curGameData = DataManager.Instance.DataTable[datanumber * 3 + (int)(gameMode) - 1];
        }
    }

    protected virtual void FinishGame(bool bSuccess = true)
    {
        IsPlaying = false;
        
        if (bSuccess)
        {
            AccountManager.Instance.AddGamePoint(GetResult());
        }
        
        GameManager.Instance.OnFinishGame?.Invoke(bSuccess, gameMode, nextMode);
    }

    protected virtual int GetResult()
    {
        return 0;
    }

    protected virtual bool UpdateGame()
    {
        gameTime += Time.deltaTime;
        GameManager.Instance.OnUpdateTime?.Invoke(1 - gameTime / limitTime);
        return true;
    }

    void Awake()
    {
        StartGame();
    }

    private void Update()
    {
        if (IsPlaying)
        {
            UpdateGame();
        }
    }
    
    /// <summary>
    /// For Test
    /// </summary>
    public void OnClickFinish()
    {
        GameManager.Instance.FinishGame(gameMode, gameMode);
    }
}