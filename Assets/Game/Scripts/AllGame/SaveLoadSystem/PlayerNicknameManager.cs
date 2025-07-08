using System;

public class PlayerNicknameManager
{
    private IPlayerSaveLoadService _loadService;
    private PlayerActualDataHolder _dataHolder;
    
    public PlayerNicknameManager()
    {
        _loadService = ServiceLocator.Get<JsonPlayerPrefsLoader>();
        _dataHolder = ServiceLocator.Get<PlayerActualDataHolder>();
    }

    public string Get()
    {
        return _dataHolder.Data.Nickname;
    }

    public void Set(string nickname)
    {
        var data = _dataHolder.Data;
        data.Nickname = nickname;
        _loadService.Save(data);
    }
}