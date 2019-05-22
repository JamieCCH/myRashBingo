using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingoBall : MonoBehaviour
{

    public BingoBall(BingoLetter letter, int number)
    {
        Letter = letter;
        Number = number;
    }

    public BingoLetter Letter { get; private set; }

    public int Number { get; private set; }

}
