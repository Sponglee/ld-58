    using System;
    using Zenject;

    public class ChunkProvider : IInitializable, ITickable, IDisposable
    {
        private WorldPreset _worldPreset;
        private GamePreset _gamePreset;
        private ChunkManager _chunkManager;
        private GameStateService _gameStateService;

        private bool _isLevelMoving = false;

        public ChunkProvider(
            WorldPreset worldPreset,
            GamePreset gamePreset,
            ChunkManager chunkManager,
            GameStateService gameStateService)
        {
            _gamePreset = gamePreset;
            _chunkManager = chunkManager;
            _gameStateService = gameStateService;
            _worldPreset = worldPreset;
            
            _gameStateService.OnGameStateChanged += GameStateHandler;
        }

        public void Initialize()
        {
           
        }

        public void Dispose()
        {
            _gameStateService.OnGameStateChanged -= GameStateHandler;
        }

        public void Tick()
        {
            if (!_isLevelMoving)
            {
                return;
            }

            _chunkManager.MoveChunks(_gamePreset.LevelMoveSpeed, _worldPreset);
        }
        
        private void GameStateHandler(GameState state)
        {
            _isLevelMoving = state == GameState.Play;
        }
    }
    
