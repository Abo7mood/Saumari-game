using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class SaveData : MonoBehaviour
{
    public static SaveData instance;
   [SerializeField] TextMeshProUGUI coins;
    PlayerInfo _playerInfo;
   [SerializeField] GameManager manager;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
            {
                Destroy(instance.gameObject);
                instance = this;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene(0);
        }
       
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Delete();
        }
          
    }
    public void OnEnable()=> Load();

    public void Save() => PlayerPrefs.SetInt("Coins", _playerInfo._coins);


    public void Delete() => PlayerPrefs.DeleteAll();

    private void Load()
    {
        _playerInfo = FindObjectOfType<PlayerInfo>().GetComponent<PlayerInfo>();
        if (manager != null)
        {
            _playerInfo._coins = PlayerPrefs.GetInt("Coins", 0);
            coins.text = _playerInfo._coins.ToString();
        }
      
       


    }
  
}
