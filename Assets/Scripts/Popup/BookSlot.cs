using UnityEngine;
using UnityEngine.UI;

public class BookSlot : MonoBehaviour
{
    [SerializeField] private Image image;
    private int id;
    public void Setup(int id, Sprite sprite)
    {
        this.id = id;
        image.sprite = sprite;
    }
}