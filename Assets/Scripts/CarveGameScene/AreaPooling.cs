using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPooling : MonoBehaviour
{
    private TypingArea _curArea;
    private Queue<TypingArea> _activeArea = new Queue<TypingArea>(6);

    private Queue<TypingArea> _offArea = new Queue<TypingArea>(6);

    private readonly Vector2 _curAreaPos = new Vector2(0, -200);
    private readonly Vector2 _nextAreaPos = new Vector2(1360, -200);

    public void Init(string str)
    {
        for (int i = 0; i < transform.childCount; ++i)
            _offArea.Enqueue(transform.GetChild(i).GetComponent<TypingArea>());

        ActiveArea(str);
        _activeArea.Peek().MoveToTargetPos(_curAreaPos);
    }

    private void ActiveArea(string str)
    {
        TypingArea temp;
        temp = _offArea.Dequeue();
        temp.Init(_nextAreaPos, str); //str�� �´� �̹��� �ֱ�
        temp.gameObject.SetActive(true);
        _activeArea.Enqueue(temp);
        temp.MoveToTargetPos(_curAreaPos);
        _curArea = temp;
    }

    public void ChangeArea(string str)
    {
        TypingArea temp = _activeArea.Dequeue();
        temp.gameObject.SetActive(false);
        _offArea.Enqueue(temp);
        ActiveArea(str);
    }

    public void ActiveImageEffect(int point)
    {
        _curArea.SetOffImage(point);
    }

    public void Clear()
    {
        TypingArea temp = _activeArea.Dequeue();
        temp.gameObject.SetActive(false);
        _offArea.Enqueue(temp);
    }
}
