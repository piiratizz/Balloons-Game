using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BalloonItemView : MonoBehaviour
{
    [SerializeField] private Image itemPreviewImage;
    [SerializeField] private Button buyBtn;
    [SerializeField] private Button selectBtn;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image selectedImage;
    
    [SerializeField] private string selectedText = "Selected";
    [SerializeField] private string ownedText = "Owned";
    
    public event Action<BalloonItemView, BalloonItemData> OnBuyButtonClicked;
    public event Action<BalloonItemView, BalloonItemData> OnSelectButtonClicked;
    
    private BalloonItemData _itemData;
    
    public void Initialize(BalloonItemData data, bool owned, bool selected)
    {
        _itemData = data;
        itemPreviewImage.sprite = _itemData.Sprite;
        HideButton(buyBtn);
        HideButton(selectBtn);
        selectedImage.gameObject.SetActive(false);
        
        if (!owned)
        {
            BindButton(buyBtn, BuyButtonClicked);
            priceText.text = _itemData.Price.ToString();
            return;
        }
        
        HideButton(buyBtn);
        
        if (selected)
        {
            priceText.text = selectedText;
            selectedImage.gameObject.SetActive(true);
            return;
        }
        
        BindButton(selectBtn, SelectButtonClicked);
        priceText.text = ownedText;
    }

    private void BuyButtonClicked()
    {
        OnBuyButtonClicked?.Invoke(this, _itemData);
    }

    public void BuyItem()
    {
        HideButton(buyBtn);
        BindButton(selectBtn, SelectButtonClicked);
        priceText.text = ownedText;
    }

    public void SelectItem()
    {
        HideButton(selectBtn);
        selectedImage.gameObject.SetActive(true);
        priceText.text = selectedText;
    }

    public void DeselectItem()
    {
        BindButton(selectBtn, SelectButtonClicked);
        selectedImage.gameObject.SetActive(false);
        priceText.text = ownedText;
    }
    
    private void SelectButtonClicked()
    {
        OnSelectButtonClicked?.Invoke(this, _itemData);
    }

    private void BindButton(Button btn, UnityAction method)
    {
        btn.gameObject.SetActive(true);
        btn.onClick = new Button.ButtonClickedEvent();
        btn.onClick.AddListener(method);
    }
    
    private void HideButton(Button btn)
    {
        btn.onClick.RemoveAllListeners();
        btn.gameObject.SetActive(false);
    }
}