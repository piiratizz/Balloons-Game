using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPlayerView : MonoBehaviour
{
    [SerializeField] private Image avatarImage;
    [SerializeField] private TextMeshProUGUI nicknameText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private GameObject localPlayerOutline;
    
    public void Initialize(Sprite avatar, string nickname, int points, bool isLocalPlayer = false)
    {
        nicknameText.text = nickname;
        pointsText.text = points.ToString();
        avatarImage.sprite = avatar;
        localPlayerOutline.SetActive(isLocalPlayer);
    }
}