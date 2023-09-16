using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class DataManager
{
    public Dictionary<int, Data> DataTable { get => _dataTable; }
    private Dictionary<int, Data> _dataTable;

    public Dictionary<int, Achive> AchiveTable { get => _achiveTable; }
    private Dictionary<int, Achive> _achiveTable;

    public Dictionary<int, CompleteBook> CompleteBookTable { get => _completeBookTable; }
    private Dictionary<int, CompleteBook> _completeBookTable;


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
        data = Resources.Load<TextAsset>("Todolist (2)");
        JsonUtility.FromJson<AchiveLoad>(data.text).Load(out _achiveTable);
        data = Resources.Load<TextAsset>("CompleteBook");
        JsonUtility.FromJson<BookLoad>(data.text).Load(out _completeBookTable);
    }
}
