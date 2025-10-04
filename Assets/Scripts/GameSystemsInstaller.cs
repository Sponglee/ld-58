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
        Container.BindInterfacesAndSelfTo<GameUIController>().AsSingle().NonLazy();

        Container.Bind<MainMenuModel>().AsSingle().NonLazy();
        Container.Bind<MainMenuView>().FromInstance(_uiViews[1] as MainMenuView);
        Container.BindInterfacesAndSelfTo<MainMenuController>().AsSingle().NonLazy();
        
        
        Container.BindInterfacesAndSelfTo<InventoryService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<InventoryItemsProvider>().AsSingle().NonLazy();
        Container.Bind<InventorySlotViewFactory>().AsCached();
        
        
        Container.BindInterfacesAndSelfTo<ItemMouseService>().AsSingle().NonLazy();


    }
}