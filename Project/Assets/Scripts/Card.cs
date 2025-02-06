using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName ="New Card",menuName ="Card")]
public class Card : ScriptableObject
{
   public  string Name;
    public int Damage;
    public int Health;
    public int Price;
    public Sprite CharacterImage;

}
