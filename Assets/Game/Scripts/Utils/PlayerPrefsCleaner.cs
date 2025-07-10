using UnityEditor;
using UnityEngine;

public static class PlayerPrefsCleaner
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    public static void ClearPlayerPrefs()
    {
        if (EditorUtility.DisplayDialog("Clear PlayerPrefs", "Are you sure?", "Yes", "No"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}