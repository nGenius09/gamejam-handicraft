using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : BasePopup
{
    [SerializeField] private SettingResetPopup settingResetPopup;
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

    public override void Show()
    {
        base.Show();

        Time.timeScale = 0;
    }

    public override void OnClickClose()
    {
        base.OnClickClose();

        Time.timeScale = 1;
    }
}