using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSystemsInstaller : MonoInstaller
{
    [SerializeField] private List<UIViewBase> _uiViews;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle().NonLazy();

        Container.BindInterfacesTo<GameUIProvider>().AsSingle();
        Container.BindInterfacesTo<MainMenuProvider>().AsSingle();

        Container.Bind<GameUIModel>().AsSingle().NonLazy();
        Container.Bind<GameUIView>().FromInstance(_uiViews[0] as GameUIView);
        Container.Bind<GameUIController>().AsSingle().NonLazy();

        Container.Bind<MainMenuModel>().AsSingle().NonLazy();
        Container.Bind<MainMenuView>().FromInstance(_uiViews[1] as MainMenuView);
        Container.Bind<MainMenuController>().AsSingle().NonLazy();
    }
}

public class GameUIProvider: IInitializable, IDisposable
{
    private GameStateService _gameStateService;
    private GameUIController _gameUIController;
    
    public GameUIProvider(GameUIController gameUIController, GameStateService gameStateService)
    {
        _gameStateService = gameStateService;
        _gameUIController = gameUIController;
    }

    public void Initialize()
    {
        _gameUIController.ToggleUI(false);

        _gameStateService.OnGameStateChanged += StateChangeHandler;
    }

    private void StateChangeHandler(GameState gameState)
    {
        var toggleState = gameState == GameState.Play;
        _gameUIController.ToggleUI(toggleState);
    }

    public void Dispose()
    {
        _gameStateService.OnGameStateChanged -= StateChangeHandler;
    }
}