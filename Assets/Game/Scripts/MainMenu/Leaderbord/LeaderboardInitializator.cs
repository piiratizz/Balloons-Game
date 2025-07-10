using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderboardInitializator : MonoBehaviour
{
    [SerializeField] private LeaderboardPlayerView leaderBoardPlayerPrefab;
    [SerializeField] private LeaderboardProfileView leaderBoardProfileView;
    [SerializeField] private Transform playersViewsContainer;
    
    private PlayerActualDataHolder _dataHolder;
    private ILeaderboardPlayerDataProvider _dataProvider;
    private AvatarLoader _avatarLoader;
    
    public void Initialize(ILeaderboardPlayerDataProvider dataProvider)
    {
        _avatarLoader = ServiceLocator.Get<AvatarLoader>();
        
        if (_dataHolder == null)
        {
            _dataHolder = ServiceLocator.Get<PlayerActualDataHolder>();
        }

        _dataProvider = dataProvider;
        
        InitializeProfile();
        InitializePlayers();
    }

    private void InitializeProfile()
    {
        var data = _dataHolder.Data;
        int averageStars = 0;
        if (data.CompletedLevels.Count != 0)
        {
            averageStars = data.Stars / data.CompletedLevels.Count;
        }

        var avatar = _avatarLoader.GetAvatar();
        leaderBoardProfileView.Initialize(avatar, data.Nickname, data.Points, averageStars);
    }

    private void InitializePlayers()
    {
        StartCoroutine(InitializePlayersCoroutine());
    }

    private IEnumerator InitializePlayersCoroutine()
    {
        var profilesData = _dataProvider.Get();

        var playerData = _dataHolder.Data;

        Sprite spriteAvatar = _avatarLoader.GetAvatar();
        
        var playerLeaderboardProfile = new LeaderboardPlayerData()
        {
            Avatar = null,
            Nickname = playerData.Nickname,
            Points = playerData.Points,
            IsLocalPlayer = true
        };

        if (spriteAvatar != null)
        {
            playerLeaderboardProfile.Avatar = spriteAvatar;
        }

        profilesData.Add(playerLeaderboardProfile);
        
        var orderedData = profilesData.OrderByDescending(s => s.Points).ToList();
        
        var playersInstances = new List<LeaderboardPlayerView>();
        
        for (int i = 0; i < profilesData.Count; i++)
        {
            var instance = Instantiate(leaderBoardPlayerPrefab, playersViewsContainer);
            playersInstances.Add(instance);
        }
        
        yield return null;
        
        for (int i = 0; i < profilesData.Count; i++)
        {
            var data = orderedData[i];
            playersInstances[i].Initialize(data.Avatar, data.Nickname, data.Points, data.IsLocalPlayer);
        }
    }

    public void Clear()
    {
        foreach (Transform child in playersViewsContainer)
        {
            Destroy(child.gameObject);
        }
    }
}