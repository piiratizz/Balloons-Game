using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

[Serializable]
public class PlayerData
{
    public string Nickname;

    public int Money;
    public int Stars
    {
        get
        {
            int total = 0;
            foreach (var level in CompletedLevels)
            {
                total += level.StarsCollected;
            }

            return total;
        }
    }

    public int Points
    {
        get
        {
            int total = 0;
            foreach (var level in CompletedLevels)
            {
                total += level.PointsCollected;
            }

            return total;
        }
    }
    
    public float SoundLevel;
    public float MusicLevel;
    public bool SendNotification;

    public int SelectedBallIndex;

    public List<int> OwnedBallsIndexes;
    public List<PlayerCompletedLevelData> CompletedLevels;
}