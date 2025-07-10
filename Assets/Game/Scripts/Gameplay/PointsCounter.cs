using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    private int _totalPoints;

    public int Points => _totalPoints;
    
    private void Start()
    {
        UpdateUI();
    }

    public void Add(int points)
    {
        _totalPoints += points;
        UpdateUI();
    }

    private void UpdateUI()
    {
        pointsText.text = _totalPoints.ToString();
    }
}