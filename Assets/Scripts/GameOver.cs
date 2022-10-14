using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public enum GameOverType
    {
        win_shed,
        win_goldenSnitch,
        lose_time,
        lose_laser,
        lose_borg
    };

    public delegate void EndGame(GameOverType condition);
    public event EndGame OnEndGame;

    public GameOverType gameOverCondition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collision happened: " + gameOverCondition.ToString());
        if (other.tag == "Player")
        {
            //Debug.Log("collision happened: " + gameOverCondition.ToString());
            if (OnEndGame != null) OnEndGame(gameOverCondition);
        }
    }
}
