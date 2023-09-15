using System;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;
    private int currentIndex;
    private void Awake()
    {
        GoToNextPage();
    }

    private void GoToNextPage()
    {
        if (currentIndex >= pages.Length)
        {
            GameManager.Instance.LoadLobby();
            return;
        }
        
        foreach (var page in pages)
        {
            page.SetActive(false);
        }
        
        pages[currentIndex].SetActive(true);
        currentIndex++;
    }

    public void OnClickGoToLobby()
    {
        GoToNextPage();
    }
}