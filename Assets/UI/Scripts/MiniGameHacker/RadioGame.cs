using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class RadioGame : MonoBehaviour
{
    [SerializeField] private RectTransform _frequencyBand;
    [SerializeField] private RectTransform _line;
    [SerializeField] private RectTransform _targetArea;
    [SerializeField] private OptionPassword _optionPassword;
    [SerializeField] private RectTransform _container;
    [SerializeField] private Notification _notification;
    [SerializeField] private TextMeshProUGUI _inputPassword;
    [SerializeField] private TextMeshProUGUI _numbersTryText;
    [SerializeField] private int _numbersTry;

    private float _width;
    private Difficulty _difficulty;
    private Dictionary<Difficulty, float> _difficultyTargetArea = new()
    {
        { Difficulty.Easy, 0.15f },
        { Difficulty.Normal, 0.1f },
        { Difficulty.Hard, 0.05f }
    };
    private Dictionary<Difficulty, float> _difficultyLine = new()
    {
        { Difficulty.Easy, 1.5f },
        { Difficulty.Normal, 1f },
        { Difficulty.Hard, 0.5f }
    };
    private List<OptionPassword> _listPassword;
    private int _count;
    private OptionPassword _truePassword;
    private string _currentPassword = "■■■■■■■■";

    private void Awake()
    {
        _width = _frequencyBand.rect.width;
        _line.anchoredPosition = new Vector2(0f, 0f);
        _targetArea.offsetMin = new Vector2(0f, 0f);
        _targetArea.offsetMax = new Vector2(-_width, 0f);
    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        StopLine();
    }

    public void StartMiniGame(Difficulty difficulty)
    {
        _difficulty = difficulty;
        SetTargetArea(difficulty);
        StartLine(difficulty);
        CreateOptionsPassword(15);
        _numbersTryText.text = _numbersTry.ToString();
    }

    private void StopMiniGame()
    {
        StopLine();
        Debug.Log("Stop");
    }

    private void StartLine(Difficulty difficulty)
    {
        var percent = 1f;
        var currentPosition = _width * percent;
        var speed = _difficultyLine[difficulty];

        _line.anchoredPosition = new Vector2(0f, 0f);

        _line.DOAnchorPosX(currentPosition, speed, false)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .SetUpdate(true);
    }

    private void StopLine()
    {
        _line.DOKill();
    }

    private void SetTargetArea(Difficulty difficulty)
    {
        var area = _difficultyTargetArea[difficulty];
        var percent = Random.Range(0f, 1f - area); 

        var currentPosition = _width * percent;
        var currentArea = _width * area;

        _targetArea.offsetMin = new Vector2(currentPosition, 0);
        _targetArea.offsetMax = new Vector2(currentPosition + currentArea - _width, 0);
    }

    private void CreateOptionsPassword(int length)
    {
        _listPassword = new List<OptionPassword>();
        
        for (int i = 0; i < length; i++)
        {
            var password = Instantiate(_optionPassword, _container);
            password.Init();
            password.OnClick += TryHack;
            _listPassword.Add(password);
        }

        _truePassword = RandomPassword();
        _truePassword.TruePassword = true;
    }

    private OptionPassword RandomPassword()
    {
        var index = Random.Range(0, _listPassword.Count);
        return _listPassword[index];
    }

    private bool CheckLineInArea()
    {
        var position = _line.anchoredPosition;

        if (position.x > _targetArea.offsetMin.x && position.x < _width - _targetArea.offsetMax.x * -1)
            return true;
        return false;
    }

    private void TryHack(OptionPassword password)
    {
        if (!password.TruePassword)
        {
            var text = GetCommonCharacters(_truePassword.TextValue, password.TextValue);
            _notification.gameObject.SetActive(true);
            _notification.WriteTrueSymbols(text);
            _currentPassword = GetPasswordTogether(_currentPassword, text);
            
            _numbersTry--;
            _numbersTryText.text = _numbersTry.ToString();
            if (_numbersTry == 0)
            {
                Debug.Log("Lose");
                StopMiniGame();
            }
        }
        else
        {
            _currentPassword = _truePassword.TextValue;
            Debug.Log("Win");
            StopMiniGame();
        }

        _inputPassword.text = _currentPassword;
    }

    private string GetCommonCharacters(string str1, string str2)
    {
        char[] charArray1 = str1.ToCharArray();
        char[] charArray2 = str2.ToCharArray();

        string commonCharacters = "";

        for (int i = 0; i < charArray1.Length; i++)
        {
            if (charArray1[i].ToString() == charArray2[i].ToString())
                commonCharacters += charArray1[i].ToString();
            else
                commonCharacters += "■";
        }

        return commonCharacters;
    }

    private string GetPasswordTogether(string str1, string str2)
    {
        char[] charArray1 = str1.ToCharArray();
        char[] charArray2 = str2.ToCharArray();

        string password = "";

        for (int i = 0; i < charArray1.Length; i++)
        {
            if (charArray1[i].ToString() != "■")
                password += charArray1[i].ToString();
            else if (charArray2[i].ToString() != "■")
                password += charArray2[i].ToString();
            else
                password += "■";
        }

        return password;
    } 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) // todo настроить управление, чтобы оно работало с машиной состаяний для меню
        {
            InputFrequency(); 
        }
    }

    private void InputFrequency()
    {
        StopLine();
        if (CheckLineInArea())
        {
            _listPassword[_count++].ShowPassword();
            if (_listPassword.Count == _count)
            {
                return;
            }
        }
        SetTargetArea(_difficulty);
        StartLine(_difficulty);
    }
}
