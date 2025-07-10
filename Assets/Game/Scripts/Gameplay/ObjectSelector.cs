using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private Camera camera;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit == null) return;
            
            if (hit.TryGetComponent(out IClickable clickable))
            {
                clickable.OnClick();
            }
        }
    }
}