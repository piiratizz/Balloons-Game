using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WindowsManager : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton;

    [SerializeField] private WindowBase[] windows;

    private WindowBase _currentWindow;
    private MainMenuWindow _mainMenuWindow;
    
    private Action _onProfileClick;
    private Action _onSettingsClick;
    private Action _onShopClick;
    private Action _onExitClick;
    
    private void Awake()
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
        _onExitClick = () => Application.Quit();
    }

    private void Start()
    {
        foreach (var window in windows)
        {
            window.Close();
        }
        SwitchWindow(WindowTypes.MainMenu);
    }

    private void OnEnable()
    {
        _mainMenuWindow.ProfileButtonClicked += _onProfileClick;
        _mainMenuWindow.SettingsButtonClicked += _onSettingsClick;
        _mainMenuWindow.ShopButtonClicked += _onShopClick;
        _mainMenuWindow.ExitButtonClicked += _onExitClick;
    }
    
    private void OnDisable()
    {
        _mainMenuWindow.ProfileButtonClicked -= _onProfileClick;
        _mainMenuWindow.SettingsButtonClicked -= _onSettingsClick;
        _mainMenuWindow.ShopButtonClicked -= _onShopClick;
        _mainMenuWindow.ExitButtonClicked -= _onExitClick;
    }

    private void SwitchWindow(WindowTypes windowType)
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
