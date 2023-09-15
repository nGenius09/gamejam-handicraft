using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button collectionBtn;
    [SerializeField] private GameObject settingPopup;
    
    private void Update()
    {
        var isOpen = SceneManager.sceneCount > 1;
        startBtn.gameObject.SetActive(isOpen == false);
        collectionBtn.gameObject.SetActive(isOpen == false);
    }

    public void OnClickStart()
    {
        GameManager.Instance.StartGame(GameManager.GameMode.Game2);
    }

    public void OnClickSetting()
    {
        settingPopup.SetActive(true);
    }
}