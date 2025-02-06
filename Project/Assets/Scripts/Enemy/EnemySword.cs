using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator anim;
    HealthBarFade fade;
    public float _speed;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        fade = FindObjectOfType<HealthBarFade>().GetComponent<HealthBarFade>();
        gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
    }
    private void Update()
    {
        _rb.velocity = transform.right * _speed;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;

            Destroy(gameObject, .25f);
            if(gameObject.name== "Sword(Clone)")
            fade.Damage(20);
            else
                fade.Damage(30);
        }


    }

}
