using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Flag : MonoBehaviour
{
    private int nextscene;
    private int sceneint;
    [SerializeField] GameObject WinPanel;
    private void Start()
    {
        sceneint= SceneManager.GetActiveScene().buildIndex;
        nextscene = SceneManager.GetActiveScene().buildIndex + 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if(SceneManager.GetActiveScene().buildIndex==1 )
            SceneManager.LoadScene(nextscene);
            else
            {
                Invoke(nameof(LoadMainMenu), 1f);
                WinPanel.SetActive(true);
            }
        }
    }
    public void LoadMainMenu() => SceneManager.LoadScene(0);
}
