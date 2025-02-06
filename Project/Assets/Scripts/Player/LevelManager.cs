using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private GameObject RealPlayer;
 
    [SerializeField] GameObject[] Characters;
    [SerializeField] GameObject[] Enemies;
    [SerializeField] int RealEnemies;
    [SerializeField] Transform SpawnMark;
    [SerializeField] Transform[] SpawnE;
    public int PlayerNumber;
    [HideInInspector] public float RespawnTime;
    private Vector2 Spawn;
    private bool CanSpawn;
    public enum Chars
    {
        MedievalWarrior, Hunterss,Hunterss2, MartialHero, MartialHero2, MartialHero3,Wizard
    }

    //public GameObject[] Heals;
    //public Transform[] Healspawn;
    //public GameObject Heal;


    //[SerializeField] GameObject Boom;
    //[SerializeField] GameObject BoomPrefab;
    //[HideInInspector] public int SpawnTime = 10;

    // Start is called before the first frame update
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
        GameManager.instance.CharacterNumber = PlayerPrefs.GetInt("PlayerNumber", 0);
        PlayerNumber = GameManager.instance.CharacterNumber;

    }
    private void Start()
    {

        Invoke(nameof(InstantiatePlayer), RespawnTime);
        Invoke(nameof(InstantiateEnemy),.1f);
    }
   
    public void InstantiatePlayer()
    {
        Spawn = SpawnMark.position;
        RealPlayer = Instantiate(Characters[PlayerNumber], Spawn, Quaternion.identity, null);
    }
    public void InstantiateEnemy()
    {
        for (int i = 0; i < RealEnemies; i++)
              Instantiate(Enemies[i], SpawnE[i].position, Quaternion.identity, null);      
         
    }

    private void booltrue() => CanSpawn = true;

    //void instantiateHeal()
    //{
    //    int rand = Random.Range(0, 6);
    //    if (Healspawn[rand] != null && Healspawn[rand].transform.Find("Heal(Clone)") == null)
    //        Instantiate(Heal, Healspawn[rand].position, Quaternion.identity, Healspawn[rand].transform);
    //    else
    //        return;
    //    StartCoroutine(disableboolvirus());



    //}


}
