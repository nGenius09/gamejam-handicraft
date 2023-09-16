using UnityEngine;
using UnityEngine.U2D;

public class AchievementPopup : BasePopup
{
    public override void Show()
    {
        base.Show();
        
        // var achievements = AccountManager.Instance.achievements;
        // for (int i = 0; i < this.achievementObjects.Length; i++)
        // {
        //     this.achievementObjects[i].SetActive(i < achievements.Count);
        // }
    }
}