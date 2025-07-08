using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileWindow : WindowBase
{
    [SerializeField] private Button saveButton;
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private TMP_InputField inputField;
    
    private PlayerNicknameManager _nicknameManager;
    
    private void Start()
    {
        saveButton.onClick = new Button.ButtonClickedEvent();
        saveButton.onClick.AddListener(Save);
        _nicknameManager = new PlayerNicknameManager();
    }

    public override void Open()
    {
        base.Open();
        nicknameText.text = _nicknameManager.Get();
    }
    
    public override void Close()
    {
        inputField.text = string.Empty;
        base.Close();
    }

    private void Save()
    {
        _nicknameManager.Set(inputField.text);
        nicknameText.text = inputField.text;
    }
    
}