using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button collectionBtn;
    [SerializeField] private TimeBar timeBar;
    [SerializeField] private GameObject settingPopup;

    private void Awake()
    {
        timeBar.gameObject.SetActive(false);
        
        GameManager.Instance.SetStartGameHandler(OnStartGame);
        GameManager.Instance.SetUpdateTimeHandler(OnUpdateTime);
        GameManager.Instance.SetFinishGameHandler(OnFinishGame);
    }

    private void OnStartGame()
    {
        timeBar.gameObject.SetActive(true);
    }

    private void OnUpdateTime(float rate)
    {
        timeBar.SetFillAmount(rate);
    }
    
    private void OnFinishGame()
    {
        timeBar.gameObject.SetActive(false);
    }

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