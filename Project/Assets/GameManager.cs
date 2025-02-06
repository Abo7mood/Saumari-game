using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public int CharacterNumber;
    public GameObject[] Equips;
    public GameObject[] ObjectData;
    bool[] DataStore = new bool[19];
    bool isBuy;

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
    private void OnEnable()
    {


        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            GetData();
          
            if (isBuy==false)
                CheckData();

            ActiveData();



        }
        else
            return;

    }
    public void SetCharacter(int charnumber)
    {
        for (int i = 0; i < Equips.Length; i++)
        {
            Equips[i].SetActive(true);
        }
        CharacterNumber = charnumber;
       
            
        Equips[charnumber].SetActive(false);
        PlayerPrefs.SetInt("PlayerNumber", CharacterNumber);
    }
    public void BuyCharacter()
    {

        isBuy = true;
        CheckData();
        StoreData();
        GetData();

    }
    public void GetData()
    {
        GameManager.instance.DataStore[0] = (PlayerPrefs.GetInt("DataStore0") != 0);
        GameManager.instance.DataStore[1] = (PlayerPrefs.GetInt("DataStore1") != 0);
        GameManager.instance.DataStore[2] = (PlayerPrefs.GetInt("DataStore2") != 0);
        GameManager.instance.DataStore[3] = (PlayerPrefs.GetInt("DataStore3") != 0);
        GameManager.instance.DataStore[4] = (PlayerPrefs.GetInt("DataStore4") != 0);
        GameManager.instance.DataStore[5] = (PlayerPrefs.GetInt("DataStore5") != 0);
        GameManager.instance.DataStore[6] = (PlayerPrefs.GetInt("DataStore6") != 0);
        GameManager.instance.DataStore[7] = (PlayerPrefs.GetInt("DataStore7") != 0);
        GameManager.instance.DataStore[8] = (PlayerPrefs.GetInt("DataStore8") != 0);
        GameManager.instance.DataStore[9] = (PlayerPrefs.GetInt("DataStore9") != 0);
        GameManager.instance.DataStore[10] = (PlayerPrefs.GetInt("DataStore10") != 0);
        GameManager.instance.DataStore[11] = (PlayerPrefs.GetInt("DataStore11") != 0);
        GameManager.instance.DataStore[12] = (PlayerPrefs.GetInt("DataStore12") != 0);
        GameManager.instance.DataStore[13] = (PlayerPrefs.GetInt("DataStore13") != 0);
        GameManager.instance.DataStore[14] = (PlayerPrefs.GetInt("DataStore14") != 0);
        GameManager.instance.DataStore[15] = (PlayerPrefs.GetInt("DataStore15") != 0);
        GameManager.instance.DataStore[16] = (PlayerPrefs.GetInt("DataStore16") != 0);
        GameManager.instance.DataStore[17] = (PlayerPrefs.GetInt("DataStore17") != 0);
        GameManager.instance.DataStore[18] = (PlayerPrefs.GetInt("DataStore18") != 0);
        isBuy = (PlayerPrefs.GetInt("IsBuy") != 0);
        Debug.Log("LoadData");
    }
    public void StoreData()
    {
        PlayerPrefs.SetInt("DataStore0", (GameManager.instance.DataStore[0] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore1", (GameManager.instance.DataStore[1] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore2", (GameManager.instance.DataStore[2] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore3", (GameManager.instance.DataStore[3] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore4", (GameManager.instance.DataStore[4] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore5", (GameManager.instance.DataStore[5] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore6", (GameManager.instance.DataStore[6] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore7", (GameManager.instance.DataStore[7] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore8", (GameManager.instance.DataStore[8] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore9", (GameManager.instance.DataStore[9] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore10", (GameManager.instance.DataStore[10] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore11", (GameManager.instance.DataStore[11] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore12", (GameManager.instance.DataStore[12] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore13", (GameManager.instance.DataStore[13] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore14", (GameManager.instance.DataStore[14] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore15", (GameManager.instance.DataStore[15] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore16", (GameManager.instance.DataStore[16] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore17", (GameManager.instance.DataStore[17] ? 1 : 0));
        PlayerPrefs.SetInt("DataStore18", (GameManager.instance.DataStore[18] ? 1 : 0));
        PlayerPrefs.SetInt("IsBuy", (isBuy ? 1 : 0));
        Debug.Log("StoreData");


    }
    public void CheckData()
    {
        DataStore[0] = ObjectData[0].activeSelf;
        DataStore[1] = ObjectData[1].activeSelf;
        DataStore[2] = ObjectData[2].activeSelf;
        DataStore[3] = ObjectData[3].activeSelf;
        DataStore[4] = ObjectData[4].activeSelf;
        DataStore[5] = ObjectData[5].activeSelf;
        DataStore[6] = ObjectData[6].activeSelf;
        DataStore[7] = ObjectData[7].activeSelf;
        DataStore[8] = ObjectData[8].activeSelf;
        DataStore[9] = ObjectData[9].activeSelf;
        DataStore[10] = ObjectData[10].activeSelf;
        DataStore[11] = ObjectData[11].activeSelf;
        DataStore[12] = ObjectData[12].activeSelf;
        DataStore[13] = ObjectData[13].activeSelf;
        DataStore[14] = ObjectData[14].activeSelf;
        DataStore[15] = ObjectData[15].activeSelf;
        DataStore[16] = ObjectData[16].activeSelf;
        DataStore[17] = ObjectData[17].activeSelf;
        DataStore[18] = ObjectData[18].activeSelf;
        Debug.Log("CheckData");
    }
    public void ActiveData()
    {
        ObjectData[0].SetActive(DataStore[0]);
        ObjectData[1].SetActive(DataStore[1]);
        ObjectData[2].SetActive(DataStore[2]);
        ObjectData[3].SetActive(DataStore[3]);
        ObjectData[4].SetActive(DataStore[4]);
        ObjectData[5].SetActive(DataStore[5]);
        ObjectData[6].SetActive(DataStore[6]);
        ObjectData[7].SetActive(DataStore[7]);
        ObjectData[8].SetActive(DataStore[8]);
        ObjectData[9].SetActive(DataStore[9]);
        ObjectData[10].SetActive(DataStore[10]);
        ObjectData[11].SetActive(DataStore[11]);
        ObjectData[12].SetActive(DataStore[12]);
        ObjectData[13].SetActive(DataStore[13]);
        ObjectData[14].SetActive(DataStore[14]);
        ObjectData[15].SetActive(DataStore[15]);
        ObjectData[16].SetActive(DataStore[16]);
        ObjectData[17].SetActive(DataStore[17]);
        ObjectData[18].SetActive(DataStore[18]);
        Debug.Log("ActiveData");
    }
    public void FixData()
    {
        DataStore[0] = true;
        DataStore[1] = true;
        DataStore[2] = true;
        DataStore[3] = true;
        DataStore[4] = true;
        DataStore[5] = true;
        DataStore[6] = true;
        DataStore[7] = true;
        DataStore[8] = true;
        DataStore[9] = true;
        DataStore[10] = true;
        DataStore[11] = true;
        DataStore[12] = true;
        DataStore[13] = true;
        DataStore[14] = true;
        DataStore[15] = true;
        DataStore[16] = true;
        DataStore[17] = true;
        DataStore[18] = true;
        
        Debug.Log("FixData");
    }
}
