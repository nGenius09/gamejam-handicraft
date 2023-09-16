using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    public Dictionary<int, Data> DataTable { get => _dataTable; }
    private Dictionary<int, Data> _dataTable;

    public static DataManager Instance
    {
        get {
            if (d_instance == null)
                d_instance = new DataManager();

            return d_instance;
        }
    }

    private static DataManager d_instance;

    public DataManager()
    {
        TextAsset data = Resources.Load<TextAsset>("minigame");
        JsonUtility.FromJson<DataLoad>(data.text).Load(out _dataTable);
    }
}
