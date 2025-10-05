
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Zenject;

public class InputSystemInstaller : MonoInstaller<InputSystemInstaller>
{
    [SerializeField] private InputActionAsset _inputAsset;
    [SerializeField] private RunnerView _runnerView;
    
    public override void InstallBindings()
    {
        Container.Bind<InputActionAsset>()
            .FromInstance(_inputAsset)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<RunnerInputService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<RunnerProvider>().AsSingle().NonLazy();
        Container.Bind<RunnerView>().FromInstance(_runnerView).AsSingle();
    }
}