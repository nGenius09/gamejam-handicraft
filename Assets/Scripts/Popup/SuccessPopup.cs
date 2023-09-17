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

        var id = Mathf.Clamp(AccountManager.Instance.gamePoint / 10 + 1, 1, 6);

        image.sprite = atlas.GetSprite($"Result{id}");
        comment.text = DataManager.Instance.GetCompleteBook(id).Txt;
        
        AccountManager.Instance.AddCollection(id);
    }
}