using System;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    [SerializeField] private SettingPopup settingPopup;
    [SerializeField] private SuccessPopup successPopup;
    [SerializeField] private FailPopup failPopup;
    [SerializeField] private AchievementPopup achievementPopup;
    [SerializeField] private CollectionPopup collectionPopup;
    
    [SerializeField] private GameObject[] collectionObjects;

    private void Awake()
    {
        settingPopup.gameObject.SetActive(false);
        successPopup.gameObject.SetActive(false);
        failPopup.gameObject.SetActive(false);
        achievementPopup.gameObject.SetActive(false);
        collectionPopup.gameObject.SetActive(false);
        
        GameManager.Instance.SetStartGameHandler(OnStartGame);
        GameManager.Instance.SetUpdateTimeHandler(OnUpdateTime);
        GameManager.Instance.SetFinishGameHandler(OnFinishGame);
        
        RefreshCollectionObjects();
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
                    RefreshCollectionObjects();
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

    private void RefreshCollectionObjects()
    {
        var collections = AccountManager.Instance.collections;
        for (int i = 0; i < this.collectionObjects.Length; i++)
        {
            this.collectionObjects[i].SetActive(i < collections.Count);
        }
    }

    private void Update()
    {
        // var isOpen = SceneManager.sceneCount > 1;
        // startBtn.gameObject.SetActive(isOpen == false);
        // collectionBtn.gameObject.SetActive(isOpen == false);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var list = new List<BasePopup>(){successPopup, failPopup, achievementPopup, collectionPopup};

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IsActive())
                {
                    list[i].OnClickClose();
                    return;
                }
            }
            
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
        collectionPopup.Show();
    }

    public void OnClickAchievement()
    {
        achievementPopup.Show();
    }

    public void OnClickSetting()
    {
        settingPopup.Show();
    }
}