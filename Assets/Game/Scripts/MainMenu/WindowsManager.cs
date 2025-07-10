using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WindowsManager : StartInitializable
{
    [SerializeField] private Button backToMenuButton;

    [SerializeField] private WindowBase[] windows;

    private WindowBase _currentWindow;
    private MainMenuWindow _mainMenuWindow;
    
    private Action _onProfileClick;
    private Action _onSettingsClick;
    private Action _onShopClick;
    private Action _onLeaderboardClick;
    private Action _onHowToPlayClick;
    private Action _onExitClick;
    private Action _onPlayClick;

    private bool _initialized = false;
    
    public override void Initialize()
    {
        backToMenuButton.onClick = new Button.ButtonClickedEvent();
        backToMenuButton.onClick.AddListener(OnBackToMenuClicked);
        
        _mainMenuWindow = windows.First(w => w.Type == WindowTypes.MainMenu) as MainMenuWindow;
        Debug.Log(_mainMenuWindow);
        if (_mainMenuWindow == null)
        {
            Debug.LogError("Can not find main menu window");
        }
        
        _onProfileClick = () => SwitchWindow(WindowTypes.Profile);
        _onSettingsClick = () => SwitchWindow(WindowTypes.Settings);
        _onShopClick = () => SwitchWindow(WindowTypes.Shop);
        _onLeaderboardClick = () => SwitchWindow(WindowTypes.Leaderboard);
        _onHowToPlayClick = () => SwitchWindow(WindowTypes.HowToPlay);
        _onPlayClick = () => SwitchWindow(WindowTypes.LevelSelectingWindow);
        _onExitClick = () => Application.Quit();
        
        foreach (var window in windows)
        {
            window.Initialize();
            window.Close();
        }
        
        SwitchWindow(WindowTypes.MainMenu);
        Subscribe();
        _initialized = true;
    }
    
    private void OnEnable()
    {
        if(!_initialized) return;

        Subscribe();
    }

    private void Subscribe()
    {
        _mainMenuWindow.ProfileButtonClicked += _onProfileClick;
        _mainMenuWindow.SettingsButtonClicked += _onSettingsClick;
        _mainMenuWindow.ShopButtonClicked += _onShopClick;
        _mainMenuWindow.LeaderboardButtonClicked += _onLeaderboardClick;
        _mainMenuWindow.HowToPlayButtonClicked += _onHowToPlayClick;
        _mainMenuWindow.ExitButtonClicked += _onExitClick;
        _mainMenuWindow.PlayButtonClicked += _onPlayClick;
    }

    private void OnDisable()
    {
        if(!_initialized) return;
        
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        _mainMenuWindow.ProfileButtonClicked -= _onProfileClick;
        _mainMenuWindow.SettingsButtonClicked -= _onSettingsClick;
        _mainMenuWindow.ShopButtonClicked -= _onShopClick;
        _mainMenuWindow.LeaderboardButtonClicked -= _onLeaderboardClick;
        _mainMenuWindow.HowToPlayButtonClicked -= _onHowToPlayClick;
        _mainMenuWindow.ExitButtonClicked -= _onExitClick;
        _mainMenuWindow.PlayButtonClicked -= _onPlayClick;
    }


    public void SwitchWindow(WindowTypes windowType)
    {
        backToMenuButton.gameObject.SetActive(windowType != WindowTypes.MainMenu);
        
        var required = windows.First(w => w.Type == windowType);
        
        if (_currentWindow != null)
        {
            _currentWindow.Close();
        }
        
        _currentWindow = required;
        _currentWindow.Open();
    }

    private void OnBackToMenuClicked()
    {
        SwitchWindow(WindowTypes.MainMenu);
    }
}
