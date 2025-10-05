
using System;using Zenject;

public class GameStateService: IInitializable
{
    public Action<GameState> OnGameStateChanged;
    public GameState GameState { get; private set; }
    
    public GameStateService()
    {
        
    }
    
    public void Initialize()
    {
        ChangeState(GameState.Start);
    }
    
    public void ChangeState(GameState targetState)
    {
        if (GameState == targetState)
        {
            return;
        }
            
        GameState = targetState;
        OnGameStateChanged?.Invoke(GameState);
    }

  
}

public enum GameState
{
    None,
    Start,
    Pause,
    Play,
    Win,
    Lose,
    Shop
}

