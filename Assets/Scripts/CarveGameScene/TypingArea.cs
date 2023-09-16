using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class TypingArea : MonoBehaviour
{
    private RectTransform _rect;
    [SerializeField]
    private Image[] _image;
    [SerializeField]
    private TextMeshProUGUI[] _text;

    [SerializeField]
    private Sprite _sprite;

    public void Init(Vector2 pos, string str)
    {
        _rect = GetComponent<RectTransform>();
        
        _rect.anchoredPosition = pos;
        for (int i = 0; i < 5; ++i)
        {
            _text[i].text = str[i].ToString();
            _image[i].gameObject.SetActive(true);
            _image[i].sprite = _sprite;
        }
    }

    public void MoveToTargetPos(Vector2 targetPos, float time = 0.5f)
    {
        _rect.DOAnchorPos(targetPos, time).SetEase(Ease.InCubic);
    }

    public void SetImage(int num)
    {
        _text[num].text = string.Empty;
        _image[num].gameObject.SetActive(false);
    }
}
