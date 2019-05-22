using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject BallPrefab;
    [SerializeField] private Sprite bBallSprite;
    [SerializeField] private Sprite iBallSprite;
    [SerializeField] private Sprite nBallSprite;
    [SerializeField] private Sprite gBallSprite;
    [SerializeField] private Sprite oBallSprite;
    [SerializeField] private Transform ballSpawnPosition;

    [SerializeField] public Button replayBt;
    [SerializeField] public Text WinnerText;
    [SerializeField] public GameObject GameOverPanel;

    private GameObject Ball;
    private Queue<GameObject> _ballsQueue;
    private NumGenerator _numbersGenerator;
    private CardNumGenerator _cardNumGenerator;

    public void Awake()
    {
        _ballsQueue = new Queue<GameObject>();
        _numbersGenerator = new NumGenerator();
        _cardNumGenerator = new CardNumGenerator();
        InvokeRepeating("BallsInQueue", 1.5f, 2.05f);
        GameOverPanel.SetActive(false);

        replayBt.onClick.AddListener(ResetGame);
    }

    public void ResetGame()
    {
        Application.Quit();
        //GameOverPanel.SetActive(false);
        //InvokeRepeating("BallsInQueue", 1.5f, 2.05f);
        //_cardNumGenerator.Start();
        //_cardNumGenerator.hasResult = false;
        //var com = GameObject.FindGameObjectsWithTag("ComNums");
        //foreach (GameObject num in com)
        //{
        //    num.GetComponent<NumView>().Reset();
        //}
    }

    public void SpawnBall()
    {
        var parent = GameObject.Find("Canvas");
        Ball = Instantiate(BallPrefab, ballSpawnPosition.transform.position, Quaternion.identity);
        Ball.transform.SetParent(parent.transform, true);
        Ball.GetComponent<BallView>()._currentBallPosition = 0;
        _ballsQueue.Enqueue(Ball);
    }

    private BingoLetter GenerateRandomBingoLetter()
    {
        var randomLetterIndex = UnityEngine.Random.Range(0, 5);
        var letterString = Enum.GetName(typeof(BingoLetter), randomLetterIndex).ToString();
        BingoLetter letter;
        Enum.TryParse<BingoLetter>(letterString, out letter);
        return letter;
    }

    public void SetBall()
    {
        var randomLetter = GenerateRandomBingoLetter();
        var randomNumber = _numbersGenerator.GenerateUniqueNumberForLetter(randomLetter);
        var newBall = new BingoBall(randomLetter, randomNumber);
        Ball.GetComponent<BallView>().ApplyBingoBallModel(newBall);

        switch (randomLetter)
        {
            case BingoLetter.B:
                Ball.GetComponent<BallView>().ApplyBallSprite(bBallSprite);
                break;
            case BingoLetter.I:
                Ball.GetComponent<BallView>().ApplyBallSprite(iBallSprite);
                break;
            case BingoLetter.N:
                Ball.GetComponent<BallView>().ApplyBallSprite(nBallSprite);
                break;
            case BingoLetter.G:
                Ball.GetComponent<BallView>().ApplyBallSprite(gBallSprite);
                break;
            case BingoLetter.O:
                Ball.GetComponent<BallView>().ApplyBallSprite(oBallSprite);
                break;
            default:
                break;
        }

        _cardNumGenerator.CheckIsNumInComCard(randomNumber);
        _cardNumGenerator.CheckIsNumInPlayerCard(randomNumber);
        if (_cardNumGenerator.hasResult)
        {
            CancelInvoke();
            //GameOverPanel.SetActive(true);
            WinnerText.text = _cardNumGenerator.winner;

            var balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject ball in balls)
            {
                //Destroy(ball);
                ball.GetComponent<BallView>().Disable();
            }
        }
    }

    private void MoveExistingBalls()
    { 
        foreach (var ball in _ballsQueue)
        {
            ball.GetComponent<BallView>()._currentBallPosition++;
            ball.GetComponent<BallView>().MoveToNextPosition();
            //print(ball.GetComponent<BallView>()._currentBallPosition);

            if (ball.GetComponent<BallView>()._currentBallPosition > 10)
            {
                ball.GetComponent<BallView>().Disable();
            }
        }
    }

    private void BallsInQueue()
    {
        SpawnBall();
        SetBall();
        MoveExistingBalls();
    }

}
