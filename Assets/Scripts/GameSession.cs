using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameSession : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject levelCompletedMenu;
    [SerializeField] GameObject lastMainMenuCanvas;
    [SerializeField] GameObject lastQuitGameCanvas;
    [SerializeField] GameObject gameOverMenuButtonSelected;
    [SerializeField] GameObject pauseMenuButtonSelected;
    [SerializeField] GameObject levelCompletedMenuButtonSelected;
    [SerializeField] GameObject lastMainMenuButtonSelected;
    [SerializeField] GameObject lastQuitGameButtonSelected;

    SFXPlayer mySFXPlayer;

    float startingGameSpeed = 1f;
    bool pauseMenuActive = false;
    bool levelComplete = false;
    bool lastCanvasOn = false;
    bool gameOverMenuActive = false;
    bool levelCompletedMenuActive = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        mySFXPlayer = FindObjectOfType<SFXPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        PauseProcess();
    }

    public void GameOverProcess()
    {
        if(!levelComplete)
        {
            StartCoroutine(GameOverCoroutine());
        }
       else
        {
            return;
        }
    }

    public void PauseProcess()
    {
        if(Input.GetKeyDown("escape") && !pauseMenuActive && !levelComplete && !lastCanvasOn)
        {
            pauseMenuActive = true;
            pauseMenu.SetActive(true);
            GameSpeedToZeroAndShowCursor();
            ButtonSelected();
        }
        else if(Input.GetKeyDown("escape") && pauseMenuActive && !levelComplete && !lastCanvasOn)
        {
            pauseMenuActive = false;
            pauseMenu.SetActive(false);
            GameSpeedToStartingAndHideCursor();
        }
    }

    public void LevelCompletedProcess()
    {
        if (levelComplete == false) ;
        StartCoroutine(LevelCompleteCoroutine());
    }

    public void ResumeGame()
    {
        pauseMenuActive = false;
        pauseMenu.SetActive(false);
        GameSpeedToStartingAndHideCursor();
    }

    public void ReturnGameSpeedToStarting()
    {
        Time.timeScale = startingGameSpeed;
    }

    IEnumerator LevelCompleteCoroutine()
    {
        levelComplete = true;
        levelCompletedMenuActive = true;
        mySFXPlayer.StopMainEngineSFX();
        FindObjectOfType<Movement>().GetComponent<Movement>().enabled = false;
        FindObjectOfType<CollisionHandler>().StartSuccessVFX();
        mySFXPlayer.PlayLevelCompletedSFX();
        yield return new WaitForSeconds(1f);
        levelCompletedMenu.SetActive(true);
        GameSpeedToZeroAndShowCursor();
        ButtonSelected();
    }

    IEnumerator GameOverCoroutine()
    {
        levelComplete = true;
        gameOverMenuActive = true;
        mySFXPlayer.StopMainEngineSFX();
        FindObjectOfType<Movement>().GetComponent<Movement>().enabled = false;
        FindObjectOfType<CollisionHandler>().StartExplosionVFX();
        mySFXPlayer.PlayGameOverSFX();
        yield return new WaitForSeconds(1f);
        GameSpeedToZeroAndShowCursor();
        gameOverMenu.SetActive(true);
        ButtonSelected();
    }

    void GameSpeedToZeroAndShowCursor()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    void GameSpeedToStartingAndHideCursor()
    {
        Cursor.visible = false;
        Time.timeScale = startingGameSpeed;
    }

    void ButtonSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if(pauseMenu.activeInHierarchy && !lastCanvasOn)
        {
            EventSystem.current.SetSelectedGameObject(pauseMenuButtonSelected);
        }
        else if(gameOverMenu.activeInHierarchy && !lastCanvasOn)
        {
            EventSystem.current.SetSelectedGameObject(gameOverMenuButtonSelected);
        }
        else if(levelCompletedMenu.activeInHierarchy && !lastCanvasOn)
        {
            EventSystem.current.SetSelectedGameObject(levelCompletedMenuButtonSelected);
        }
    }

    void LastCanvasButtonSelected()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if(lastMainMenuCanvas.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(lastMainMenuButtonSelected);
        }
        else if(lastQuitGameCanvas.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(lastQuitGameButtonSelected);
        }
    }

    
    public void ShowLastMainMenuCanvas()
    {
        lastCanvasOn = true;
        ShowOrHideCanvas(false);
        EventSystem.current.SetSelectedGameObject(null);
        lastMainMenuCanvas.SetActive(true);
        LastCanvasButtonSelected();
    }

    public void ShowLastQuitGameCanvas()
    {
        lastCanvasOn = true;
        ShowOrHideCanvas(false);
        EventSystem.current.SetSelectedGameObject(null);
        lastQuitGameCanvas.SetActive(true);
        LastCanvasButtonSelected();
    }

    public void NoButton()
    {
        if(lastMainMenuCanvas.activeInHierarchy)
        {
            lastCanvasOn = false;
            ShowPreviousCanvas();
            ButtonSelected();
        }
        else if(lastQuitGameCanvas.activeInHierarchy)
        {
            lastCanvasOn = false;
            ShowPreviousCanvas();
            ButtonSelected();
        }
    }

    void ShowOrHideCanvas(bool active)
    {
        if (pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(active);
        }
        else if (gameOverMenu.activeInHierarchy)
        {
            gameOverMenu.SetActive(active);
        }
        else if (levelCompletedMenu.activeInHierarchy)
        {
            levelCompletedMenu.SetActive(active);
        }
    }

    void ShowPreviousCanvas()
    {
        if(lastMainMenuCanvas.activeInHierarchy)
        {
            lastMainMenuCanvas.SetActive(false);
            if (pauseMenuActive)
            {
                pauseMenu.SetActive(true);
            }
            else if(gameOverMenuActive)
            {
                gameOverMenu.SetActive(true);
            }
            else if(levelCompletedMenuActive)
            {
                levelCompletedMenu.SetActive(true);
            }
        }
        else if(lastQuitGameCanvas.activeInHierarchy)
        {
            lastQuitGameCanvas.SetActive(false);
            if(pauseMenuActive)
            {
                pauseMenu.SetActive(true);
            }
            else if(gameOverMenuActive)
            {
                gameOverMenu.SetActive(true);
            }
            else if(levelCompletedMenuActive)
            {
                levelCompletedMenu.SetActive(true);
            }
        }
    }
}
