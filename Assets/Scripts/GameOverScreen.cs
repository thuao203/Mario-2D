using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public static GameOverScreen Instance ;
    public Text livesText;
    public Text coinsText;
    public Text highscoreText; 
    
    public void Setup(int lives, int coins, int highscore)
    {
        gameObject.SetActive(true);
        livesText.text = "Lives:  " + lives;
        coinsText.text = "Coins:  " + coins;
        highscoreText.text = "Highscore: " + highscore;
    }
    private void Awake()
    {
        
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // public void UpdateUI(int lives, int coins, int highscore)
    // {
    //     livesText.text = "Lives:  " + lives;
    //     coinsText.text = "Coins:  " + coins;
    //     highscoreText.text = "Highscore: " + highscore;
    // }
    
    
}

    

