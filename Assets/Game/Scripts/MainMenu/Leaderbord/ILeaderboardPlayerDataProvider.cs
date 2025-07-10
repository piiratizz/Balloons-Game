using System.Collections.Generic;

public interface ILeaderboardPlayerDataProvider
{
    List<LeaderboardPlayerData> Get();
}