using System;
using UnityEngine;

public class TimingStyleGame : BaseGame
{
    [SerializeField] private float destinationTime;
    protected bool isPlaying;
    protected override void StartGame()
    {
        base.StartGame();
        isPlaying = true;
    }

    protected override void FinishGame(bool bSuccess = true)
    {
        base.FinishGame();
    }

    protected override int GetResult()
    {
        return base.GetResult();
    }

    protected override bool UpdateGame()
    {
        return base.UpdateGame();
    }

    protected virtual void StopAndCheckTiming()
    {
    }
}