using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public Text livesText;
    public Text coinsText;
    public Text highscoreText; 

    

    private void Awake()
    {
        if (Instance != null )
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void OnDestroy()
    {
        if (Instance == this) 
        {
            Instance = null;
        }
    }

    public void UpdateUI(int lives, int coins, int highscore)
    {
        livesText.text = "Lives:  " + lives;
        coinsText.text = "Coins:  " + coins;
        highscoreText.text = "Highscore: " + highscore;
    }

    
}
