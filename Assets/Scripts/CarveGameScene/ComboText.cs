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

    public float _keepTime = 1;
    public float _curtime = 1;

    private readonly Color _red = new Color((float)0xB3 / 255, (float)0x21 / 255, (float)0x16 / 255, 1);
    private readonly Color _fade = new Color((float)0xB3 / 255, (float)0x21 / 255, (float)0x16 / 255, 0);

    public void Init()
    {
        Fade().Forget();
        _curtime = 0;
    }

    private async UniTaskVoid Fade()
    {
        while (true)
        {
            _text.color = Color.Lerp(_fade, _red, _curtime / _keepTime);
            await UniTask.DelayFrame(1, cancellationToken: _cts.Token);
            _curtime -= Time.deltaTime;
        }
    }

    public void GetInput(int combo)
    {
        if (combo == 0)
        {
            _curtime = 0;
            _text.text = $"{0} 연속..";
        }
        else
        {
            _curtime = _keepTime;
            _text.text = $"{combo} 연속!";
        }
    }

    public void Clear()
    {
        _cts.Cancel();
        _cts.Dispose();
    }
}
