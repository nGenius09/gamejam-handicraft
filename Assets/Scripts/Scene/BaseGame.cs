using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class BaseGame : MonoBehaviour
{
    [FormerlySerializedAs("gameName")] [SerializeField] private GameManager.GameMode gameMode;
    private float gameTime;
    
    protected abstract void StartGame();
    protected abstract void FinishGame();
    protected abstract int GetResult();
    protected abstract bool UpdateGame();

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