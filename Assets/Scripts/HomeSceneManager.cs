using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HomeSceneManager : MonoBehaviour
{
    public TMP_Text playerName;
    public TMP_Text totalScore;
    public TMP_Text highScore;
    public TMP_Text timesPlayed;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            playerName.text = "Welcome, " + PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            playerName.text = "Welcome, Commander Shepard";
        }

        if (PlayerPrefs.HasKey("TotalScore"))
        {
            totalScore.text = PlayerPrefs.GetInt("TotalScore").ToString();
        }
        else
        {
            totalScore.text = "0";
        }

        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
        else
        {
            highScore.text = "0";
        }

        if (PlayerPrefs.HasKey("TimesPlayed"))
        {
            timesPlayed.text = PlayerPrefs.GetInt("TimesPlayed").ToString();
        }
        else
        {
            timesPlayed.text = "0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }
}
