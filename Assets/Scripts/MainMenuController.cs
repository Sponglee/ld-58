public class MainMenuController
{
    private UIViewBase _view;
    private MainMenuModel _model;
    
    public MainMenuController(MainMenuView menuView, MainMenuModel model)
    {
        _view = menuView;
        _model = model;
    }
    
    public void ToggleUI(bool toggleState)
    {
        _view.gameObject.SetActive(toggleState);
    }

    private void StartGame()
    {
        
    }
}