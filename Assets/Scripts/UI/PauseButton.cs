using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pausePanel;
    AudioSource music;
    public AudioClip buttonClip;

    private void Start()
    {
        music = GetComponent<AudioSource>();
        music.clip = buttonClip;
    }
    public void Pause()
    {
        PlaySound();
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void PlaySound()
    {
        music.Play();
    }
}
