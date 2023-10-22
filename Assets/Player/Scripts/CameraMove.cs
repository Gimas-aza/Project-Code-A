using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CameraMove : MonoBehaviour
{
    [Header("Follow")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothing = 5;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Button _rotateCamera;

    private CinemachineFreeLook _cinemachineFreeLook;
    private float _targetCenterCamera = 0;
    private bool _isRotate = false;

    private void Awake()
    {
        _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
    }

    private void OnEnable()
    {
        _rotateCamera.onClick.AddListener(SetСenterСamera);
    }

    private void OnDisable()
    {
        _rotateCamera.onClick.RemoveListener(SetСenterСamera);
    }

    private void Update()
    {
        if (_isRotate)
        {
            StartCoroutine(SmoothCenterСamera(_targetCenterCamera, _smoothing));
            _isRotate = false;
        }
    }

    private void SetСenterСamera()
    {
        _targetCenterCamera = Math.Abs(_player.transform.eulerAngles.y - _mainCamera.transform.eulerAngles.y);
        _isRotate = true;
    }

    private IEnumerator<WaitForEndOfFrame> SmoothCenterСamera(float target, float smooth)
    {
        float currentRotateCamera = _mainCamera.transform.eulerAngles.y;

        for (int i = 0; i < (int)target / smooth; i++)
        {
            if (_player.transform.eulerAngles.y < currentRotateCamera)
                _cinemachineFreeLook.m_XAxis.Value -= smooth;
            else if (_player.transform.eulerAngles.y > currentRotateCamera)
                _cinemachineFreeLook.m_XAxis.Value += smooth;
            
            yield return new WaitForEndOfFrame();
        }
    }
}
