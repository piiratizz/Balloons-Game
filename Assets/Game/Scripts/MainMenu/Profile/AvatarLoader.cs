using UnityEngine;
using System.IO;

public class AvatarLoader
{
    private string _path;
    
    public AvatarLoader(string fileName)
    {
        _path = Path.Combine(Application.dataPath, fileName);
    }
    
    public void LoadAndSaveAvatar(string sourcePath)
    {
        if (!File.Exists(sourcePath))
        {
            Debug.LogError("File not founded: " + sourcePath);
            return;
        }

        byte[] pngData = File.ReadAllBytes(sourcePath);

        Texture2D texture = new Texture2D(2, 2);
        if (!texture.LoadImage(pngData))
        {
            Debug.LogError("Cant load png");
            return;
        }
        
        File.WriteAllBytes(_path, pngData);

        Debug.Log("Avatar saved" + _path);
    }
    
    public Sprite GetAvatar()
    {
        if (!File.Exists(_path))
        {
            Debug.LogWarning("Avatar file not found: " + _path);
            return null;
        }

        byte[] pngData = File.ReadAllBytes(_path);
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(pngData))
        {
            Sprite sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );
            return sprite;
        }
        Debug.LogError("Failed to load texture from PNG.");
        return default;
    }
}