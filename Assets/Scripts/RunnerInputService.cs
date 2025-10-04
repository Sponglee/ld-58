using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
    
public class RunnerInputService : IInitializable, ITickable, IDisposable
    {
        public event Action<Vector2> OnRunnerMove;
        
        public Vector2 RunnerMoveInput { get; private set; }
        
        private InputAction _moveAction;
        private bool _isPlayerMoving;

        public RunnerInputService(InputActionAsset input)
        {
            var map = input.FindActionMap("Player");
                map.Enable();
            _moveAction = map.FindAction("Move");
        }
        
        public void Initialize()
        {
            _moveAction.started += ctx => _isPlayerMoving = true;
            _moveAction.canceled += ctx => _isPlayerMoving = false;
            _moveAction.Enable();
        }
        
        public void Tick()
        {
            RunnerMoveInput = _moveAction.ReadValue<Vector2>();

            if (_isPlayerMoving)
            {
                OnRunnerMove?.Invoke(RunnerMoveInput);
            }
        }
        
        public void Dispose()
        {
            _moveAction?.Disable();
            _moveAction?.Dispose();
        }
    }