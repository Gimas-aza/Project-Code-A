using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace Assets.UI
{
    public class FloatingJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private GameObject _joystick;

        private OnScreenStick _screenStick;
        private RectTransform _mainRect;
        private RectTransform _joystickRect;
        private Vector2 _joystickStartPosition;

        private void Awake()
        {
            _screenStick = _joystick.GetComponentInChildren<OnScreenStick>();
            _mainRect = GetComponent<RectTransform>();
            _joystickRect = _joystick.GetComponent<RectTransform>();
        }

        private void Start()
        {
            _joystickStartPosition = _joystickRect.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            ExecuteEvents.dragHandler(_screenStick, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 localPosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_mainRect, eventData.pressPosition, Camera.main, out localPosition);

            _joystickRect.position = eventData.position;

            ExecuteEvents.pointerDownHandler(_screenStick, eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _joystickRect.localPosition = _joystickStartPosition;

            ExecuteEvents.pointerUpHandler(_screenStick, eventData);
        }
    }
}
