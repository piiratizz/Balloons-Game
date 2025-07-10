using System.Collections.Generic;

public class PlayerInventory
{
    private List<int> _ownedBalloonsIndexes;
    private PlayerActualDataHolder _dataHolder;
    private IPlayerSaveLoadService _loadService;
    private int _selectedBalloonIndex;

    public int SelectedBalloonIndex => _selectedBalloonIndex;
    public int Balance => _dataHolder.Data.Money;
    
    public void Initialize()
    {
        _dataHolder = ServiceLocator.Get<PlayerActualDataHolder>();
        _loadService = ServiceLocator.Get<JsonPlayerPrefsLoader>();
        
        _ownedBalloonsIndexes = new List<int>();

        var data = _dataHolder.Data;
        _ownedBalloonsIndexes = data.OwnedBallsIndexes;
        _selectedBalloonIndex = data.SelectedBallIndex;
    }

    public bool IsOwned(BalloonItemData data)
    {
        return _ownedBalloonsIndexes.Contains(data.Index);
    }
    
    public void AddNewBalloon(BalloonItemData data)
    {
        _ownedBalloonsIndexes.Add(data.Index);
        
        var data1 = _dataHolder.Data;
        data1.OwnedBallsIndexes = _ownedBalloonsIndexes;
        _loadService.Save(data1);
    }

    public bool TryPay(int money)
    {
        var data = _dataHolder.Data;

        if (data.Money - money >= 0)
        {
            data.Money -= money;
            _loadService.Save(data);
            return true;
        }
        
        return false;
    }
    
    public void SelectBalloon(BalloonItemData data)
    {
        _selectedBalloonIndex = data.Index;
        var data1 = _dataHolder.Data;
        data1.SelectedBallIndex = _selectedBalloonIndex;
        _loadService.Save(data1);
    }

    public void AddMoney(int money)
    {
        var data = _dataHolder.Data;
        data.Money += money;
        _loadService.Save(data);
    }
}