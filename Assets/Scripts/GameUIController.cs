public class GameUIController
{
    private GameUIView _view;
    private GameUIModel _model;
    
    public GameUIController(GameUIView view, GameUIModel model)
    {
        _view = view;
        _model = model;
    }

    public void ToggleUI(bool toggleState)
    {
        _view.gameObject.SetActive(toggleState);
    }
}