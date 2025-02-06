using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static Weapon instance;
    [HideInInspector] public Vector3 direction;
    [SerializeField] bool m_FacingRight;
    public float offeset;
    [HideInInspector] public float rotZ;
    SpriteRenderer sprite;
    [Space(15f)]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletprefab;
    [Space(15f)]
    [SerializeField] GameObject bullet1;
    [SerializeField] GameObject bulletprefab1;
    [SerializeField] GameObject WeaponTransform;
    private void Awake()
    {
        instance = this;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ + offeset);

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 90)
            sprite.flipY = false;

        if (transform.eulerAngles.z > -90 && transform.eulerAngles.z < 0)
            sprite.flipY = false;

       else if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 180)
            sprite.flipY = true;

        else if (transform.eulerAngles.z > -180 && transform.eulerAngles.z < -90)
            sprite.flipY = true;

    }

    public void ShootBullet() => bulletprefab = Instantiate(bullet, WeaponTransform.transform.position, transform.rotation, null);
    public void ShootBullet1() => bulletprefab1 = Instantiate(bullet1, WeaponTransform.transform.position, transform.rotation, null);

}
