using System.Collections.Generic;

public class PlayerInventory
{
    private int _balance;
    private List<int> _ownedBalloonsIndexes;
    private PlayerActualDataHolder _dataHolder;
    private IPlayerSaveLoadService _loadService;
    private int _selectedBalloonIndex;

    public int SelectedBalloonIndex => _selectedBalloonIndex;
    
    public void Initialize()
    {
        _dataHolder = ServiceLocator.Get<PlayerActualDataHolder>();
        _loadService = ServiceLocator.Get<JsonPlayerPrefsLoader>();
        
        _ownedBalloonsIndexes = new List<int>();

        var data = _dataHolder.Data;
        _balance = data.Money;
        _ownedBalloonsIndexes = data.OwnedBallsIndexes;
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

    public void SelectBalloon(BalloonItemData data)
    {
        _selectedBalloonIndex = data.Index;
    }
}