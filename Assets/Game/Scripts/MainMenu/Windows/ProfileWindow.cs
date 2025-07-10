using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProfileWindow : WindowBase
{
    [SerializeField] private Image avatar;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button avatarButton;
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private TMP_InputField nicknameChangeInput;
    [SerializeField] private TMP_InputField avatarChangeInput;
    
    private PlayerNicknameManager _nicknameManager;
    private AvatarLoader _avatarLoader;

    private bool _avatarChangeMode;
    
    public override void Initialize()
    {
        _avatarLoader = ServiceLocator.Get<AvatarLoader>();
        
        saveButton.onClick = new Button.ButtonClickedEvent();
        saveButton.onClick.AddListener(Save);

        avatarButton.onClick = new Button.ButtonClickedEvent();
        avatarButton.onClick.AddListener(OnChangeAvatarClicked);
            
        _nicknameManager = new PlayerNicknameManager();
    }

    private void OnChangeAvatarClicked()
    {
        avatarChangeInput.gameObject.SetActive(true);
        _avatarChangeMode = true;
    }
    
    public override void Open()
    {
        base.Open();
        
        avatarChangeInput.gameObject.SetActive(false);
        _avatarChangeMode = false;

        var loadedAvatar = _avatarLoader.GetAvatar();

        if (loadedAvatar != null)
        {
            avatar.sprite = loadedAvatar;
        }
        
        nicknameText.text = _nicknameManager.Get();
    }
    
    public override void Close()
    {
        nicknameChangeInput.text = string.Empty;
        base.Close();
    }

    private void Save()
    {
        if (_avatarChangeMode)
        {
            _avatarLoader.LoadAndSaveAvatar(avatarChangeInput.text);
            avatar.sprite = _avatarLoader.GetAvatar();
        }
        
        if (nicknameChangeInput.text != string.Empty)
        {
            nicknameText.text = nicknameChangeInput.text;
            _nicknameManager.Set(nicknameChangeInput.text);
        }
    }
    
}