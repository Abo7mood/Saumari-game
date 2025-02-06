using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealArrow : MonoBehaviour
{
    Transform Player;
    Rigidbody2D rb;
    [SerializeField] float _speed;
    const string Arrow = "Arrow";
    const string ArrowP = "ArrowP";
    const string Spear = "Spear";

    private void Awake() => rb = GetComponent<Rigidbody2D>();


    private void Start()
    {
        if (gameObject.name == "ArrowP(Clone)")
            gameObject.name = ArrowP;
        else if (gameObject.name == "Spear(Clone)")
            gameObject.name = Spear;
        else
            gameObject.name = Arrow;
    }
    private void Update()
    {
        rb.velocity = -transform.right * _speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(gameObject, .5f);
           
        }

           
    }
}
