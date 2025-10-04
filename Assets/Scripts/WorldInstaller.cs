using UnityEngine;
using Zenject;

namespace DefaultNamespace.Installers
{
    public class WorldInstaller : MonoInstaller
    {
        [SerializeField] private Transform _worldHolder;
        [SerializeField] private WorldPreset _worldPreset;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_worldHolder).WithId("WorldSpawn");
            
            Container.BindInterfacesAndSelfTo<WorldGenerator>().AsSingle().NonLazy();
            Container.Bind<ChunkManager>().AsSingle().NonLazy();

            Container.Bind<IChunkFactory>()
                .To<WorldChunkFactory>()
                .AsSingle();
            
            Container.Bind<WorldPreset>().FromInstance(_worldPreset).AsCached();

            Container.BindInterfacesAndSelfTo<ChunkProvider>().AsSingle().NonLazy();
        }
    }
}