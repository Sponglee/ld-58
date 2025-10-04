
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputSystemInstaller : MonoInstaller<InputSystemInstaller>
{
    [SerializeField] private InputActionAsset _inputAsset;
    [SerializeField] private Runner _runner;
    
    public override void InstallBindings()
    {
        Container.Bind<InputActionAsset>()
            .FromInstance(_inputAsset)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<RunnerInputService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<RunnerProvider>().AsSingle().NonLazy();
        Container.Bind<Runner>().FromInstance(_runner).AsSingle();
    }
}