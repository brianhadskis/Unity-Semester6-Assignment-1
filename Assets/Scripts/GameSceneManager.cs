using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
    public GameObject goalTrigger;

    public GameObject player;

    public GameObject goldenSnitch;
    public GameObject laser;
    public GameObject borg;

    public CinemachineStateDrivenCamera stateDrivenCamera;
    
    public CinemachineVirtualCamera cutscenePanoramicCamera;
    public CinemachineVirtualCamera cutsceneZoomCamera;
    public CinemachineVirtualCamera cutsceneFlyoverCamera;

    public GameObject playUI;
    public GameObject mobileUI;
    public GameObject gameoverUI;

    public TMP_Text gameoverText;

    public TMP_Text tmTimer;
    public TMP_Text tmHighScore;

    private GameInputActions gameInputActions;
    private bool isStarted;
    private int currentTime;
    private int highScore;

    private int START_TIME = 150;

    private string gameOverMessage = "Did you win?";



    private void Awake()
    {
        currentTime = START_TIME;
        gameInputActions = new GameInputActions();

    }


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            highScore = 0;
        }

        if (PlayerPrefs.HasKey("TimesPlayed"))
        {
            int tempTimesPlayed = PlayerPrefs.GetInt("TimesPlayed") + 1;
            PlayerPrefs.SetInt("TimesPlayed", tempTimesPlayed);
        }
        else
        {
            PlayerPrefs.SetInt("TimesPlayed", 1);
        }

        //cutsceneZoomCamera.gameObject.GetComponent<Animator>().StopPlayback();
        //cutsceneFlyoverCamera.gameObject.GetComponent<Animator>().StopPlayback();

        tmHighScore.text = highScore.ToString();

        OnBeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        tmTimer.text = currentTime.ToString();

        if (currentTime <= START_TIME - 5 && isStarted)
        {
            goldenSnitch.transform.parent.gameObject.SetActive(true);
            laser.SetActive(true);
            borg.transform.parent.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        goalTrigger.GetComponent<GameOver>().OnEndGame += OnEndGame;
        goldenSnitch.GetComponent<GameOver>().OnEndGame += OnEndGame;
        laser.GetComponent<GameOver>().OnEndGame += OnEndGame;
        borg.GetComponent<GameOver>().OnEndGame += OnEndGame;

        cutscenePanoramicCamera.gameObject.GetComponent<CutsceneHelper>().OnStartZoom += OnStartZoom;
        cutsceneZoomCamera.gameObject.GetComponent<CutsceneHelper>().OnStartFlyover += OnStartFlyover;
        cutsceneFlyoverCamera.gameObject.GetComponent<CutsceneHelper>().OnBeginGame += OnBeginGame;
    }

    private void OnDisable()
    {
        //goalTrigger.GetComponent<GameOver>().OnEndGame -= OnEndGame;

        cutscenePanoramicCamera.gameObject.GetComponent<CutsceneHelper>().OnStartZoom -= OnStartZoom;
        cutsceneZoomCamera.gameObject.GetComponent<CutsceneHelper>().OnStartFlyover -= OnStartFlyover;
        cutsceneFlyoverCamera.gameObject.GetComponent<CutsceneHelper>().OnBeginGame -= OnBeginGame;
    }

    public void OnBeginGame()
    {
        if (!isStarted)
        {
            StartCoroutine(RunTimer());
        }

        cutsceneFlyoverCamera.gameObject.GetComponent<Animator>().StopPlayback();
        player.GetComponent<ThirdPersonController>().EnableInput();

        playUI.SetActive(true);
        //mobileUI.SetActive(true);
    }

    public void OnStartZoom()
    {
        //cutscenePanoramicCamera.gameObject.GetComponent<Animator>().StopPlayback();
        cutsceneZoomCamera.gameObject.GetComponent<Animator>().SetTrigger("doZoom");
    }

    public void OnStartFlyover()
    {
        //cutsceneZoomCamera.gameObject.GetComponent<Animator>().StopPlayback();
        /*cutsceneFlyoverCamera.gameObject.GetComponent<Animator>().StartPlayback();
        cutsceneFlyoverCamera.gameObject.GetComponent<Animator>().Play("CM Cutscene Fly To Target Animation");*/

        cutsceneFlyoverCamera.gameObject.GetComponent<Animator>().SetTrigger("doFlyover");
    }

    public void ToggleCamera()
    {
        stateDrivenCamera.gameObject.GetComponent<Animator>().SetTrigger("ToggleCamera");
    }

    IEnumerator RunTimer()
    {
        isStarted = true;

        while (isStarted)
        {
            yield return new WaitForSeconds(1);

            currentTime--;
            //Debug.Log("Another second");

            // Times up! End the game.
            if (currentTime <= 0)
            {
                GameOver(global::GameOver.GameOverType.lose_time);
            }
        }
    }

    public void GoHome()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void OnEndGame(GameOver.GameOverType condition)
    {
        GameOver(condition);
    }

    private void GameOver(GameOver.GameOverType condition)
    {
        Debug.Log("Game over event");
        isStarted = false;
        //player.GetComponent<PlayerController>().DisableInput();

        if (condition == global::GameOver.GameOverType.win_shed)
        {
            // winner - reached the shed!
            gameOverMessage = "You WIN! You reached the shed";
            if (highScore < currentTime)
            {
                PlayerPrefs.SetInt("HighScore", currentTime);
                gameOverMessage += " with a high score of " + currentTime;
            }
            gameOverMessage += "!";
            gameoverText.color = new Color(60, 255, 120, 255);
        }
        else if (condition == global::GameOver.GameOverType.win_goldenSnitch)
        {
            // winner - caught the thing!
            gameOverMessage = "You WIN! You caught the golden orb";
            if (highScore < currentTime)
            {
                PlayerPrefs.SetInt("HighScore", currentTime);
                gameOverMessage += " with a high score of " + currentTime;
            }
            gameOverMessage += "!";
            gameoverText.color = new Color(60, 255, 120, 255);
        }
        else if (condition == global::GameOver.GameOverType.lose_time)
        {
            // loser - outta time!
            gameOverMessage = "You ran out of time! You LOSE!";
            gameoverText.color = new Color(255, 80, 80, 255);
        }
        else if (condition == global::GameOver.GameOverType.lose_laser)
        {
            // loser - incinerated by laser from sky!
            gameOverMessage = "You've been incinerated from space! You LOSE!";
            gameoverText.color = new Color(255, 80, 80, 255);
        }
        else if (condition == global::GameOver.GameOverType.lose_borg)
        {
            // loser - assimilated by the Borg!
            gameOverMessage = "You've been assimilated by the Borg! You LOSE!";
            gameoverText.color = new Color(255, 80, 80, 255);
        }

        if (PlayerPrefs.HasKey("TotalScore"))
        {
            int tempTotal = PlayerPrefs.GetInt("TotalScore") + currentTime;
            PlayerPrefs.SetInt("TotalScore", tempTotal);
        }
        else
        {
            PlayerPrefs.SetInt("TotalScore", currentTime);
        }

        goldenSnitch.transform.parent.gameObject.SetActive(false);
        laser.SetActive(false);
        borg.transform.parent.gameObject.SetActive(false);

        gameoverText.text = gameOverMessage;
        gameoverUI.SetActive(true);
        playUI.SetActive(false);
        player.GetComponent<PlayerInputManager>().SetCursorState(false);
    }
}
