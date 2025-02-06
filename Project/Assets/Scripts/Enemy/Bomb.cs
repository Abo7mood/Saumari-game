using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator anim;
    HealthBarFade fade;
    Rigidbody2D rb;
    Vector2 direction;
    [SerializeField] float x, y;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fade = FindObjectOfType<HealthBarFade>().GetComponent<HealthBarFade>();
    }
    private void Start()
    {
        direction = new Vector2(x, y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fade.Damage(20);
            anim.SetTrigger("Dead");
            Destroy(this.gameObject, .8f);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

        }else if(collision.gameObject.layer==9)
        {
            anim.SetTrigger("Dead");
            Destroy(this.gameObject, .8f);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }
    private void FixedUpdate()
    {
        
        rb.AddForce(direction*Time.deltaTime, ForceMode2D.Impulse);
    }
}
