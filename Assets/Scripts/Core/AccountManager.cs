using System.Collections.Generic;
using System.Diagnostics;

public class AccountManager
{
    public static AccountManager Instance { get; } = new AccountManager();

    public int gamePoint { get; private set; }
    public List<int> collections { get; private set; } = new List<int>();
    public List<int> achievements { get; private set; } = new List<int>();

    public bool hasEnding { get; private set; }

    public void ResetPoint()
    {
        this.gamePoint = 0;
    }
 
    public void AddGamePoint(int gamePoint)
    {
        this.gamePoint += gamePoint;
    }

    public void AddCollection(int id)
    {
        collections.Add(id);
    }
    
    public void AddAchievement(int id)
    {
        achievements.Add(id);
    }

    public void Load()
    {
        
    }

    public void Save()
    {
        
    }

    public void SaveEnding()
    {
        hasEnding = true;
    }
}