using UnityEngine;

public class SettingResetPopup : BasePopup
{
    public void OnClickYes()
    {
        GameManager.Instance.ResetAll();
    }
}