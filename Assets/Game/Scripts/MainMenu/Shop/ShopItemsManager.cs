using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ShopItemsManager : MonoBehaviour
{
    [SerializeField] private BalloonItemView balloonItemsViewPrefab;
    [SerializeField] private GameObject pagePrefab;
    [SerializeField] private List<BalloonPage> balloonPages;
    [SerializeField] private Transform pageSpawnPosition;

    private PlayerInventory _playerInventory;
    private List<GameObject> _pagesInstances;
    private Dictionary<BalloonItemData, BalloonItemView> _balloonsDataViewDictionary;
    private int _currentPage;
    private bool _created;

    private const int ItemsForEachPage = 4;
    
    public int AvailablePages => balloonPages.Count;

    private void Start()
    {
        _playerInventory = ServiceLocator.Get<PlayerInventory>();
        _pagesInstances = new List<GameObject>(balloonPages.Count);
        _balloonsDataViewDictionary = new Dictionary<BalloonItemData, BalloonItemView>();
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
                itemInstance.Initialize(item, _playerInventory.IsOwned(item), _playerInventory.SelectedBalloonIndex == item.Index);
                _balloonsDataViewDictionary.Add(item, itemInstance);
                itemInstance.OnBuyButtonClicked += OnBuy;
                itemInstance.OnSelectButtonClicked += OnSelect;
            }
            _pagesInstances.Add(pageInstance);
            pageInstance.gameObject.SetActive(false);
        }

        _created = true;
    }
    
    public void SelectPage(int page)
    {
        _pagesInstances[_currentPage].gameObject.SetActive(false);
        _currentPage = page;
        _pagesInstances[_currentPage].gameObject.SetActive(true);
    }

    private void OnSelect(BalloonItemView sender, BalloonItemData data)
    {
        var old = _playerInventory.SelectedBalloonIndex;
        var oldData = 
            _balloonsDataViewDictionary.First(p => p.Key.Index == old);
        
        _balloonsDataViewDictionary[oldData.Key].DeselectItem();
        
        sender.SelectItem();
        _playerInventory.SelectBalloon(data);
    }
    
    private void OnBuy(BalloonItemView sender, BalloonItemData data)
    {
        sender.BuyItem();
        _playerInventory.AddNewBalloon(data);
    }

    private void OnEnable()
    {
        if(!_created) return;
        
        foreach (var view in _balloonsDataViewDictionary.Values)
        {
            view.OnBuyButtonClicked += OnBuy;
            view.OnBuyButtonClicked += OnSelect;
        }
    }

    private void OnDisable()
    {
        foreach (var view in _balloonsDataViewDictionary.Values)
        {
            view.OnBuyButtonClicked -= OnBuy;
            view.OnBuyButtonClicked -= OnSelect;
        }
    }
}

[Serializable]
public class BalloonPage
{
    public List<BalloonItemData> balloonsItems;
}