using UnityEngine;

public abstract class WindowBase : MonoBehaviour
{
    [SerializeField] private WindowTypes windowType;
    [SerializeField] private GameObject content;
    
    public WindowTypes Type => windowType;
    
    public virtual void Open() { content.SetActive(true); }
    public virtual void Close() { content.SetActive(false); }
}
