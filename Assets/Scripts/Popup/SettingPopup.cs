using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : BasePopup
{
    [SerializeField] private SettingResetPopup settingResetPopup;
    [SerializeField] private SettingExitPopup settingExitPopup;
    public void OnBGMValueChanged(Slider slider)
    {
        
    }
    
    public void OnSFXValueChanged(Slider slider)
    {
        
    }
    
    public void OnClickReset()
    {
        settingResetPopup.Show();
    }

    public void OnClickExit()
    {
        settingExitPopup.Show();
    }

    public override void Show()
    {
        base.Show();

        Time.timeScale = 0;
    }

    public override void OnClickClose()
    {
        if (settingResetPopup.IsActive())
        {
            settingResetPopup.OnClickClose();
            return;
        }

        if (settingExitPopup.IsActive())
        {
            settingExitPopup.OnClickClose();
            return;
        }
        
        base.OnClickClose();

        Time.timeScale = 1;
    }
}