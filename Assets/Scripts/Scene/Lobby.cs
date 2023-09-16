using System;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    [SerializeField] private SettingPopup settingPopup;
    [SerializeField] private SuccessPopup successPopup;
    [SerializeField] private FailPopup failPopup;

    private void Awake()
    {
        settingPopup.gameObject.SetActive(false);
        successPopup.gameObject.SetActive(false);
        failPopup.gameObject.SetActive(false);
        
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
    
    private void OnFinishGame(bool bSuccess, GameManager.GameMode current, GameManager.GameMode next)
    {
        if (bSuccess)
        {
            if (current.IsFinalMode())
            {
                successPopup.Show(() =>
                {
                    GameManager.Instance.FinishGame(current, GameManager.GameMode.None);
                });
            }
            else
            {
                GameManager.Instance.FinishGame(current, next);
            }
        }
        else
        {
            failPopup.Show(() =>
            {
                GameManager.Instance.FinishGame(current, GameManager.GameMode.None);
            });
        }
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