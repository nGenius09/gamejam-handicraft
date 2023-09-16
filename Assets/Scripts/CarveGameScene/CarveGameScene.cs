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
    private readonly StringBuilder _startconsonant = new StringBuilder("ぁあいぇえぉけげこさざしじすずせぜそぞ");
    private readonly StringBuilder _vowel = new StringBuilder("ただちぢっつづてでとどなにぬねのはばぱひび");
    private readonly StringBuilder _endconsonant = new StringBuilder("ぁあぃいぅうぇぉおかがきぎくぐけげごさざしじずせぜそぞ");
    private readonly StringBuilder _excludedLetter = new StringBuilder("こすえあざぢてとどなねのはひぃぅうおかがきぎくぐご");

    private readonly ushort _koreanStart = 0xAC00;
    private readonly ushort _koreanEnd = 0xD79F;
    private readonly ushort _numberStart = 48;
    private readonly ushort _numberEnd = 57;

    private Dictionary<KeyCode, char> _keyCodeNCharPair = new Dictionary<KeyCode, char>();
    
    private readonly List<ushort> _includeLetter = new List<ushort>() { 39, 44, 45, 46, 47, 59, 61, 91, 92, 93, 96 }; 

    System.Random _rand = new System.Random();

    private Dictionary<char, int> _appearLetterSearchChar = new Dictionary<char, int>();
    private Dictionary<int, char> _appearLetterSearchInt = new Dictionary<int, char>();

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
        Divide_Letter(new StringBuilder("1格;櫛'蟹5櫛精./"));
        CheckDupllicate(20, 4);
        _appearLetterSearchChar.Clear();
        _appearLetterSearchInt.Clear();
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
            //辛芝但 active食採?
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
                        //惟績 魁!
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
                    //叔鳶!
                }
            }
        }
    }

    private void CheckDupllicate(int letterLen, int typeCount)
    {
        _nonDuplicateString = new StringBuilder(letterLen);
        List<int> randType = new List<int>();
        int randomNum;
        char curLetter = '!';
        short countDuplication = 1;

        while (randType.Count < typeCount)
        {
            randomNum = _rand.Next(0, _appearLetterSearchChar.Count);
            if (randType.IndexOf(randomNum) == -1)
                randType.Add(randomNum);
        }

        for (int i = 0; i < letterLen; ++i)
        {
            randomNum = _rand.Next(0, randType.Count);
    
            if (curLetter == _appearLetterSearchInt[randomNum])
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
                curLetter = _appearLetterSearchInt[randomNum];
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
                {
                    //dividedLetter.Append(str[i]);
                    if (_appearLetterSearchChar.TryAdd(str[i], _appearLetterSearchChar.Count))
                        _appearLetterSearchInt.Add(_appearLetterSearchInt.Count, str[i]);
                }
                continue;
            }
            tempUnicode -= _koreanStart;
            letterStart = tempUnicode / (21 * 28);
            //dividedLetter.Append(_startconsonant[letterStart]);
            if (_appearLetterSearchChar.TryAdd(_startconsonant[letterStart], _appearLetterSearchChar.Count))
                _appearLetterSearchInt.Add(_appearLetterSearchInt.Count, _startconsonant[letterStart]);

            tempUnicode %= (21 * 28);
            letterMid = tempUnicode / 28;
            //dividedLetter.Append(_vowel[letterMid]);
            if (_appearLetterSearchChar.TryAdd(_vowel[letterMid], _appearLetterSearchChar.Count))
                _appearLetterSearchInt.Add(_appearLetterSearchInt.Count, _vowel[letterMid]);

            tempUnicode %= 28;
            letterEnd = tempUnicode;

            if (letterEnd != 0)
            {
                //dividedLetter.Append(_endconsonant[letterEnd - 1]);
                if (_appearLetterSearchChar.TryAdd(_endconsonant[letterEnd - 1], _appearLetterSearchChar.Count))
                    _appearLetterSearchInt.Add(_appearLetterSearchInt.Count, _endconsonant[letterEnd - 1]);
            }
        }

        for (i = 0; i < _excludedLetter.Length; ++i)
            _appearLetterSearchChar.Remove(_excludedLetter[i]);
    }

    public void Clear()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    private void MakeKeyCodeNCharTable()
    {
        _keyCodeNCharPair.Add(KeyCode.Q, 'げ');
        _keyCodeNCharPair.Add(KeyCode.W, 'じ');
        _keyCodeNCharPair.Add(KeyCode.E, 'ぇ');
        _keyCodeNCharPair.Add(KeyCode.R, 'ぁ');
        _keyCodeNCharPair.Add(KeyCode.T, 'さ');
        _keyCodeNCharPair.Add(KeyCode.Y, 'に');
        _keyCodeNCharPair.Add(KeyCode.U, 'づ');
        _keyCodeNCharPair.Add(KeyCode.I, 'ち');
        _keyCodeNCharPair.Add(KeyCode.O, 'だ');
        _keyCodeNCharPair.Add(KeyCode.P, 'つ');
        _keyCodeNCharPair.Add(KeyCode.A, 'け');
        _keyCodeNCharPair.Add(KeyCode.S, 'い');
        _keyCodeNCharPair.Add(KeyCode.D, 'し');
        _keyCodeNCharPair.Add(KeyCode.F, 'ぉ');
        _keyCodeNCharPair.Add(KeyCode.G, 'ぞ');
        _keyCodeNCharPair.Add(KeyCode.H, 'で');
        _keyCodeNCharPair.Add(KeyCode.J, 'っ');
        _keyCodeNCharPair.Add(KeyCode.K, 'た');
        _keyCodeNCharPair.Add(KeyCode.L, 'び');
        _keyCodeNCharPair.Add(KeyCode.Z, 'せ');
        _keyCodeNCharPair.Add(KeyCode.X, 'ぜ');
        _keyCodeNCharPair.Add(KeyCode.C, 'ず');
        _keyCodeNCharPair.Add(KeyCode.V, 'そ');
        _keyCodeNCharPair.Add(KeyCode.B, 'ば');
        _keyCodeNCharPair.Add(KeyCode.N, 'ぬ');
        _keyCodeNCharPair.Add(KeyCode.M, 'ぱ');
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
                return 'ぁ';
            case 'r':
                return 'ぁ';
            case 'Q':
                return 'げ';
            case 'q':
                return 'げ';
            case 'A':
                return 'け';
            case 'a':
                return 'け';
            case 'S':
                return 'い';
            case 's':
                return 'い';
            case 'D':
                return 'し';
            case 'd':
                return 'し';
            case 'F':
                return 'ぉ';
            case 'f':
                return 'ぉ';
            case 'Z':
                return 'せ';
            case 'z':
                return 'せ';
            case 'X':
                return 'ぜ';
            case 'x':
                return 'ぜ';
            case 'C':
                return 'ず';
            case 'c':
                return 'ず';
            case 'V':
                return 'そ';
            case 'v':
                return 'そ';
            case 'W':
                return 'じ';
            case 'w':
                return 'じ';
            case 'E':
                return 'ぇ';
            case 'e':
                return 'ぇ';
            case 'T':
                return 'さ';
            case 't':
                return 'さ';
            case 'Y':
                return 'に';
            case 'y':
                return 'に';
            case 'U':
                return 'づ';
            case 'u':
                return 'づ';
            case 'I':
                return 'ち';
            case 'i':
                return 'ち';
            case 'G':
                return 'ぞ';
            case 'g':
                return 'ぞ';
            case 'H':
                return 'で';
            case 'h':
                return 'で';
            case 'J':
                return 'っ';
            case 'j':
                return 'っ';
            case 'K':
                return 'た';
            case 'k':
                return 'た';
            case 'B':
                return 'ば';
            case 'b':
                return 'ば';
            case 'N':
                return 'ぬ';
            case 'n':
                return 'ぬ';
            case 'M':
                return 'ぱ';
            case 'm':
                return 'ぱ';
            case 'O':
                return 'だ';
            case 'o':
                return 'だ';
            case 'P':
                return 'つ';
            case 'p':
                return 'つ';
            case 'L':
                return 'び';
            case 'l':
                return 'び';
            default:
                return _text.text[0];
        }
    }
    */
}