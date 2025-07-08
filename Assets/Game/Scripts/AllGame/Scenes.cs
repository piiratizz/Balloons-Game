public class Scenes
{
    public const string Boot = "Boot";
    public const string MainMenu = "MainMenu";

    public string GetLevelScene(LevelConfig config)
    {
        return $"Level{config.LevelIndex}";
    }
}