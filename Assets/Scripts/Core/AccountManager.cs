using System.Collections.Generic;

public class AccountManager
{
    public static AccountManager Instance { get; } = new AccountManager();

    public int gamePoint { get; private set; }
    public List<int> collections { get; private set; } = new List<int>();

    public void AddGamePoint(int gamePoint)
    {
        this.gamePoint += gamePoint;
    }

    public void AddCollection(int id)
    {
        collections.Add(id);
    }

    public void Load()
    {
        
    }

    public void Save()
    {
        
    }
}