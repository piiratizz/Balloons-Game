using System;
using System.Collections.Generic;
using UnityEngine;

public class JsonPlayerPrefsLoader : IPlayerSaveLoadService
{
    private const string SaveKey = "PlayerSaveData";
    
    private readonly PlayerData _defaultPlayerData = new PlayerData()
    {
        Nickname = "Player",
        Money = 1000,
        SoundLevel = 100,
        MusicLevel = 100,
        SendNotification = true,
        SelectedBallIndex = 0,
        OwnedBallsIndexes = new List<int>(collection: new List<int>(1) {0}),
        CompletedLevels = new List<PlayerCompletedLevelData>()
    };
    
    public event Action OnDataUpdated;
    
    public void Save(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, prettyPrint: true);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
        
        OnDataUpdated?.Invoke();
    }

    public PlayerData Load()
    {
        if (!PlayerPrefs.HasKey(SaveKey))
        {
            return _defaultPlayerData;
        }

        string json = PlayerPrefs.GetString(SaveKey);
        return JsonUtility.FromJson<PlayerData>(json);
    }
}