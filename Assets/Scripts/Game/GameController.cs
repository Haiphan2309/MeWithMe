using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    bool isGameOver;
    bool isToWorldMap;

    public bool isGetFairy;
    public GameObject cage;

    public Animator blackSceneAnim;
    public GameObject panelStageClear;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        Time.timeScale = 1;
        isGameOver = false;
        isGetFairy = GameData.isGetFairy[GameData.currentLevel];
        if (isGetFairy == true) cage.SetActive(false);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        print("isCompleteLevel " + GameData.currentLevel + " :" + GameData.isCompleteLevel[GameData.currentLevel]);
    //        print("isGetFairy " + GameData.currentLevel + " :" + GameData.isGetFairy[GameData.currentLevel]);
    //    }
    //    if (Input.GetKeyDown(KeyCode.K)) ToWorldMap(1);
    //    if (Input.GetKeyDown(KeyCode.J)) isGetFairy = true;
    //}

    public void GameOver()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            ToWorldMap(0);
            
        }
    }

    public void ToMenu()
    {
        GameData.isCompleteLevel[GameData.currentLevel] = true;
        if (isGetFairy) GameData.isGetFairy[GameData.currentLevel] = true;
        panelStageClear.SetActive(true);
        StartCoroutine(DoBlackSceneAnim(2));
        StartCoroutine(LoadMenuScene(3));
    }

    public void BackButtonToMenu()
    {
        StartCoroutine(DoBlackSceneAnim(0));
        StartCoroutine(LoadMenuScene(1));
    }
    IEnumerator LoadMenuScene(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        SceneManager.LoadScene(0);
    }

    public void ToWorldMap(int i) //i=0: lose, i!=0: win level
    {
        if (isToWorldMap == false)
        {
            if (i != 0)
            {
                GameData.isCompleteLevel[GameData.currentLevel] = true;
                if (isGetFairy) GameData.isGetFairy[GameData.currentLevel] = true;
                panelStageClear.SetActive(true);
                StartCoroutine(DoBlackSceneAnim(2));
                StartCoroutine(LoadWorldMapScene(3));
            }
            else
            {
                StartCoroutine(DoBlackSceneAnim(1));
                if (GameData.currentLevel == 0)
                    StartCoroutine(LoadCurrentScene(2));
                else
                    StartCoroutine(LoadWorldMapScene(2));
            }

            isToWorldMap = true;
            print("ToWorldMap");

            //Invoke("DoBlackSceneAnim", 1);
            //Invoke("LoadCurrentScene",2);
            //StartCoroutine(DoBlackSceneAnim(1));
            //StartCoroutine(LoadCurrentScene(2));
        }
    }

    public void LoadEndingScene()
    {
        GameData.isCompleteLevel[GameData.currentLevel] = true;
        StartCoroutine(DoBlackSceneAnim(1));
        StartCoroutine(LoadEndingScene(2));
    }

    IEnumerator LoadEndingScene(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        SceneManager.LoadScene(11);
    }

    IEnumerator DoBlackSceneAnim(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        blackSceneAnim.Play("End");
    }
    IEnumerator LoadCurrentScene(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator LoadWorldMapScene(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        SceneManager.LoadScene(1);
    }
}
