using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Attacks : MonoBehaviour
{
    PlayerController _Player;
    public event EventHandler OnPlayerSpawn;
   [SerializeField] Image[] AttacksImages;
    [SerializeField] Button[] _buttons;
    private float speed=.15f;
    private void Awake()
    {
        OnPlayerSpawn += _testOnplayerSpawn;
     
    }
   
    public void Attack1()
    {
        StartCoroutine(Attack1cooldown());
        AttacksImages[0].fillAmount = 0;
        AttacksImages[1].fillAmount = 0;
        _Player.Attack();
    }
    public void Attack2()
    {
        AttacksImages[2].fillAmount = 0;
        AttacksImages[3].fillAmount = 0;
        _Player.Attack2();
     
    }
    public void _testOnplayerSpawn(object sender,EventArgs e)
    {
     
        _Player = FindObjectOfType<PlayerController>();
      
      
    }
    private void Update()
    {
        if (AttacksImages[3].fillAmount >= 1)
            _buttons[1].enabled = true;
                else
            _buttons[1].enabled = false;


        OnPlayerSpawn?.Invoke(this, EventArgs.Empty);
        if (AttacksImages[0].fillAmount > 1|| AttacksImages[1].fillAmount > 1)
        {
            AttacksImages[0].fillAmount = 1;
            AttacksImages[1].fillAmount = 1;
        }

        else
        {
            AttacksImages[0].fillAmount += Time.deltaTime * 1;
            AttacksImages[1].fillAmount += Time.deltaTime * 1;
        }
            

        if (AttacksImages[2].fillAmount > 1|| AttacksImages[3].fillAmount > 1)
        {
            AttacksImages[2].fillAmount = 1;
            AttacksImages[3].fillAmount = 1;

        }

        else
        {
            AttacksImages[2].fillAmount += Time.deltaTime * speed;
            AttacksImages[3].fillAmount += Time.deltaTime * speed;
        }
    
     
    }
    IEnumerator Attack1cooldown()
    {
        _buttons[0].enabled = false;
        yield return new WaitForSeconds(1f);
        _buttons[0].enabled = true;
    }
 
}
