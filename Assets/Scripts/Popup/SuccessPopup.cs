using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SuccessPopup : FinishActionPopup
{
    [SerializeField] private SpriteAtlas atlas;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI comment;

    public override void Show()
    {
        base.Show();

        var id = Mathf.Clamp(AccountManager.Instance.gamePoint / 10 + 1, 1, 5);

        image.sprite = atlas.GetSprite($"Result{id}");
        
        var id2 = Mathf.Clamp(AccountManager.Instance.gamePoint / 3 + 1, 1, 35);
        comment.text = DataManager.Instance.GetCompleteBook(id2).Txt;
        
        AccountManager.Instance.AddCollection(id);
    }
}