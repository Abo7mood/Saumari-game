using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] float speed;
    [SerializeField] float speed2;
    [SerializeField] float lifetime;
    [SerializeField] GameObject BulletParticles;
    GameObject BulletParticlesGameObject;
    [SerializeField] GameObject BulletBoom;
    CircleCollider2D _c;
    // Start is called before the first frame update
    private void Awake()
    {
        _c = GetComponent<CircleCollider2D>();
    }
    void Start()
    {
        if(gameObject.name == "Bullet 2(Clone)")
        {
            _c.enabled = false;
            Invoke("CricleActive", .2f);
            gameObject.transform.parent = null;
        }
        else
        Invoke("DestroyVoid", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name!= "Bullet 2(Clone)")
        transform.Translate(Vector2.left* speed * Time.deltaTime);
        else
           transform.Translate(Vector2.up * speed2 * Time.deltaTime);
    }
    private void DestroyVoid()
    {
      if(BulletBoom!=null)
            Instantiate(BulletBoom, transform.position, Quaternion.identity, null);


        BulletParticlesGameObject= Instantiate(BulletParticles, transform.position, Quaternion.identity, null);
        Destroy(gameObject);
        Destroy(BulletParticlesGameObject, 15f);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ground"))
            DestroyVoid();
    }
    private void CricleActive()
    {
        _c.enabled = true;
    }
}
