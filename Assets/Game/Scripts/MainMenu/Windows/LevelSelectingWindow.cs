using UnityEngine;

public class LevelSelectingWindow : WindowBase
{
    [SerializeField] private LevelSelectingManager levelsManager;

    public override void Open()
    {
        base.Open();
        levelsManager.CreateLevels();
    }

    public override void Close()
    {
        base.Close();
        levelsManager.ClearLevels();
    }
}