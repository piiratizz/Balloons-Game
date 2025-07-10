using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyConfirmPopup : StartInitializable
{
    [SerializeField] private Button confirmBtn;
    [SerializeField] private Button cancelBtn;
    [SerializeField] private GameObject content;
    [SerializeField] private Image itemImage;
    
    public event Action ConfirmButtonClicked;
    public event Action CancelButtonClicked;

    public override void Initialize()
    {
        confirmBtn.onClick = new Button.ButtonClickedEvent();
        confirmBtn.onClick.AddListener( () => ConfirmButtonClicked?.Invoke());
        
        cancelBtn.onClick = new Button.ButtonClickedEvent();
        cancelBtn.onClick.AddListener( () => CancelButtonClicked?.Invoke());
        
        Hide();
    }

    public void Show(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
        content.SetActive(true);
    }

    public void Hide()
    {
        content.SetActive(false);
    }
}