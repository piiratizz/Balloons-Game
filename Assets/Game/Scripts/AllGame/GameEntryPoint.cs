using UnityEngine;

public class GameEntryPoint
{
    private static GameEntryPoint _instance;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBoot()
    {
        _instance = new GameEntryPoint();
        _instance.InitializeGame();
    }

    private void InitializeGame()
    {
        ServiceLocator.Register(new JsonPlayerPrefsLoader());
        ServiceLocator.Register(new PlayerActualDataHolder());
        
        var playerInventory = new PlayerInventory();
        playerInventory.Initialize();
        ServiceLocator.Register(playerInventory);
    }
}