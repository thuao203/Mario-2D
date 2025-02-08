using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    [SerializeField] private AudioMixer  myMixer;
    //kiểm soát các nhóm âm thanh (music, SFX) trong Unity.
    [SerializeField] private Slider  musicSlider;
    [SerializeField] private Slider  SFXSlider;
    //slider điều khiển âm lượng

    private void Start()
    {   
        SetMusicVolume(); 
        SetSFXVolume();       
    }
    public void SetMusicVolume()
    //lấy giá trị từ slider musicSlider
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
        //chuyển đổi giá trị slider (thường từ 0 đến 1) thành giá trị âm lượng theo thang decibel (dB
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume)*20);
    }
    public void Music()
    {
        audioManager.PlaySFX(audioManager.SFX);
        //phát một hiệu ứng âm thanh khi có sự thay đổi âm lượng
    }

    

}  
