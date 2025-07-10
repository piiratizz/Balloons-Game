using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardProfileView : MonoBehaviour
{
    [SerializeField] private Image avatar;
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private List<Image> starsImages;

    public void Initialize(Sprite avatarSprite, string nickname, int points, int averageStars)
    {
        nicknameText.text = nickname;
        pointsText.text = points.ToString();

        if (avatarSprite != null)
        {
            avatar.sprite = avatarSprite;
        }
        
        foreach (var star in starsImages)
        {
            star.gameObject.SetActive(false);
        }
        
        for (int i = 0; i < averageStars; i++)
        {
            starsImages[i].gameObject.SetActive(true);
        }
    }
}