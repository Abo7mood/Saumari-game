using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _coins : MonoBehaviour
{
    Rigidbody2D _rb;
   [SerializeField] Sprite[] _sprites;
    SpriteRenderer _sprite;

    private void Awake() { _rb = GetComponent<Rigidbody2D>();  _sprite = GetComponent<SpriteRenderer>(); } 

    private void Start()
    {
        int Randomization = Random.Range(0, 11);

        _sprite.sprite = _sprites[Randomization];
    }

    [SerializeField] float _speed;
    private void FixedUpdate()
    {
        int randomization = Random.Range(0, 16);
     
        _rb.velocity = new Vector2(0, _speed * Time.deltaTime);
    }
}
