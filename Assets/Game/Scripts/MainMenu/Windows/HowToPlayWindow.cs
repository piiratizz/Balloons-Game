using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayWindow : WindowBase
{
    [SerializeField] private TextMeshProUGUI howToPlayText;
    [SerializeField] private Button okBtn;
    [SerializeField] private WindowsManager windowsManager;
    
    private HowToPlayTextConfig _config;
    private const string Path = "ActualHowToPlayConfig/HowToPlay";

    private void Start()
    {
        okBtn.onClick = new Button.ButtonClickedEvent();
        okBtn.onClick.AddListener(() => windowsManager.SwitchWindow(WindowTypes.MainMenu));
    }

    public override void Open()
    {
        base.Open();
        if (_config == null)
        {
            _config = Resources.Load(Path) as HowToPlayTextConfig;
        }

        if (_config != null)
        {
            howToPlayText.text = _config.Text;
        }
        
    }
}