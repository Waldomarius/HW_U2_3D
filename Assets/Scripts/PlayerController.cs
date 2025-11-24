using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Serializable]
    public class Boundary
    {
        [SerializeField] internal float xMin;
        [SerializeField] internal float xMax;
        [SerializeField] internal float yMin;
        [SerializeField] internal float yMax;
    }
    
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _tilt = 10f;
    
    [SerializeField] private Boundary _boundary;
    [SerializeField] private GameObject _explosion;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        _rigidbody.velocity = movement * _speed;
    
        // Задаем координаты в доступном интервале.
        _rigidbody.position = new Vector3(
            Mathf.Clamp(_rigidbody.position.x, _boundary.xMin, _boundary.xMax),
            0.0f,
            Mathf.Clamp(_rigidbody.position.z, _boundary.yMin, _boundary.yMax)
            );
        
        _rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, _rigidbody.velocity.x * -_tilt);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
            
        // Астероид столкнулся с игроком
        if (go.tag is "Asteroid" && gameObject.tag is "Player")
        {
            Destroy(gameObject);
            Instantiate(_explosion, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

}
