using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuStartController : MonoBehaviour
{
    public Button startBtn, exitBtn;
    Animator startBtnAnim, exitBtnAnim;
    public Animator blackSceneAnim;

    public AudioClip stretchClip, shrinkClip, pressButtonClip;
    AudioSource music;

    //public ParticleSystem clickEffect;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        music = gameObject.GetComponent<AudioSource>();
        //blackSceneAnim = blackScene.GetComponent<Animator>();
        startBtnAnim = startBtn.GetComponent<Animator>();
        exitBtnAnim = exitBtn.GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        //blackSceneAnim.Play("BlackSceneBegin");
        Time.timeScale = 1;
        Load();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R)) EraseData(); //de erase data luc moi vao
    //}
    public void onHoverStartBtn()
    {
        music.clip = stretchClip;
        music.Play();
        //startBtnAnim.Play("BtnEnter");
        startBtnAnim.SetBool("isHover", true);
        music.Play();
    }

    public void onExitStartBtn()
    {
        music.clip = shrinkClip;
        music.Play();
        //startBtnAnim.Play("BtnExit");
        startBtnAnim.SetBool("isHover", false);
    }

    public void onHoverExitBtn()
    {
        music.clip = stretchClip;
        music.Play();
        //exitBtnAnim.Play("BtnEnter");
        exitBtnAnim.SetBool("isHover", true);
        music.Play();
    }
    public void onExitExitBtn()
    {
        music.clip = shrinkClip;
        music.Play();
        //exitBtnAnim.Play("BtnExit");
        exitBtnAnim.SetBool("isHover", false);
    }

    public void ExitBtn()
    {
        music.clip = pressButtonClip;
        music.Play();
        Debug.Log("Exit");
        Application.Quit();
    }    
    public void StartBtn()
    {
        //startObj.SetActive(false);
        //mode.SetActive(true);

        music.clip = pressButtonClip;
        music.Play();
        blackSceneAnim.Play("EndWorldMap");
        Invoke("LoadGameScene", 1f);

        //SceneManager.LoadScene(1);
    }

    void LoadGameScene()
    {
        if (GameData.currentLevel == 0)
            SceneManager.LoadScene(2);
        else SceneManager.LoadScene(1);
    }

    void EraseData()
    {
        GameData data = SaveSystem.LoadData();
        if (data != null)
        {
            print("EraseData");
            GameData.currentLevel = 0;
            for (int i = 0; i < GameData.NUM_LEVEL; i++)
            {
                GameData.isCompleteLevel[i] = false;
                GameData.isGetFairy[i] = false;
            }
        }
        Save();
    }

    void Save()
    {
        print("Save");
        SaveSystem.SaveData();
    }
    void Load()
    {
        GameData data = SaveSystem.LoadData();
        if (data != null)
        {
            print("Data not null");
            GameData.currentLevel = data.currentLevelData;
            for (int i = 0; i < GameData.NUM_LEVEL; i++)
            {
                GameData.isCompleteLevel[i] = data.isCompleteLevelData[i];
                GameData.isGetFairy[i] = data.isGetFairyData[i];
            }
        }
        print("Load Current Level:" + GameData.currentLevel);
    }
}
