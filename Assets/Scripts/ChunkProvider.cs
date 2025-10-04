    using System;
    using Zenject;

    public class ChunkProvider : IInitializable, ITickable, IDisposable
    {

        private GamePreset _gamePreset;
        private ChunkManager _chunkManager;
        private GameStateService _gameStateService;

        private bool _isLevelMoving = false;

        public ChunkProvider(
            GamePreset gamePreset,
            ChunkManager chunkManager,
            GameStateService gameStateService)
        {
            _gamePreset = gamePreset;
            _chunkManager = chunkManager;
            _gameStateService = gameStateService;
        }

        public void Initialize()
        {
            _gameStateService.OnGameStateChanged += GameStateHandler;
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

            _chunkManager.MoveChunks(_gamePreset.LevelMoveSpeed);
        }
        
        private void GameStateHandler(GameState state)
        {
            _isLevelMoving = state == GameState.Play;
        }
    }
    
