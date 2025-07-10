using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class LevelSelectingManager : StartInitializable
{
    [SerializeField] private AvailableLevelView availableLevelViewPrefab;
    [SerializeField] private GameObject lockedLevelPrefab;
    [SerializeField] private Transform levelsContainer;
    
    private const string Path = "Levels";
    private PlayerActualDataHolder _dataHolder;
    private LevelConfig[] _levels;

    private List<AvailableLevelView> _levelsViews = new List<AvailableLevelView>();
    private SceneLoaderManager _sceneLoaderManager;
    
    public override void Initialize()
    {
        _dataHolder = ServiceLocator.Get<PlayerActualDataHolder>();
        _sceneLoaderManager = ServiceLocator.Get<SceneLoaderManager>();
        _levels = Resources.LoadAll<LevelConfig>(Path);
        _levels = _levels.OrderBy(l => l.LevelIndex).ToArray(); 
    }

    public void CreateLevels()
    {
        StartCoroutine(CreateLevelsCoroutine());
    }

    private IEnumerator CreateLevelsCoroutine()
    {
        var completedLevels = _dataHolder.Data.CompletedLevels;
        int completedCount = completedLevels.Count;

        for (int i = 0; i < completedCount; i++)
        {
            CreateAvailableLevel(completedLevels[i].LevelIndex, completedLevels[i].StarsCollected);
            yield return null;
        }
        
        if (completedCount < _levels.Length)
        {
            CreateAvailableLevel(_levels[completedCount].LevelIndex, 0);
            completedCount++;
            yield return null;
        }
        
        for (int i = completedCount; i < _levels.Length; i++)
        {
            Instantiate(lockedLevelPrefab, levelsContainer);
            yield return null;
        }
    }

    private void CreateAvailableLevel(int levelIndex, int stars)
    {
        var instance = Instantiate(availableLevelViewPrefab, levelsContainer);
        instance.Initialize(levelIndex, stars);
        instance.OnLevelSelected += OnLevelSelected;
        _levelsViews.Add(instance);
    }

    private void OnLevelSelected(int levelIndex)
    {
        ClearLevels();
        _sceneLoaderManager.LoadScene($"Level {levelIndex}");
    }
    
    public void ClearLevels()
    {
        foreach (var view in _levelsViews)
        {
            view.OnLevelSelected -= OnLevelSelected;
        }
        _levelsViews.Clear();
        
        foreach (Transform child in levelsContainer)
        {
            Destroy(child.gameObject);
        }
    }
}