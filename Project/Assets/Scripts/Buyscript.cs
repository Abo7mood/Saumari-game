using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyscript : MonoBehaviour
{
    PlayerInfo _playerinfo;

        
       [SerializeField] GameObject[] Objects;


    private void Awake()
    {
        _playerinfo = FindObjectOfType<PlayerInfo>().GetComponent<PlayerInfo>();

    }

    public void Buy(int coinsAmount)
    {
        if (_playerinfo.RealCoins >= coinsAmount)
        {

            Objects[0].SetActive(true);
            Objects[1].SetActive(false);
            Objects[2].SetActive(false);
            GameManager.instance.BuyCharacter();
          
            
        }
        else
            return;

    }
    
}
