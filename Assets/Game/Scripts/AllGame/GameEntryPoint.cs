using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint _instance;

    private CoroutineHolder _coroutineHolder;
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBoot()
    {
        _instance = new GameEntryPoint();
        _instance.InitializeGame();
    }

    private GameEntryPoint()
    {
        _coroutineHolder = new GameObject("CoroutineHolder").AddComponent<CoroutineHolder>();
        Object.DontDestroyOnLoad(_coroutineHolder.gameObject);
    }
    
    private void InitializeGame()
    {
        _coroutineHolder.StartCoroutine(InitializeGameCoroutine());
    }

    
    
    private IEnumerator InitializeGameCoroutine()
    {
        ServiceLocator.Register(new JsonPlayerPrefsLoader());
        ServiceLocator.Register(new PlayerActualDataHolder());
        ServiceLocator.Register(new AvatarLoader("Avatar.png"));
        
        var playerInventory = new PlayerInventory();
        playerInventory.Initialize();
        ServiceLocator.Register(playerInventory);
        
        var loadingScreenPrefab = Resources.Load("LoadingScreen");
        var loadingScreenInstance = Object.Instantiate(loadingScreenPrefab);

        yield return null;
        
        Object.DontDestroyOnLoad(loadingScreenInstance);
        
        var sceneLoaderManager = new SceneLoaderManager(_coroutineHolder, loadingScreenInstance.GetComponent<LoadingScreen>());
        ServiceLocator.Register(sceneLoaderManager);
        
        yield return sceneLoaderManager.LoadScene(Scenes.MainMenu);
        Object.FindObjectOfType<MainMenuEntryPoint>().RunScene();
    }
}