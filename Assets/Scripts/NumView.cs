using UnityEngine;
using UnityEngine.UI;

public class NumView : MonoBehaviour
{
    private Text _text;
    private Button _button;
    private bool _generated;

    public Sprite playerMark;
    public bool isDone;

    public void Awake()
    {
        _text = GetComponentInChildren<Text>();
        _button = GetComponent<Button>();
    }

    public void ShowMarkNum()
    {
        _button.image.sprite = playerMark;
        _button.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        _text.text = "";
        //_button.enabled = false;
        _button.interactable = false;
        isDone = true;
    }

    public void Reset()
    {
        if (_button.image.sprite = playerMark)
        {
            _button.image.sprite = null;
        }

        _generated = false;
        isDone = false;
    }

    public void Update()
    {
        if (_generated)
        {
            _button.onClick.AddListener(ShowMarkNum);
        }
    }

    public void SetNumber(int number)
    {
        _text.text = number.ToString();
    }

    public void MarkAsGenerated()
    {
        _generated = true;
    }

    public void Disable()
    {
        _button.interactable = false;
    }

    public bool Generated
    {
        get { return _generated; }
    }
}
