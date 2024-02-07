using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class RadioGame : MonoBehaviour
{
    [SerializeField] private RectTransform _line;
    [SerializeField] private RectTransform _targetArea;
    [SerializeField] private OptionPassword _optionPassword;
    [SerializeField] private RectTransform _container;
    [SerializeField] private Notification _notification;
    [SerializeField] private TextMeshProUGUI _inputPassword;

    private float _width;
    private Dictionary<Difficulty, float> _difficulty = new()
    {
        { Difficulty.Easy, 0.15f },
        { Difficulty.Normal, 0.1f },
        { Difficulty.Hard, 0.05f }
    };
    private List<OptionPassword> _listPassword;
    private int _count;
    private OptionPassword _truePassword;
    private string _currentPassword = "■■■■■■■■";

    private void Awake()
    {
        _width = GetComponent<RectTransform>().rect.width;
        _line.anchoredPosition = new Vector2(0f, 0f);
        _targetArea.offsetMin = new Vector2(0f, 0f);
        _targetArea.offsetMax = new Vector2(-_width, 0f);
    }

    private void Start()
    {
        SetTargetArea(Difficulty.Easy);
        StartLine(1f);
        CreateOptionsPassword(10);
    }

    private void OnDestroy()
    {
        StopLine();
    }

    private void StartLine(float speed)
    {
        var percent = 1f;
        var currentPosition = _width * percent;

        _line.anchoredPosition = new Vector2(0f, 0f);

        _line.DOAnchorPosX(currentPosition, speed, false)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear)
            .SetUpdate(true);
    }

    [ContextMenu(nameof(StopLine))]
    private void StopLine()
    {
        _line.DOKill();
    }

    private void SetTargetArea(Difficulty difficulty)
    {
        var area = _difficulty[difficulty];
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
        }
        else
        {
            _currentPassword = _truePassword.TextValue;
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
        if (Input.GetKeyDown(KeyCode.Tab))
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
            SetTargetArea(Difficulty.Easy);
            StartLine(1f);
        }
    }
}
