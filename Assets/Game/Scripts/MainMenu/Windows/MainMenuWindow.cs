using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuWindow : WindowBase
{
    [SerializeField] private Button profileButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button playButton;

    public event Action ProfileButtonClicked;
    public event Action SettingsButtonClicked;
    public event Action ShopButtonClicked;
    public event Action LeaderboardButtonClicked;
    public event Action HowToPlayButtonClicked;
    public event Action ExitButtonClicked;
    public event Action PlayButtonClicked;
    
    public override void Initialize()
    {
        profileButton.onClick = new Button.ButtonClickedEvent();
        profileButton.onClick.AddListener(() => ProfileButtonClicked?.Invoke());
        
        settingsButton.onClick = new Button.ButtonClickedEvent();
        settingsButton.onClick.AddListener(() => SettingsButtonClicked?.Invoke());
        
        shopButton.onClick = new Button.ButtonClickedEvent();
        shopButton.onClick.AddListener(() => ShopButtonClicked?.Invoke());
        
        leaderboardButton.onClick = new Button.ButtonClickedEvent();
        leaderboardButton.onClick.AddListener(() => LeaderboardButtonClicked?.Invoke());
        
        howToPlayButton.onClick = new Button.ButtonClickedEvent();
        howToPlayButton.onClick.AddListener(() => HowToPlayButtonClicked?.Invoke());
        
        exitButton.onClick = new Button.ButtonClickedEvent();
        exitButton.onClick.AddListener(() => ExitButtonClicked?.Invoke());
        
        playButton.onClick = new Button.ButtonClickedEvent();
        playButton.onClick.AddListener(() => PlayButtonClicked?.Invoke());
    }
}