using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private float _backgroundLenght;
    
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
        
    }

    private void Update()
    {
        // Повторяем один и тот же бэк за одно и то же время
        float newPosition = Mathf.Repeat(Time.time * _scrollSpeed, _backgroundLenght);
        transform.position = _startPosition + Vector3.forward * newPosition;
    }
}
