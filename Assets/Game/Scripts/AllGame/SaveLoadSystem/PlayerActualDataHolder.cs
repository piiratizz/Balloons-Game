using System.Collections.Generic;

public class PlayerActualDataHolder
{
    private IPlayerSaveLoadService _loadService;
    private PlayerData _playerData;

    public PlayerData Data => new PlayerData()
    {
        Nickname = _playerData.Nickname,
        Money = _playerData.Money,
        Stars = _playerData.Stars,
        Points = _playerData.Points,
        SoundLevel = _playerData.SoundLevel,
        MusicLevel = _playerData.MusicLevel,
        SendNotification = _playerData.SendNotification,
        SelectedBallIndex = _playerData.SelectedBallIndex,
        OwnedBallsIndexes = new List<int>(_playerData.OwnedBallsIndexes),
        CompletedLevels = new List<PlayerCompletedLevelData>(_playerData.CompletedLevels),
    };
    
    public PlayerActualDataHolder()
    {
        _loadService = ServiceLocator.Get<JsonPlayerPrefsLoader>();
        _loadService.OnDataUpdated += UpdateData;
        UpdateData();
    }

    private void UpdateData()
    {
        _playerData = _loadService.Load();
    }
}