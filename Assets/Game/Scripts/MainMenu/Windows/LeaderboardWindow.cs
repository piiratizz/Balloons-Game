using System;
using UnityEngine;

public class LeaderboardWindow : WindowBase
{
    [SerializeField] private LeaderboardInitializator leaderboardInitializator;
    
    public override void Open()
    {
        base.Open();
        leaderboardInitializator.Initialize(new LeaderboardMockDataProvider());
    }

    public override void Close()
    {
        leaderboardInitializator.Clear();
        base.Close();
    }
}