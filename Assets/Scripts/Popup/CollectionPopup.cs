using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CollectionPopup : BasePopup
{
    [SerializeField] private SpriteAtlas atlas;
    [SerializeField] private GameObject bookSlotObj;
    [SerializeField] private RectTransform pivot;

    private List<BookSlot> bookSlots = new List<BookSlot>();

    public override void Show()
    {
        base.Show();
        
        var collections = AccountManager.Instance.collections;
        for (int i = 0; i < collections.Count; i++)
        {
            if (i >= bookSlots.Count)
            {
                var obj = Instantiate(bookSlotObj, pivot);
                var slot = obj.GetComponent<BookSlot>();
                bookSlots.Add(slot);
            }
            
            bookSlots[i].gameObject.SetActive(true);
            bookSlots[i].Setup(collections[i], atlas.GetSprite($"Result{collections[i]}"));
        }

        for (int i = collections.Count; i < bookSlots.Count; i++)
        {
            bookSlots[i].gameObject.SetActive(false);
        }
    }
}