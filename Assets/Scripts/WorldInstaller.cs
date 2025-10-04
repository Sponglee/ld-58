using UnityEngine;
using Zenject;

    public class WorldInstaller : MonoInstaller
    {
        [SerializeField] private Transform _worldHolder;
        [SerializeField] private WorldPreset _worldPreset;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_worldHolder).WithId("WorldSpawn");
            
            Container.BindInterfacesAndSelfTo<WorldGenerator>().AsSingle().NonLazy();
            
            Container.Bind<ChunkManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ChunkProvider>().AsSingle().NonLazy();
            Container.Bind<IChunkFactory>()
                .To<WorldChunkFactory>()
                .AsCached();
            
            Container.Bind<ArtifactManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ArtifactProvider>().AsSingle().NonLazy();
            Container.Bind<IArtifactFactory>()
                .To<ArtifactFactory>()
                .AsCached();
           
            
            Container.Bind<WorldPreset>().FromInstance(_worldPreset).AsCached();
        }
    }