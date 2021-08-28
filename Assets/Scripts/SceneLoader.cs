using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex = 0;

    public void LoadMainMenu()
    {
        if(FindObjectOfType<GameSession>())
        {
            FindObjectOfType<GameSession>().ReturnGameSpeedToStarting();
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
    }
    public void LoadCurrentLevel()
    {
        FindObjectOfType<GameSession>().ReturnGameSpeedToStarting();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex < SceneManager.sceneCount - 2 )
        {
            Cursor.visible = false;
        }  
        FindObjectOfType<GameSession>().ReturnGameSpeedToStarting();
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadFirstLevel()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(1);
    }

    public void LoadRules()
    {
        SceneManager.LoadScene("Rules");
    }

    public int GetCurrentSceneIndex()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        return currentSceneIndex;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
