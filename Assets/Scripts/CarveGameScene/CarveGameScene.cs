using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TMPro;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CarveGameScene : MonoBehaviour
{
    private readonly StringBuilder _startconsonant = new StringBuilder("ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ");
    private readonly StringBuilder _vowel = new StringBuilder("ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ");
    private readonly StringBuilder _endconsonant = new StringBuilder("ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ");
    private readonly StringBuilder _excludedLetter = new StringBuilder("ㅃㅉㄸㄲㅆㅒㅖㅘㅙㅚㅝㅞㅟㅢㄳㄵㄶㄺㄻㄼㄽㄾㄿㅀㅄ");

    private readonly ushort _koreanStart = 0xAC00;
    private readonly ushort _koreanEnd = 0xD79F;
    private readonly ushort _numberStart = 48;
    private readonly ushort _numberEnd = 57;

    private Dictionary<KeyCode, char> _keyCodeNCharPair = new Dictionary<KeyCode, char>();
    
    private readonly List<ushort> _includeLetter = new List<ushort>() { 39, 44, 45, 46, 47, 59, 61, 91, 92, 93, 96 }; 

    System.Random _rand = new System.Random();

    private Dictionary<char, int> _appearLetterSearchChar = new Dictionary<char, int>();

    private StringBuilder _nonDuplicateString;
    //List<KeyCode> _nonDuplicateEnum = new List<KeyCode>();
    private int _stringPointer = 0;

    [SerializeField]
    private AreaPooling _area;

    [SerializeField]
    private UnityEngine.UI.Slider _timeLimit;

    private CancellationTokenSource _cts = new CancellationTokenSource();

    public float time = 60;

    private void Start()
    {
        Divide_Letter(new StringBuilder("씌쑤뺴쨔꼐뛰"));
        CheckDupllicate(20, 4);
        _appearLetterSearchChar.Clear();
        MakeKeyCodeNCharTable();
        _timeLimit.maxValue = time;

        StringBuilder str = new StringBuilder(5);

        for (int i = 0; i < 5; ++i)
            str.Append(_nonDuplicateString[i]);

        _area.Init(str.ToString());

        this.UpdateAsObservable().Subscribe(_ => CheckInput());
        TickTime().Forget();
    }

    private async UniTaskVoid TickTime()
    {
        while (time > 0)
        {
            //�ɼ�â active����?
            await UniTask.DelayFrame(1, cancellationToken: _cts.Token);
            time -= Time.deltaTime;
            _timeLimit.value = time;
        }
    }

    private async UniTaskVoid ShakeSlider(float time = 0.5f, float amount = 10)
    {
        RectTransform t = _timeLimit.GetComponent<RectTransform>();
        Vector2 startpos = t.anchoredPosition;
        Debug.Log(startpos);
        while (time > 0.0f)
        {
            t.anchoredPosition = startpos + new Vector2(UnityEngine.Random.Range(-amount, amount),
                UnityEngine.Random.Range(-amount, amount));
            await UniTask.DelayFrame(1, cancellationToken: _cts.Token);
            t.anchoredPosition = startpos;
            time -= Time.deltaTime;
        }
    }

    private void CheckInput()
    {
        //Input.inputString
        foreach(KeyCode key in _keyCodeNCharPair.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                if (_nonDuplicateString[_stringPointer] == _keyCodeNCharPair[key])
                {
                    Debug.Log($"input is {_keyCodeNCharPair[key]}, Success");
                    _area.ActiveImageEffect(_stringPointer % 5);
                    ++_stringPointer;

                    if (_nonDuplicateString.Length == _stringPointer)
                    {
                        //���� ��!
                        _area.Clear();
                    }

                    else if (_stringPointer % 5 == 0)
                    {
                        StringBuilder stringBuilder = new StringBuilder(5);
                        for (int i = 0; i < 5; ++i)
                        {
                            stringBuilder.Append(_nonDuplicateString[i + _stringPointer]);
                        }
                        _area.ChangeArea(stringBuilder.ToString());
                    }
                }

                else
                {
                    ShakeSlider().Forget();
                    time -= 5;
                    Debug.Log($"input is {_keyCodeNCharPair[key]} curLetter is {_nonDuplicateString[_stringPointer]}, Fail");
                    //����!
                }
            }
        }
    }

    private void CheckDupllicate(int letterLen, int typeCount)
    {
        _nonDuplicateString = new StringBuilder(letterLen);
        StringBuilder temp = new StringBuilder(_appearLetterSearchChar.Count);
        List<int> randType = new List<int>();
        int randomNum;
        char curLetter = '!';
        short countDuplication = 1;

        foreach(var Key in _appearLetterSearchChar.Keys)
            temp.Append(Key);

        if (typeCount < temp.Length)
        {
            while (randType.Count < typeCount)
            {
                randomNum = _rand.Next(0, temp.Length);
                if (randType.IndexOf(randomNum) == -1)
                    randType.Add(randomNum);
            }
        }

        else
        {
            for (int i = 0; i < temp.Length; ++i)
                randType.Add(i);
        }

        for (int i = 0; i < letterLen; ++i)
        {
            randomNum = _rand.Next(0, randType.Count);
    
            if (curLetter == temp[randomNum])
            {
                if (countDuplication > 1)
                {
                    --i;
                    continue;
                }

                ++countDuplication;
            }

            else
            {
                countDuplication = 1;
                curLetter = temp[randomNum];
            }

            _nonDuplicateString.Append(curLetter);
        }
        Debug.Log($"Letter is {_nonDuplicateString.ToString()}");

    }

    private void Divide_Letter(string str)
    {
        StringBuilder stringBuilder = new StringBuilder(str);
        Divide_Letter(stringBuilder);
    }

    private void Divide_Letter(StringBuilder str)
    {
        int i;
        int letterStart, letterMid, letterEnd;
        ushort tempUnicode;
        //StringBuilder dividedLetter = new StringBuilder(str.Length);
        
        for (i = 0; i < str.Length; ++i)
        {
            tempUnicode = Convert.ToUInt16(str[i]);
            if (tempUnicode < _koreanStart || tempUnicode > _koreanEnd)
            {
                if ((tempUnicode >= _numberStart && tempUnicode <= _numberEnd))
                    _appearLetterSearchChar.TryAdd(str[i], _appearLetterSearchChar.Count);
                
                continue;
            }
            tempUnicode -= _koreanStart;
            letterStart = tempUnicode / (21 * 28);
            //dividedLetter.Append(_startconsonant[letterStart]);
            _appearLetterSearchChar.TryAdd(_startconsonant[letterStart], _appearLetterSearchChar.Count);

            tempUnicode %= (21 * 28);
            letterMid = tempUnicode / 28;
            //dividedLetter.Append(_vowel[letterMid]);
            _appearLetterSearchChar.TryAdd(_vowel[letterMid], _appearLetterSearchChar.Count);

            tempUnicode %= 28;
            letterEnd = tempUnicode;

            if (letterEnd != 0)
            {
                //dividedLetter.Append(_endconsonant[letterEnd - 1]);
                _appearLetterSearchChar.TryAdd(_endconsonant[letterEnd - 1], _appearLetterSearchChar.Count);
            }
        }

        for (i = 0; i < _excludedLetter.Length; ++i)
        {
            _appearLetterSearchChar.Remove(_excludedLetter[i]);
        }
    }

    public void Clear()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    private void MakeKeyCodeNCharTable()
    {
        _keyCodeNCharPair.Add(KeyCode.Q, 'ㅂ');
        _keyCodeNCharPair.Add(KeyCode.W, 'ㅈ');
        _keyCodeNCharPair.Add(KeyCode.E, 'ㄷ');
        _keyCodeNCharPair.Add(KeyCode.R, 'ㄱ');
        _keyCodeNCharPair.Add(KeyCode.T, 'ㅅ');
        _keyCodeNCharPair.Add(KeyCode.Y, 'ㅛ');
        _keyCodeNCharPair.Add(KeyCode.U, 'ㅕ');
        _keyCodeNCharPair.Add(KeyCode.I, 'ㅑ');
        _keyCodeNCharPair.Add(KeyCode.O, 'ㅐ');
        _keyCodeNCharPair.Add(KeyCode.P, 'ㅔ');
        _keyCodeNCharPair.Add(KeyCode.A, 'ㅁ');
        _keyCodeNCharPair.Add(KeyCode.S, 'ㄴ');
        _keyCodeNCharPair.Add(KeyCode.D, 'ㅇ');
        _keyCodeNCharPair.Add(KeyCode.F, 'ㄹ');
        _keyCodeNCharPair.Add(KeyCode.G, 'ㅎ');
        _keyCodeNCharPair.Add(KeyCode.H, 'ㅗ');
        _keyCodeNCharPair.Add(KeyCode.J, 'ㅓ');
        _keyCodeNCharPair.Add(KeyCode.K, 'ㅏ');
        _keyCodeNCharPair.Add(KeyCode.L, 'ㅣ');
        _keyCodeNCharPair.Add(KeyCode.Z, 'ㅋ');
        _keyCodeNCharPair.Add(KeyCode.X, 'ㅌ');
        _keyCodeNCharPair.Add(KeyCode.C, 'ㅊ');
        _keyCodeNCharPair.Add(KeyCode.V, 'ㅍ');
        _keyCodeNCharPair.Add(KeyCode.B, 'ㅠ');
        _keyCodeNCharPair.Add(KeyCode.N, 'ㅜ');
        _keyCodeNCharPair.Add(KeyCode.M, 'ㅡ');
        _keyCodeNCharPair.Add(KeyCode.Alpha1, '1');
        _keyCodeNCharPair.Add(KeyCode.Alpha2, '2');
        _keyCodeNCharPair.Add(KeyCode.Alpha3, '3');
        _keyCodeNCharPair.Add(KeyCode.Alpha4, '4');
        _keyCodeNCharPair.Add(KeyCode.Alpha5, '5');
        _keyCodeNCharPair.Add(KeyCode.Alpha6, '6');
        _keyCodeNCharPair.Add(KeyCode.Alpha7, '7');
        _keyCodeNCharPair.Add(KeyCode.Alpha8, '8');
        _keyCodeNCharPair.Add(KeyCode.Alpha9, '9');
        _keyCodeNCharPair.Add(KeyCode.Alpha0, '0');
    }
    /*
    private char InputUnity()
    {
        switch (_text.text[0])
        {
            case 'R':
                return '��';
            case 'r':
                return '��';
            case 'Q':
                return '��';
            case 'q':
                return '��';
            case 'A':
                return '��';
            case 'a':
                return '��';
            case 'S':
                return '��';
            case 's':
                return '��';
            case 'D':
                return '��';
            case 'd':
                return '��';
            case 'F':
                return '��';
            case 'f':
                return '��';
            case 'Z':
                return '��';
            case 'z':
                return '��';
            case 'X':
                return '��';
            case 'x':
                return '��';
            case 'C':
                return '��';
            case 'c':
                return '��';
            case 'V':
                return '��';
            case 'v':
                return '��';
            case 'W':
                return '��';
            case 'w':
                return '��';
            case 'E':
                return '��';
            case 'e':
                return '��';
            case 'T':
                return '��';
            case 't':
                return '��';
            case 'Y':
                return '��';
            case 'y':
                return '��';
            case 'U':
                return '��';
            case 'u':
                return '��';
            case 'I':
                return '��';
            case 'i':
                return '��';
            case 'G':
                return '��';
            case 'g':
                return '��';
            case 'H':
                return '��';
            case 'h':
                return '��';
            case 'J':
                return '��';
            case 'j':
                return '��';
            case 'K':
                return '��';
            case 'k':
                return '��';
            case 'B':
                return '��';
            case 'b':
                return '��';
            case 'N':
                return '��';
            case 'n':
                return '��';
            case 'M':
                return '��';
            case 'm':
                return '��';
            case 'O':
                return '��';
            case 'o':
                return '��';
            case 'P':
                return '��';
            case 'p':
                return '��';
            case 'L':
                return '��';
            case 'l':
                return '��';
            default:
                return _text.text[0];
        }
    }
    */
}