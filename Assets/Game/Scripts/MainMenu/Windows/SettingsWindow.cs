using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : WindowBase
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Toggle notificationToggle;
    [SerializeField] private Button saveBtn;
    
    private PlayerActualDataHolder _dataHolder;
    private IPlayerSaveLoadService _loadService;
    
    public override void Initialize()
    {
        _dataHolder = ServiceLocator.Get<PlayerActualDataHolder>();
        _loadService = ServiceLocator.Get<JsonPlayerPrefsLoader>();
        
        saveBtn.onClick = new Button.ButtonClickedEvent();
        saveBtn.onClick.AddListener(Save);
    }

    public override void Open()
    {
        base.Open();
        var data = _dataHolder.Data;
        
        soundSlider.value = data.SoundLevel;
        musicSlider.value = data.MusicLevel;
        notificationToggle.isOn = data.SendNotification;
    }

    private void Save()
    {
        var data = _dataHolder.Data;

        data.SoundLevel = soundSlider.value;
        data.MusicLevel = musicSlider.value;
        data.SendNotification = notificationToggle.isOn;
        
        _loadService.Save(data);
    }
}