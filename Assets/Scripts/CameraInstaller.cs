using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private CinemachineCamera _cameraCinemachine;
    [SerializeField] private Camera _cameraMain;

    public override void InstallBindings()
    {
        Container.BindInstance(_cameraPivot).WithId("CameraPivot");
        Container.BindInstance(_cameraCinemachine).WithId("VCam");
        Container.BindInstance(_cameraMain).WithId("CameraMain");

        Container.Bind<CameraManager>().AsSingle().WithArguments(_cameraCinemachine, _cameraMain).NonLazy();
    }
}

