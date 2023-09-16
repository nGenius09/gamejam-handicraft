using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Dictionary<int, Data> DataTable { get => _dataTable; }
    private Dictionary<int, Data> _dataTable;

    public static DataManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Init();
        }
        else
            Destroy(gameObject);
    }

    private void Init()
    {
        DontDestroyOnLoad(gameObject);
        TextAsset data = Resources.Load<TextAsset>("minigame");
        JsonUtility.FromJson<DataLoad>(data.text).Load(out _dataTable);
        Debug.Log("x");
    }
}
