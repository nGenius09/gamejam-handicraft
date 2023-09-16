using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ComboText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    private CancellationTokenSource _cts = new CancellationTokenSource();

    private float _keepTime = 1;
    private float _curtime = 1;

    public void Init()
    {
        Fade().Forget();
    }

    private async UniTaskVoid Fade()
    {
        while (true)
        {
            _text.color = Color.Lerp(Color.clear, Color.black, _curtime / _keepTime);
            await UniTask.DelayFrame(1, cancellationToken: _cts.Token);
            _curtime -= Time.deltaTime;
        }
    }

    public void GetInput(int combo)
    {
        if (combo == 0)
        {
            _curtime = 0;
            _text.text = $"Combo: {0}";
        }
        else
        {
            _curtime = _keepTime;
            _text.text = $"Combo: {combo}";
        }
    }

    public void Clear()
    {
        _cts.Cancel();
        _cts.Dispose();
    }
}
