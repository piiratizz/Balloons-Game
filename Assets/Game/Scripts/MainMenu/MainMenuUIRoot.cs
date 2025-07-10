using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIRoot : StartInitializable
{
    [SerializeField] private List<StartInitializable> initializableChilds;
    
    public override void Initialize()
    {
        foreach (var child in initializableChilds)
        {
            child.Initialize();
        }
    }
}