using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private PointsCounter pointsCounter;
    
    [Header("Level Failed Popup")]
    [SerializeField] private GameObject levelFailedPopup;
    [SerializeField] private TextMeshProUGUI levelFailedPointsText;
    [SerializeField] private Button levelFailedHomeBtn;
    [SerializeField] private Button levelFailedRetryBtn;
    
    [Header("Level Completed Popup")]
    [SerializeField] private GameObject levelCompletedPopup;
    [SerializeField] private TextMeshProUGUI levelCompletedPointsText;
    [SerializeField] private Button levelCompletedHomeBtn;
    [SerializeField] private List<GameObject> stars;
    
    private SceneLoaderManager _sceneLoaderManager;
    
    private void Start()
    {
        foreach (var star in stars)
        {
            star.SetActive(false);
        }
        
        _sceneLoaderManager = ServiceLocator.Get<SceneLoaderManager>();
        
        levelFailedPopup.gameObject.SetActive(false);
        levelCompletedPopup.gameObject.SetActive(false);

        levelCompletedHomeBtn.onClick = new Button.ButtonClickedEvent();
        levelCompletedHomeBtn.onClick.AddListener(() => _sceneLoaderManager.LoadMainMenu());
        
        levelFailedHomeBtn.onClick = new Button.ButtonClickedEvent();
        levelFailedHomeBtn.onClick.AddListener(() => _sceneLoaderManager.LoadMainMenu());
        
        levelFailedRetryBtn.onClick = new Button.ButtonClickedEvent();
        levelFailedRetryBtn.onClick.AddListener(() => _sceneLoaderManager.LoadScene(SceneManager.GetActiveScene().name));
    }
    
    public void ShowLevelFailedPopup()
    {
        levelFailedPopup.gameObject.SetActive(true);
        levelFailedPointsText.text = pointsCounter.Points.ToString();
    }

    public void ShowLevelCompletedPopup()
    {
        levelCompletedPopup.gameObject.SetActive(true);
        levelCompletedPointsText.text = pointsCounter.Points.ToString();
    }

    public void ShowStars(int starsCount)
    {
        for (int i = 0; i < starsCount; i++)
        {
            stars[i].SetActive(true);
        }
    }
}