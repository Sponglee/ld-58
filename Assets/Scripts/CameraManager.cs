using System;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class CameraManager
{
    public event Action<CameraState> CameraStateChanged;

    private CameraPreset _cameraSettingsPreset;

    private Camera _mainCamera;
    private CinemachineCamera _vCam;
        
    private CinemachineOrbitalFollow _orbitalFollow;
    private Transform _cameraPivot;
    private Tween _moveCameraTween;

    public CameraState CameraState { get; private set; }
    
    [Inject]
    public CameraManager(
        CameraPreset cameraSettings,
        [Inject(Id = "CameraPivot")] Transform cameraPivot,
        [Inject(Id = "VCam")] CinemachineCamera vCam,
        [Inject(Id = "MainCamera")] Camera mainCamera)
    {
        _cameraPivot = cameraPivot;
        _cameraSettingsPreset = cameraSettings;
        _mainCamera = mainCamera;
        _vCam = vCam;
        _orbitalFollow = _vCam.GetComponent<CinemachineOrbitalFollow>();
    }

    public void LookAt(Transform objTransform)
    {
        _moveCameraTween?.Kill();
        _moveCameraTween = null;
            
        ChangeState(CameraState.Cinematic);
        var lookAtPos = Vector3.Scale(new Vector3(1,0,1),objTransform.position);
        _moveCameraTween = _cameraPivot.DOMove(lookAtPos, _cameraSettingsPreset.CameraLookAtDuration).SetEase(_cameraSettingsPreset.CameraLookAtEase).OnComplete(()=>ChangeState(CameraState.LookAt));
    }

    private void ChangeState(CameraState targetState)
    {
        if (CameraState == targetState)
        {
            return;
        }
            
        CameraState = targetState;
        CameraStateChanged?.Invoke(CameraState);
    }
        
    public void Move(Vector2 move)
    {
        MoveCamera(move, _cameraSettingsPreset.CameraMoveSpeed);
    }
        
    public void Pan(Vector2 move)
    {
        MoveCamera(move, _cameraSettingsPreset.CameraPanSpeed);
    }

    public void Rotate(float input)
    {
        _orbitalFollow.HorizontalAxis.Value += input * _cameraSettingsPreset.CameraRotateSpeed;
        _orbitalFollow.HorizontalAxis.Validate();
    }
        
    public void Zoom(float zoom)
    {
        _orbitalFollow.RadialAxis.Value -= zoom * _cameraSettingsPreset.CameraZoomSpeed;
        _orbitalFollow.RadialAxis.Validate();
    }
        
    private void MoveCamera(Vector2 move, float speedOverride)
    {
        ChangeState(CameraState.Idle);
            
        _moveCameraTween?.Kill();
        _moveCameraTween = null;
            
        var moveDirection = Quaternion.Euler(0, _vCam.transform.rotation.eulerAngles.y, 0)
                            * new Vector3(move.x, 0, move.y) * _orbitalFollow.RadialAxis.Value;
        _cameraPivot.Translate(moveDirection * speedOverride);
    }
}

public enum CameraState
{
    LookAt,
    Cinematic,
    Idle,
    Follow
}