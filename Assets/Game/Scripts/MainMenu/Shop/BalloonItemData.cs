using UnityEngine;

[CreateAssetMenu(fileName = "New Balloon Item", menuName = "Game/Balloon Item", order = 0)]
public class BalloonItemData : ScriptableObject
{
    public int Index;
    public int Price;
    public Sprite Sprite;
}