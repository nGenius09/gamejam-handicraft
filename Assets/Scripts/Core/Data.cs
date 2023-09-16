using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Data
{
    public int ID;
    public int Type;
    public int Score;
    public int TimeLimit;
    public int LetterTypeCount;
    public int LetterLength;

    public void Load(out Dictionary<int, Data> data)
    {
        data = new Dictionary<int, Data>();

    }
}

[Serializable]
public class DataLoad
{
    public List<Data> data;

    public void Load(out Dictionary<int, Data> dic)
    {
        dic = new Dictionary<int, Data>();
        for (int i = 0; i < data.Count; ++i)
            dic.Add(data[i].ID, data[i]);
    }
}
