using System;

public interface IPlayerSaveLoadService
{
    event Action OnDataUpdated;
    void Save(PlayerData data);
    PlayerData Load();
}