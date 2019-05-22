using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
    [SerializeField] public Button replayBt;
    //[SerializeField] public Text WinnerText;
    //[SerializeField] public GameObject GameOverPanel;

    //private CardNumGenerator _cardNumGenerator;

    void Awake()
    {
        //_cardNumGenerator = new CardNumGenerator();
        //WinnerText.text = _cardNumGenerator.winner;
        replayBt.onClick.AddListener(Exit);
    }

    public void Exit()
    {
        print("quit");
        //Application.Quit();
        SceneManager.LoadScene("GameScene");
    }

    void Update()
    {
        //WinnerText.text = _cardNumGenerator.winner;
    }
}
