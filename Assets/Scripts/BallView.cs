using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallView : MonoBehaviour
{
    [SerializeField] private Text _letterText;
    [SerializeField] private Text _numberText;

    private Image _ballImage;
    private RectTransform _rectTransform;
    public int _currentBallPosition = 0;
    private Animator _animator;
    private const int MaxBallsCount = 10;
    private const string MoveToNextPositionAnimationParameterName = "CurrentBallPosition";

    public void Awake()
    {
        _ballImage = GetComponent<Image>();
        _animator = GetComponent<Animator>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void ApplyBingoBallModel(BingoBall ball)
    {
        _letterText.text = ball.Letter.ToString();
        _numberText.text = ball.Number.ToString();
    }

    public void ApplyBallSprite(Sprite sprite)
    {
        _ballImage.sprite = sprite;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public bool IsDisabled
    {
        get { return !gameObject.activeInHierarchy; }
    }

    public void Appear(Vector3 appearancePosition)
    {
        //_currentBallPosition = 0;
        _rectTransform.anchoredPosition = appearancePosition;
        //gameObject.SetActive(true);
        _animator.SetInteger(MoveToNextPositionAnimationParameterName, _currentBallPosition);
    }

    public void Disappear()
    {
        _animator.SetInteger(MoveToNextPositionAnimationParameterName, MaxBallsCount);
    }

    public void MoveToNextPosition()
    {
        //_currentBallPosition++;
        _animator.SetInteger("CurrentBallPosition", _currentBallPosition);
    }
}
