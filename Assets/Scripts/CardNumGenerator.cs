using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardNumGenerator : MonoBehaviour
{
    private GameObject[] bColNums;
    private GameObject[] iColNums;
    private GameObject[] nColNums;
    private GameObject[] gColNums;
    private GameObject[] oColNums;

    [SerializeField] GameObject bNums_11;
    [SerializeField] GameObject bNums_12;
    [SerializeField] GameObject bNums_13;
    [SerializeField] GameObject bNums_14;
    [SerializeField] GameObject bNums_15;

    [SerializeField] GameObject iNums_21;
    [SerializeField] GameObject iNums_22;
    [SerializeField] GameObject iNums_23;
    [SerializeField] GameObject iNums_24;
    [SerializeField] GameObject iNums_25;

    [SerializeField] GameObject nNums_31;
    [SerializeField] GameObject nNums_32;
    [SerializeField] GameObject nNums_33;
    [SerializeField] GameObject nNums_34;
    [SerializeField] GameObject nNums_35;

    [SerializeField] GameObject gNums_41;
    [SerializeField] GameObject gNums_42;
    [SerializeField] GameObject gNums_43;
    [SerializeField] GameObject gNums_44;
    [SerializeField] GameObject gNums_45;

    [SerializeField] GameObject oNums_51;
    [SerializeField] GameObject oNums_52;
    [SerializeField] GameObject oNums_53;
    [SerializeField] GameObject oNums_54;
    [SerializeField] GameObject oNums_55;

    public bool hasResult;
    public string winner; 

    public void Start()
    {
        bColNums = new[] { bNums_11, bNums_12, bNums_13, bNums_14, bNums_15 };
        iColNums = new[] { iNums_21, iNums_22, iNums_23, iNums_24, iNums_25 };
        nColNums = new[] { nNums_31, nNums_32, nNums_33, nNums_34, nNums_35 };
        gColNums = new[] { gNums_41, gNums_42, gNums_43, gNums_44, gNums_45 };
        oColNums = new[] { oNums_51, oNums_52, oNums_53, oNums_54, oNums_55 };

        SetRandomCardNums();
    }

    public void SetRandomCardNums()
    {
        NumGenerator ng = new NumGenerator();

        for (int i = 0; i < bColNums.Length; i++)
        {
            bColNums[i].GetComponentsInChildren<Text>()[0].text = (ng.GenerateUniqueNumberForLetter(BingoLetter.B)).ToString();
            iColNums[i].GetComponentsInChildren<Text>()[0].text = (ng.GenerateUniqueNumberForLetter(BingoLetter.I)).ToString();
            nColNums[i].GetComponentsInChildren<Text>()[0].text = (ng.GenerateUniqueNumberForLetter(BingoLetter.N)).ToString();
            gColNums[i].GetComponentsInChildren<Text>()[0].text = (ng.GenerateUniqueNumberForLetter(BingoLetter.G)).ToString();
            oColNums[i].GetComponentsInChildren<Text>()[0].text = (ng.GenerateUniqueNumberForLetter(BingoLetter.O)).ToString();
        }
    }

    private bool IsColumnComplete(int columnIndex, GameObject[] numbers)
    {
        var result = true;
        var startIndex = (columnIndex * 5);

        for (var i = startIndex; i < startIndex +  5; i++)
        {
            if (!numbers[i].GetComponent<NumView>().isDone)
            {
                result = false;
                break;
            }
        }
        return result;
    }

    private bool IsRowComplete(int rowIndex, GameObject[] numbers)
    {
        var result = true;
        for (var i = rowIndex; i < numbers.Length; i += 5)
        {
            if (!numbers[i].GetComponent<NumView>().isDone)
            {
                result = false;
                break;
            }
        }
        return result;
    }

    /*
     * [0,5,10,15,20]
     * [1,6,11,16,21]
     * [2,7,12,17,22]
     * [3,8,13,18,23]
     * [4,9,14,19,24]
     */

    private bool IsLeftDiagonalComplete(GameObject[] numbers)
    {
        var result = true;

        //Left diagonal indices : 0,6,12,18,24
        for (var i = 0; i < 4; i++)
        {
            var leftDiagonalIndex = i * 6;
            if (!numbers[leftDiagonalIndex].GetComponent<NumView>().isDone)
            {
                result = false;
                break;
            }
        }

        return result;
    }

    private bool IsRightDiagonalComplete(GameObject[] numbers)
    {
        var result = true;

        //Right Diagonal indices : 4,8,12,16,20
        for (var i = 4; i < numbers.Length - 1; i += 4)
        {
            if (!numbers[i].GetComponent<NumView>().isDone)
            {
                result = false;
                break;
            }
        }

        return result;
    }

    private bool HasWinner(GameObject[] numbers)
    {
        var isRowComplete = false;
        var isColumnComplete = false;
        var isLeftDiagnolComplete = IsLeftDiagonalComplete(numbers);
        var isRightDiagnolComplete = IsRightDiagonalComplete(numbers);

        for (var i = 0; i < 5; i++)
        {
            if (IsRowComplete(i, numbers))
            {
                isRowComplete = true;
                break;
            }

            if (IsColumnComplete(i, numbers))
            {
                isColumnComplete = true;
                break;
            }
        }

        var result = isRowComplete || isColumnComplete || isLeftDiagnolComplete || isRightDiagnolComplete;

        return result;
    }

    public void CheckIsNumInComCard(int n)
    {

        var checkN = n.ToString();
        var comNums = GameObject.FindGameObjectsWithTag("ComNums");

        for (int i = 0; i < comNums.Length; i++)
        {
            if (comNums[i].GetComponentsInChildren<Text>()[0].text == checkN)
            {
                //print(checkN + " yes");
                comNums[i].GetComponent<NumView>().ShowMarkNum();
            }
        }

        //print(HasWinner(comNums));

        if (HasWinner(comNums))
        {
            print("com won");
            hasResult = true;
            winner = "..COMPUTER..";
            //SceneManager.LoadScene("ComWon");
        }
    }


    public void CheckIsNumInPlayerCard(int n)
    {
        var checkN = n.ToString();
        var playerNums = GameObject.FindGameObjectsWithTag("PlayerNums");
        for (int i = 0; i < playerNums.Length; i++)
        {
            if (playerNums[i].GetComponentsInChildren<Text>()[0].text == checkN)
            {
                //print(checkN + " yes");
                playerNums[i].GetComponent<NumView>().MarkAsGenerated();
            }
        }

        if (HasWinner(playerNums))         {             print("YOUUUUUUUUUU won");             hasResult = true;
            winner = "!!YOU!!";
            //SceneManager.LoadScene("PlayerWon");         }
    }

}
