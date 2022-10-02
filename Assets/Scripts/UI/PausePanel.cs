using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    GameController gameControllerScr;

    public PauseButton pauseButtonScr;

    private void Start()
    {
        gameControllerScr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    public void Resume()
    {
        pauseButtonScr.PlaySound();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Back()
    {
        pauseButtonScr.PlaySound();
        gameControllerScr.ToWorldMap(0);
    }

    public void BackButtonToMenu()
    {
        pauseButtonScr.PlaySound();
        gameControllerScr.BackButtonToMenu();
    }
}
