using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject
{
    public int LevelIndex;
    public int Points;

    public float balloonsMinSpeed;
    public float balloonsMaxSpeed;
    public int totalBalloons;
    public float secondsDelayBetweenSpawn;
    public int deadBalloonSpawnPercentChance;
}