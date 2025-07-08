using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

[Serializable]
public class PlayerData
{
    public string Nickname;

    public int Money;
    public int Stars;
    public int Points;
    
    public float SoundLevel;
    public float MusicLevel;
    public bool SendNotification;

    public int SelectedBallIndex;

    public List<int> OwnedBallsIndexes;
    public List<PlayerCompletedLevelData> CompletedLevels;
}