using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraMove : InputPlayer
{
    [Header("Follow")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothing = 5;

    private InputSystem _inputSystem;
    private InputAction _actionMove;
    private CinemachineFreeLook _cinemachineFreeLook;
    private float _targetCenterCamera = 0;
    private bool _isInverseRotate = false;

    protected override void Awake()
    {
        base.Awake();
        _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        InputSystem.Player.CenterCamera.performed += SetСenterСamera;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        InputSystem.Player.CenterCamera.performed -= SetСenterСamera;
    }

    private void SetСenterСamera(InputAction.CallbackContext context)
    {
        _targetCenterCamera = Math.Abs((int)_player.transform.eulerAngles.y - (int)MainCamera.transform.eulerAngles.y);
        if (_targetCenterCamera > 180)
        {
            _targetCenterCamera = 360 - _targetCenterCamera;
            _isInverseRotate = true;
        }

        StartCoroutine(SmoothCenterСamera(_targetCenterCamera, _smoothing));
        _isInverseRotate = false;
    }

    private IEnumerator<WaitForEndOfFrame> SmoothCenterСamera(float target, float smooth)
    {
        float currentRotateCamera = MainCamera.transform.eulerAngles.y;
        float currentRotatePlayer = _player.transform.eulerAngles.y;

        if (_isInverseRotate)
        {
            currentRotateCamera = (currentRotateCamera > 180) ? currentRotateCamera - 360 : currentRotateCamera;
            currentRotatePlayer = (currentRotatePlayer > 180) ? currentRotatePlayer - 360 : currentRotatePlayer;
        }

        for (int i = 0; i < (int)target / smooth; i++)
        {
            float direction = _isInverseRotate ? -1 : 1;
            if (currentRotatePlayer < currentRotateCamera)
                _cinemachineFreeLook.m_XAxis.Value -= direction * smooth;
            else if (currentRotatePlayer > currentRotateCamera)
                _cinemachineFreeLook.m_XAxis.Value += direction * smooth;
            
            yield return new WaitForEndOfFrame();
        }

        _cinemachineFreeLook.m_XAxis.Value = _player.transform.eulerAngles.y - MainCamera.transform.eulerAngles.y;
    }
}
