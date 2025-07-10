using System;
using UnityEngine;
using UnityEngine.UI;

public class Balloon : MonoBehaviour, IClickable
{
    [SerializeField] private bool isDeadBalloon;
    [SerializeField] private SpriteRenderer skinImage;
    
    private const string DestroyerTag = "BalloonsDestroyer";
    
    private float _speed;

    private bool _initialized;

    //Returned sender, isDeadBalloon
    public event Action<Balloon, bool> OnBalloonClicked; 
    public event Action<Balloon> OnDestroyerCollided; 
    
    public void Initialize(Sprite skin, float speed)
    {
        _speed = speed;
        _initialized = true;
        
        if(isDeadBalloon) return;

        skinImage.sprite = skin;
    }
    
    private void Update()
    {
        if(!_initialized) return;
        
        transform.Translate(0,_speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(DestroyerTag))
        {
            OnDestroyerCollided?.Invoke(this);
        }
    }

    public void OnClick()
    {
        OnBalloonClicked?.Invoke(this, isDeadBalloon);
    }

    private void OnDestroy()
    {
        OnBalloonClicked = null;
        OnDestroyerCollided = null;
    }
}