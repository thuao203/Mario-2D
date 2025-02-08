using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraMenu : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
       
    }
    public void QuitGame()
    {
        Application.Quit();
    }


}
