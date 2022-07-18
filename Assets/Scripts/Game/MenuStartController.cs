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
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) //Hieu ung khi click chuot
        //{
        //    music.Play();
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    mousePos.z += 10;
        //    Instantiate(clickEffect, mousePos, Quaternion.identity);
        //}
    }
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
        SceneManager.LoadScene(2);
    }

    void LoadTimeModeScene()
    {
        SceneManager.LoadScene(2);
    }
}
