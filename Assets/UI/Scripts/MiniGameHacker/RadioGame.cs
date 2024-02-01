using Zelude;
using UnityEngine;

public class RadioGame : MonoBehaviour
{
    [SerializeField] private RectTransform _line;
    [SerializeField] private RectTransform _targetArea;
    [SerializeField, Range(0, 1)] private float _currentPositionLine;
    [SerializeField, MinMaxSlider(0, 1)] private Vector2 _currentPositionTargetArea;

    private float _width;

    private void OnValidate()
    {
        _width = GetComponent<RectTransform>().rect.width;

        SetLine(_currentPositionLine); 
        SetTargetArea(_currentPositionTargetArea);
    }

    private void Awake()
    {
        _line.anchoredPosition = new Vector2(0f, 0f);
    }

    private void SetLine(float percent)
    {
        percent = Mathf.Clamp01(percent);
        var currentPosition = _width * percent;

        _line.anchoredPosition = new Vector2(currentPosition, 0f);
    }

    private void SetTargetArea(Vector2 percent)
    {
        var currentPosition = _width * percent;
        Debug.Log(_width);
        _targetArea.offsetMin = new Vector2(currentPosition.x, 0);
        _targetArea.offsetMax = new Vector2(currentPosition.y - _width, 0);
    }
}
