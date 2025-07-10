using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private MainMenuUIRoot _mainMenu;

    public void RunScene()
    {
        _mainMenu.Initialize();
    }
}