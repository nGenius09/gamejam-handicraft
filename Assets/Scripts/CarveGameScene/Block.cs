using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public string _result;
    private void OnEnable()
    {
        _result = GetComponent<Image>().sprite.name;
    }
}
