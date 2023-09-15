using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

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
    private readonly List<ushort> _includeLetter = new List<ushort>() { 39, 44, 45, 46, 47, 59, 61, 91, 92, 93, 96 }; 

    System.Random _rand = new System.Random();

    private Dictionary<char, int> _appearLetterSearchChar = new Dictionary<char, int>();
    private Dictionary<int, char> _appearLetterSearchInt = new Dictionary<int, char>();

    private char _curLetter;
    private short _countDuplication;

    private void Start()
    {
        Divide_Letter(new StringBuilder("1格;櫛'蟹5櫛精./"));
        CheckDupllicate(20, 2);
        _appearLetterSearchChar.Clear();
        _appearLetterSearchInt.Clear();
    }

    private StringBuilder CheckDupllicate(int letterLen, int typeCount)
    {
        StringBuilder nonDuplicateString = new StringBuilder(letterLen);
        List<int> randType = new List<int>();
        int randomNum, count = 0;
        while (randType.Count < typeCount || count < 100)
        {
            ++count;
            randomNum = _rand.Next(0, _appearLetterSearchChar.Count);
            if (randType.IndexOf(randomNum) != -1)
                randType.Add(randomNum);
        }
        Debug.Log(randType.Count);

        for (int i = 0; i < letterLen; ++i)
        {
            randomNum = _rand.Next(0, randType.Count);
    
            if (_curLetter == _appearLetterSearchInt[randomNum])
            {
                if (_countDuplication > 2)
                {
                    --i;
                    continue;
                }

                ++_countDuplication;
            }

            _curLetter = _appearLetterSearchInt[randomNum];
            nonDuplicateString.Append(_curLetter);
        }
        Debug.Log(nonDuplicateString.ToString());
        return nonDuplicateString;
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
                if ((tempUnicode >= _numberStart && tempUnicode <= _numberEnd)
                || _includeLetter.IndexOf(tempUnicode) != -1)
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
}