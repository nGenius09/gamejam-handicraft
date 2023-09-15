using System;
using UnityEngine;

public class TimingStyleGame : BaseGame
{
    [SerializeField] private float destinationTime;
    protected bool isPlaying;
    protected override void StartGame()
    {
        isPlaying = true;
    }

    protected override void FinishGame()
    {
        throw new System.NotImplementedException();
    }

    protected override int GetResult()
    {
        throw new System.NotImplementedException();
    }

    protected override bool UpdateGame()
    {
        if (isPlaying && Input.GetKeyDown(KeyCode.Space))
        {
            StopAndCheckTiming();
        }
        
        return true;
    }

    protected virtual void StopAndCheckTiming()
    {
        isPlaying = false;
    }
}