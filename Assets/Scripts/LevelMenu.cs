using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void OpenLevel(int levelId)
{
    // Tạo tên scene dựa trên levelId
    string levelName = "1-" + levelId;

    if (levelId == 0)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame(); // Reset số mạng và điểm
        }
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.gameObject.SetActive(false);
        }
    }
    else
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.stage = levelId; // Cập nhật stage trong GameManager
        }
        
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.gameObject.SetActive(true);
        }
    }

    // Tải scene mới
    SceneManager.LoadScene(levelName);
}
}   