using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    public void SetProgress(float progress)
    {
        progressBar.value = progress;
    }
}