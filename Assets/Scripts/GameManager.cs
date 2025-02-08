
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance ;
    
//     public int world ;
//     public int stage ;
//     public int lives ;
//     public int coins ;
//     public int highscore ;
    
//     private void Awake()
//     {
//         //NewGame();
        
//         if (Instance != null) 
//         {
//             DestroyImmediate(gameObject);
//         }
//         else 
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//     }
//     private void OnDestroy()
//     {
//         if (Instance == this) 
//         {
//             Instance = null;
//         }
//     }
//     public void Start()
//     {
//         Application.targetFrameRate = 60; 
//         NewGame();
//         //ScoreManager.Instance.UpdateUI(lives, coins, highscore);
//     }
    
//     public void NewGame()
//     {
//         lives = 3;
//         coins = 0;
//         //thu
//         if (ScoreManager.Instance != null)
//         {
//             ScoreManager.Instance.UpdateUI(lives, coins, highscore);
//         }
//         //
//        // ScoreManager.Instance.UpdateUI(lives, coins, highscore);
//         LoadLevel(world, stage);
        
//     }
//     public void ResetGame()
//     {
//         lives = 3;
//         coins = 0;
//         highscore = 0;

//         //thu
//         if (ScoreManager.Instance != null)
//         {
//             ScoreManager.Instance.UpdateUI(lives, coins, highscore);
//         }
//         //
//         //ScoreManager.Instance.UpdateUI(lives, coins, highscore);
        
//     }
//     public void LoadLevel(int world, int stage)
    
//     {
//         this.world = world;
//         this.stage = stage;

//         SceneManager.LoadScene($"{world}-{stage}");

        
//     }
//     public void ResetLevel(float delay)
//     {
//         Invoke(nameof(ResetLevel),delay);
//     }
//     public void NextLevel()
//     {
//         LoadLevel(world, stage + 1);
//     }
//     public void ResetLevel()
//     {
//         lives--;
//         //ScoreManager.Instance.UpdateUI(lives, coins, highscore);
//         //thu
//         if (ScoreManager.Instance != null)
//         {
//             ScoreManager.Instance.UpdateUI(lives, coins, highscore);
//         }
//         //
//         if (lives > 0)
//         {
//             LoadLevel(1, stage);
            
//         }
//         else 
//         {
//             GameOver();
            
//         }
        
//     }
//     public void GameOver()
//     {
//         //ResetGame();
        
//         LoadLevel(1, 6);
        
    
//     }
//     public void AddCoin()
//     { 
//         coins++;
        
//         if (coins == 100)
//         {
//             AddLife();
//             coins = 0;
//         }
//         //thu
//         if (ScoreManager.Instance != null)
//         {
//             ScoreManager.Instance.UpdateUI(lives, coins, highscore);
//         }
//         //ScoreManager.Instance.UpdateUI(lives, coins, highscore);
        
//     }
//     public void AddLife()
//     {
//         lives++;
//         //thu
//         if (ScoreManager.Instance != null)
//         {
//             ScoreManager.Instance.UpdateUI(lives, coins, highscore);
//         }
//         //ScoreManager.Instance.UpdateUI(lives, coins, highscore);

//     }
//     public void ReturnToHome()
//     {
//         ResetGame(); // Reset lại số mạng và điểm khi quay về trang chính
//         LoadLevel(1, 0); 
//         //SceneManager.LoadScene("Main Menu"); // Tên của màn hình Home
//         //thu
//         Time.timeScale = 1;
        
//     }
// }



using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int world;
    public int stage;
    public int lives;
    public int coins;
    public int highscore;
    
    private int initialLives = 3; // Số mạng ban đầu

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            //Nếu đã có một instance GameManager, hủy game object mới
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //Instance là game object hiện tại và
            //sử dụng DontDestroyOnLoad để giữ nó qua các scene.
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        NewGame();
    }

    public void NewGame()
    {
        lives = initialLives; // Reset số mạng về giá trị ban đầu
        coins = 0;
        highscore = 0;
        Debug.Log("New Game: Số mạng đã reset về 3");
        ScoreManager.Instance.UpdateUI(lives, coins, highscore);
        LoadLevel(world, stage);
    }

    public void ResetGame()
    {
        NewGame(); // Gọi phương thức NewGame để reset trò chơi
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay); // Gọi lại phương thức ResetLevel không tham số sau khoảng thời gian trễ
    }

    public void ResetLevel()
    {
        lives--; // Giảm số mạng
        ScoreManager.Instance.UpdateUI(lives, coins, highscore);

        if (lives > 0)
        {
            LoadLevel(world, stage); // Tải lại cấp độ hiện tại
        }
        else
        {
            GameOver(); // Nếu không còn mạng, gọi GameOver
        }
    }

    public void GameOver()
    {
        // Hiển thị màn hình Game Over, không tự động khởi động lại
        Debug.Log("Game Over: Người chơi đã hết mạng.");
        
        // Gọi màn hình Game Over hoặc tải lại scene chính
        SceneManager.LoadScene("1-6"); // Đảm bảo bạn có một scene Game Over
    }

    public void RestartGame()
    {
        // Khởi động lại game với 3 mạng sau khi người chơi chọn chơi lại
        NewGame();
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void AddCoin()
    { 
        coins++;
        if (coins == 100)
        {
            AddLife();
            coins = 0;
        }
        ScoreManager.Instance.UpdateUI(lives, coins, highscore);
    }

    public void AddLife()
    {
        lives++;
        ScoreManager.Instance.UpdateUI(lives, coins, highscore);
    }
}