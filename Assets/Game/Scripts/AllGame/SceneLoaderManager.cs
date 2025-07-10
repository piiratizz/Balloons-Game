using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager
{
    private CoroutineHolder _coroutineHolder;
    private LoadingScreen _loadingScreen;
    
    public SceneLoaderManager(CoroutineHolder holder, LoadingScreen loadingScreenInstance)
    {
        _coroutineHolder = holder;
        _loadingScreen = loadingScreenInstance;
    }
    
    public Coroutine LoadScene(string sceneName)
    {
        return _coroutineHolder.StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    public Coroutine LoadMainMenu()
    {
        return _coroutineHolder.StartCoroutine(LoadMainMenuCoroutine());
    }

    private IEnumerator LoadMainMenuCoroutine()
    {
        _loadingScreen.gameObject.SetActive(true);
        _loadingScreen.SetProgress(0);
        yield return Scenes.Boot;
        yield return SceneManager.LoadSceneAsync(Scenes.MainMenu);
        yield return new WaitForSeconds(0.3f);
        _loadingScreen.SetProgress(0.3f);
        yield return new WaitForSeconds(0.3f);
        _loadingScreen.SetProgress(0.7f);
        yield return new WaitForSeconds(0.3f);
        _loadingScreen.SetProgress(1);
        yield return new WaitForSeconds(0.3f);
        _loadingScreen.gameObject.SetActive(false);
        
        Object.FindObjectOfType<MainMenuEntryPoint>().RunScene();
    }
    
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        _loadingScreen.gameObject.SetActive(true);
        _loadingScreen.SetProgress(0);
        yield return Scenes.Boot;
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitForSeconds(0.3f);
        _loadingScreen.SetProgress(0.3f);
        yield return new WaitForSeconds(0.3f);
        _loadingScreen.SetProgress(0.7f);
        yield return new WaitForSeconds(0.3f);
        _loadingScreen.SetProgress(1);
        yield return new WaitForSeconds(0.3f);
        _loadingScreen.gameObject.SetActive(false);
    }
}