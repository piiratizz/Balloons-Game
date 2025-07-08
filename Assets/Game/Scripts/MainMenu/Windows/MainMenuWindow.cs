using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuWindow : WindowBase
{
    [SerializeField] private Button profileButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button exitButton;

    public event Action ProfileButtonClicked;
    public event Action SettingsButtonClicked;
    public event Action ShopButtonClicked;
    public event Action ExitButtonClicked;

    private void Start()
    {
        profileButton.onClick = new Button.ButtonClickedEvent();
        profileButton.onClick.AddListener(() => ProfileButtonClicked?.Invoke());
        
        settingsButton.onClick = new Button.ButtonClickedEvent();
        settingsButton.onClick.AddListener(() => SettingsButtonClicked?.Invoke());
        
        shopButton.onClick = new Button.ButtonClickedEvent();
        shopButton.onClick.AddListener(() => ShopButtonClicked?.Invoke());
        
        exitButton.onClick = new Button.ButtonClickedEvent();
        exitButton.onClick.AddListener(() => ExitButtonClicked?.Invoke());
    }
}