using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopItemsManager : MonoBehaviour
{
    [SerializeField] private BalloonItemView balloonItemsViewPrefab;
    [SerializeField] private GameObject pagePrefab;
    [SerializeField] private List<BalloonPage> balloonPages;
    [SerializeField] private Transform pageSpawnPosition;
    [SerializeField] private BuyConfirmPopup buyConfirmPopup;
    [SerializeField] private TextMeshProUGUI balanceText;
    
    private PlayerInventory _playerInventory;
    private List<GameObject> _pagesInstances;
    private Dictionary<BalloonItemData, BalloonItemView> _balloonsDataViewDictionary;
    private KeyValuePair<BalloonItemView, BalloonItemData> _selectedToBuyItem;
    private int _currentPage;
    private bool _created;
    private bool _initialized;
    
    private const int ItemsForEachPage = 4;
    
    public int AvailablePages => balloonPages.Count;

    private void Initialize()
    {
        _playerInventory = ServiceLocator.Get<PlayerInventory>();
        _pagesInstances = new List<GameObject>(balloonPages.Count);
        _balloonsDataViewDictionary = new Dictionary<BalloonItemData, BalloonItemView>();
        UpdateBalanceView();
        _initialized = true;
    }

    public void Create()
    {
        if(_created) return;
        
        foreach (var page in balloonPages)
        {
            var pageInstance = Instantiate(pagePrefab, pageSpawnPosition.position, Quaternion.identity, transform);
            foreach (var item in page.balloonsItems)
            {
                var itemInstance = Instantiate(balloonItemsViewPrefab, pageInstance.transform).GetComponent<BalloonItemView>();
                var isBalloonOwned = _playerInventory.IsOwned(item);
                var isBalloonSelected = _playerInventory.SelectedBalloonIndex == item.Index;
                
                itemInstance.Initialize(item, isBalloonOwned, isBalloonSelected);
                _balloonsDataViewDictionary.Add(item, itemInstance);
                itemInstance.OnBuyButtonClicked += OnBuyClicked;
                itemInstance.OnSelectButtonClicked += OnSelectBalloon;
            }
            _pagesInstances.Add(pageInstance);
            pageInstance.gameObject.SetActive(false);
        }

        buyConfirmPopup.ConfirmButtonClicked += OnBuyConfirmed;
        buyConfirmPopup.CancelButtonClicked += OnBuyCanceled;
        
        _created = true;
    }
    
    public void SelectPage(int page)
    {
        _pagesInstances[_currentPage].gameObject.SetActive(false);
        _currentPage = page;
        _pagesInstances[_currentPage].gameObject.SetActive(true);
    }

    private void OnSelectBalloon(BalloonItemView sender, BalloonItemData data)
    {
        var old = _playerInventory.SelectedBalloonIndex;
        var oldData = 
            _balloonsDataViewDictionary.First(p => p.Key.Index == old);
        
        _balloonsDataViewDictionary[oldData.Key].DeselectItem();
        
        sender.SelectItem();
        _playerInventory.SelectBalloon(data);
    }
    
    private void OnBuyClicked(BalloonItemView sender, BalloonItemData data)
    {
        if(_playerInventory.Balance < data.Price) return;
         
        _selectedToBuyItem = new KeyValuePair<BalloonItemView, BalloonItemData>(sender, data);
        buyConfirmPopup.Show(data.Sprite);
    }

    private void OnBuyConfirmed()
    {
        if (_playerInventory.TryPay(_selectedToBuyItem.Value.Price))
        {
            _selectedToBuyItem.Key.BuyItem();
            _playerInventory.AddNewBalloon(_selectedToBuyItem.Value);
            buyConfirmPopup.Hide();
            UpdateBalanceView();
        }
    }

    private void OnBuyCanceled()
    {
        buyConfirmPopup.Hide();
    }

    private void OnEnable()
    {
        if (!_initialized)
        {
            Initialize();
            return;
        }
        
        if(!_created) return;
        
        foreach (var view in _balloonsDataViewDictionary.Values)
        {
            view.OnBuyButtonClicked += OnBuyClicked;
            view.OnSelectButtonClicked += OnSelectBalloon;
        }
        
        buyConfirmPopup.ConfirmButtonClicked += OnBuyConfirmed;
        buyConfirmPopup.CancelButtonClicked += OnBuyCanceled;
    }

    private void OnDisable()
    {
        foreach (var view in _balloonsDataViewDictionary.Values)
        {
            view.OnBuyButtonClicked -= OnBuyClicked;
            view.OnSelectButtonClicked -= OnSelectBalloon;
        }
        
        buyConfirmPopup.ConfirmButtonClicked -= OnBuyConfirmed;
        buyConfirmPopup.CancelButtonClicked -= OnBuyCanceled;
    }

    private void UpdateBalanceView()
    {
        balanceText.text = _playerInventory.Balance.ToString();
    }
}

[Serializable]
public class BalloonPage
{
    public List<BalloonItemData> balloonsItems;
}