using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerInfo : MonoBehaviour
{
    [HideInInspector] public int _coins = 0;
    private int Coins = 0;
    [HideInInspector] public int RealCoins;
    TextMeshProUGUI CoinsText;

    [SerializeField] GameObject Note;
    private void Awake()
    {
        CoinsText = GetComponent<TextMeshProUGUI>();
        InvokeRepeating(nameof(Caluclate), 0f, 1f);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {       
        LoadData();

       

    }
    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Q))
            AddCoins(1000);
        if (_coins != Coins)
        {
            Coins = _coins;
            LoadData();
        }

    }
    public void LoadData()
    {       
            CoinsText.text = _coins.ToString();       
    }
    public void AddCoins(int coinsAmount)
    {
        _coins += coinsAmount;
    }
    public void MinusCoins(int coinsAmount)
    {
        if (_coins >= coinsAmount)
        {

            _coins -= coinsAmount;
        }

        else
            StartCoroutine(NoteEnable());
;    }
  
    IEnumerator NoteEnable()
    {
        Note.SetActive(true);
        yield return new WaitForSeconds(2f);
        Note.SetActive(false);
    }
    public void Caluclate() => RealCoins = _coins;
}
