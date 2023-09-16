using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button collectionBtn;
    [SerializeField] private SettingPopup settingPopup;

    private void Awake()
    {
        GameManager.Instance.SetStartGameHandler(OnStartGame);
        GameManager.Instance.SetUpdateTimeHandler(OnUpdateTime);
        GameManager.Instance.SetFinishGameHandler(OnFinishGame);
    }

    private void OnStartGame()
    {
        
    }

    private void OnUpdateTime(float rate)
    {
        
    }
    
    private void OnFinishGame()
    {
        
    }

    private void Update()
    {
        // var isOpen = SceneManager.sceneCount > 1;
        // startBtn.gameObject.SetActive(isOpen == false);
        // collectionBtn.gameObject.SetActive(isOpen == false);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!settingPopup.IsActive())
            {
                settingPopup.Show();
            }
            else
            {
                settingPopup.OnClickClose();
            }
        }
    }

    public void OnClickStart()
    {
        GameManager.Instance.StartGame(GameManager.GameMode.Game1);
    }

    public void OnClickCollection()
    {
        
    }

    public void OnClickAchievement()
    {
        
    }

    public void OnClickSetting()
    {
        settingPopup.Show();
    }
}