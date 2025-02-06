using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    Scene currentScene;
    string sceneName;
    public void LoadSceneFight() => SceneManager.LoadScene(1);

    public void Menu() => SceneManager.LoadScene(0);

    public void Quit() => Application.Quit();

   

    void Start()
    {

         currentScene = SceneManager.GetActiveScene();


         sceneName = currentScene.name;

       


    }
    public void LoadScene() => SceneManager.LoadScene(sceneName);

}
