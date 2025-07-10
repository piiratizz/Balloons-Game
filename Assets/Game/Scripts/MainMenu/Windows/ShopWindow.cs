
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : WindowBase
{
    [SerializeField] private Button leftArrowBtn;
    [SerializeField] private Button rightArrowBtn;
    [SerializeField] private ShopItemsManager shopItemsManager;
    
    private int _selectedPage;
    
    public override void Initialize()
    {
        leftArrowBtn.onClick = new Button.ButtonClickedEvent();
        leftArrowBtn.onClick.AddListener(PreviousPage);
        
        rightArrowBtn.onClick = new Button.ButtonClickedEvent();
        rightArrowBtn.onClick.AddListener(NextPage);
    }

    public override void Open()
    {
        base.Open();
        _selectedPage = 0;
        shopItemsManager.Create();
        shopItemsManager.SelectPage(_selectedPage);
    }

    private void NextPage()
    {
        if (_selectedPage+1 <= shopItemsManager.AvailablePages-1)
        {
            _selectedPage++;
            shopItemsManager.SelectPage(_selectedPage);
        }
    }

    private void PreviousPage()
    {
        if (_selectedPage-1 >= 0)
        {
            _selectedPage--;
            shopItemsManager.SelectPage(_selectedPage);
        }
    }
}
