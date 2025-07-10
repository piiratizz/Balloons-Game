using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AvailableLevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private List<GameObject> stars;
    [SerializeField] private Button levelBtn;

    private int _levelIndex;
    
    public event Action<int> OnLevelSelected;
    
    public void Initialize(int number, int starsCount)
    {
        foreach (var star in stars)
        {
            star.SetActive(false);
        }
        
        _levelIndex = number;
        levelNumberText.text = number.ToString();
        for (int i = 0; i < starsCount; i++)
        {
            stars[i].SetActive(true);
        }

        levelBtn.onClick = new Button.ButtonClickedEvent();
        levelBtn.onClick.AddListener(LevelSelected);
    }

    private void LevelSelected()
    {
        OnLevelSelected?.Invoke(_levelIndex);
    }
}