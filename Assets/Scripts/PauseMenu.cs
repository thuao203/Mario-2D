using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Home()
    {
        if (GameManager.Instance != null)
    {
        //thu
        //GameManager.Instance.ReturnToHome();
        GameManager.Instance.ResetGame();
    }
       if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.gameObject.SetActive(false);
        }   
        
        SceneManager.LoadScene("1-0");
        Time.timeScale = 1; 
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    
}
