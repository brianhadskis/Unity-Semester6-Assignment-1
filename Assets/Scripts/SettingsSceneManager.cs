using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsSceneManager : MonoBehaviour
{
    public TMP_InputField inputName;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            inputName.text = PlayerPrefs.GetString("PlayerName");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoHome()
    {
        PlayerPrefs.SetString("PlayerName", inputName.text);
        SceneManager.LoadScene("HomeScene");
    }

    public void ResetStats()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetString("PlayerName", "Commander Shepard");
        PlayerPrefs.SetInt("TotalScore", 0);
        PlayerPrefs.SetInt("TimesPlayed", 0);
    }
}
