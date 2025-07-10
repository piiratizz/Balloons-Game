using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMockDataProvider : ILeaderboardPlayerDataProvider
{
    private Sprite[] _avatars;

    private const int ProfilesLimit = 10;
    private const int MinPoints = 0;
    private const int MaxPoints = 10000;
    
    public List<LeaderboardPlayerData> Get()
    {
        if (_avatars == null)
        {
            _avatars = Resources.LoadAll<Sprite>("MockAvatars");
        }

        var list = new List<LeaderboardPlayerData>();

        for (int i = 0; i < ProfilesLimit; i++)
        {
            list.Add(new LeaderboardPlayerData()
            {
                Avatar = _avatars[i],
                Nickname = $"Player {i}",
                Points = Random.Range(MinPoints, MaxPoints)
            });
        }

        return list;
    }
}