using System.Collections.Generic;

public class AccountManager
{
    public static AccountManager Instance { get; } = new AccountManager();

    public int reputation { get; private set; }
    public List<int> collections { get; private set; } = new List<int>();

    public void AddReputation(int reputation)
    {
        
    }

    public void AddCollection(int collection)
    {
        // 최신이 앞으로
        collections.Insert(0, collection);
    }

    public void Load()
    {
        
    }

    public void Save()
    {
        
    }
}