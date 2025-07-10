using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class BalloonsManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private List<Balloon> balloonsPrefabs;
    [SerializeField] private Balloon deadBalloonPrefab;
    [SerializeField] private GameplayUI gameplayUI;
    [SerializeField] private PointsCounter pointsCounter;

    private PlayerActualDataHolder _dataHolder;
    private IPlayerSaveLoadService _loadService;
    private PlayerInventory _playerInventory;
    
    private List<Balloon> _goodBalloonsInstances;
    private List<Balloon> _allBalloonsInstances;
    
    private float _balloonMaxSpeed = 1f;
    private float _balloonMinSpeed = 0.1f;

    private LevelConfig _levelConfig;
    private int _remainingBalloons;
    private float _spawnDelay;
    private float _deadBalloonSpawnChance;

    private PlayerData _cachedNotUpdatableData;
    private Sprite _selectedSkin;
    
    
    public void Initialize(LevelConfig config)
    {
        _levelConfig = config;
        _balloonMinSpeed = _levelConfig.balloonsMinSpeed;
        _balloonMaxSpeed = _levelConfig.balloonsMaxSpeed;
        _remainingBalloons = _levelConfig.totalBalloons;
        _spawnDelay = _levelConfig.secondsDelayBetweenSpawn;
        _deadBalloonSpawnChance = _levelConfig.deadBalloonSpawnPercentChance;

        _goodBalloonsInstances = new List<Balloon>();
        _allBalloonsInstances = new List<Balloon>();

        _dataHolder = ServiceLocator.Get<PlayerActualDataHolder>();
        _loadService = ServiceLocator.Get<JsonPlayerPrefsLoader>();
        _playerInventory = ServiceLocator.Get<PlayerInventory>();

        _cachedNotUpdatableData = _dataHolder.Data;

        var balloonsData = Resources.LoadAll<BalloonItemData>("Shop");
        _selectedSkin = balloonsData.First(b => b.Index == _cachedNotUpdatableData.SelectedBallIndex).Sprite;
    }

    public void StartSpawnLoop()
    {
        StartCoroutine(SpawnLoopCoroutine());
    }

    private IEnumerator SpawnLoopCoroutine()
    {
        while (_remainingBalloons > 0)
        {
            SpawnSingle();
            yield return new WaitForSeconds(_spawnDelay);
        }

        int pointsForStar = Mathf.RoundToInt((float)_levelConfig.Points / 3);
        int totalStars = 0;
        
        
        yield return new WaitWhile(() => _goodBalloonsInstances.Count > 0);
        
        for (int i = 1; i <= 3; i++)
        {
            if (pointsCounter.Points >= pointsForStar * i)
            {
                totalStars++;
            }
        }
        
        if (pointsCounter.Points >= _levelConfig.Points)
        {
            totalStars = 3;
        }
        
        gameplayUI.ShowStars(totalStars);
        gameplayUI.ShowLevelCompletedPopup();
        
        SaveProgress(totalStars, pointsCounter.Points);
        ClearBalloonsInstances();
        
    }
    
    private void SpawnSingle()
    {
        float randomX = Random.Range(0f, 1f);
        float yViewport = 0f;
        
        Vector3 viewportPos = new Vector3(randomX, yViewport, 0f);
        Vector3 worldPos = camera.ViewportToWorldPoint(viewportPos);
        
        worldPos.z = 0f;

        Balloon spawnableBalloon;
        bool deadBalloon;
        if (Random.Range(0, 100) <= _deadBalloonSpawnChance)
        {
            spawnableBalloon = deadBalloonPrefab;
            deadBalloon = true;
        }
        else
        {
            spawnableBalloon = balloonsPrefabs[Random.Range(0, balloonsPrefabs.Count)];
            _remainingBalloons--;
            deadBalloon = false;
        }
        
        var balloonInstance = Instantiate(spawnableBalloon, worldPos, Quaternion.identity);
        
        if (!deadBalloon)
        {
            _goodBalloonsInstances.Add(balloonInstance);
        }

        _allBalloonsInstances.Add(balloonInstance);
        
        balloonInstance.Initialize(_selectedSkin, Random.Range(_balloonMaxSpeed, _balloonMinSpeed));
        balloonInstance.OnBalloonClicked += OnBalloonClicked;
        balloonInstance.OnDestroyerCollided += DestroyBalloon;
    }

    private void DestroyBalloon(Balloon sender)
    {
        _goodBalloonsInstances.Remove(sender);
        Destroy(sender.gameObject);
    }

    private void OnBalloonClicked(Balloon sender, bool isDeadBalloon)
    {
        if (isDeadBalloon)
        {
            StopAllCoroutines();
            gameplayUI.ShowLevelFailedPopup();
            ClearBalloonsInstances();
            return;
        }
        
        pointsCounter.Add(_levelConfig.Points / _levelConfig.totalBalloons);
        DestroyBalloon(sender);
    }

    private void ClearBalloonsInstances()
    {
        for (int i = 0; i < _allBalloonsInstances.Count; i++)
        {
            if (_allBalloonsInstances[i] != null)
            {
                DestroyBalloon(_allBalloonsInstances[i]);
            }
        }

        _allBalloonsInstances.Clear();
    }

    private void SaveProgress(int stars, int points)
    {
        var data = _dataHolder.Data;
        
        var levelData = data.CompletedLevels.Find(t => t.LevelIndex == _levelConfig.LevelIndex);
        bool wasCompletedBefore;
        // IF LEVEL ALREADY WAS COMPLETED
        if (levelData != null)
        {
            wasCompletedBefore = true;
            levelData.StarsCollected = stars;
            levelData.PointsCollected = points;
        }
        // IF LEVEL WASNT COMPLETED YET
        else
        {
            wasCompletedBefore = false;
            data.CompletedLevels.Add(new PlayerCompletedLevelData()
            {
                LevelIndex = _levelConfig.LevelIndex,
                PointsCollected = points,
                StarsCollected = stars
            });
            
        }
        _loadService.Save(data);
        
        if (!wasCompletedBefore)
        {
            _playerInventory.AddMoney(pointsCounter.Points);
        }
    }
}