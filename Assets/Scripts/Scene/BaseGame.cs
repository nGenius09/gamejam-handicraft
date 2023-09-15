using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class BaseGame : MonoBehaviour
{
    [FormerlySerializedAs("gameName")] [SerializeField] private GameManager.GameMode gameMode;
    [SerializeField] private Button finishBtn;

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
        UpdateGame();
    }
    
    public void OnClickFinish()
    {
        GameManager.Instance.FinishGame(gameMode);
    }
}