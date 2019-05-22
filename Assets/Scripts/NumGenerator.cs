using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

public enum BingoLetter
{
    B = 0, I, N, G, O
}

public class NumGenerator : MonoBehaviour
{
    private readonly List<int> _bNumList = null;
    private readonly List<int> _iNumList = null;
    private readonly List<int> _nNumList = null;
    private readonly List<int> _gNumList = null;
    private readonly List<int> _oNumList = null;
    private readonly List<int>[] _allNumListsArr = null;

    public NumGenerator()
    {
        _bNumList = GenerateNums(1, 10);
        _iNumList = GenerateNums(11, 20);
        _nNumList = GenerateNums(21, 30);
        _gNumList = GenerateNums(31, 40);
        _oNumList = GenerateNums(41, 50);

        _allNumListsArr = new[] { _bNumList, _iNumList, _nNumList, _gNumList, _oNumList};
    }

    public int GenerateUniqueNumberForLetter(BingoLetter letter)
    {
      
        var letterIndex = letter.GetHashCode();

        var numbersList = _allNumListsArr[letterIndex];

        var randomIndexForPickNumber = Random.Range(0, numbersList.Count);

        var randomNumber = numbersList[randomIndexForPickNumber];

        numbersList.RemoveAt(randomIndexForPickNumber);

        return randomNumber;
    }

    private List<int> GenerateNums(int from, int to)
    {
        var result = new List<int>();
        for (var i = from; i <= to; i++)
        {
            result.Add(i);
        }
        return result;
    }

    public int GenerateNumberForLetter(BingoLetter letter)
    {
        var letterIndex = letter.GetHashCode();
        print("letterIndex "+letterIndex);
        return 0;
    }

    public bool CanGenerateNumberForLetter(BingoLetter letter)
    {
        var letterIndex = letter.GetHashCode();
        return _allNumListsArr[letterIndex].Count > 0;
    }


}
