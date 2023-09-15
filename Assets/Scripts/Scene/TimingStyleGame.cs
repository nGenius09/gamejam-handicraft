using System;
using UnityEngine;

public class TimingStyleGame : BaseGame
{
    private float destinationTime;
    protected override void StartGame()
    {
        
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckTiming();
        }
        
        return true;
    }

    protected virtual void CheckTiming()
    {
        
    }
}