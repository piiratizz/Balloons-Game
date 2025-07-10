using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private BalloonsManager balloonsManager;
    [SerializeField] private int levelIndex;
    
    private void Start()
    {
        LevelConfig levelConfig = Resources.Load($"Levels/Level {levelIndex}") as LevelConfig;
        balloonsManager.Initialize(levelConfig);
        balloonsManager.StartSpawnLoop();
    }
}