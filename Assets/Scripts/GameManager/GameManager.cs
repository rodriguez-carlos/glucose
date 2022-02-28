using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public void LoadFirstScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(1);
    }

    public void LoadNextScene()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        int thisSceneIndex = thisScene.buildIndex;
        SceneManager.LoadScene(thisSceneIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game.");
        Application.Quit();
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
    }

    private void Update()
    {

    }





}
