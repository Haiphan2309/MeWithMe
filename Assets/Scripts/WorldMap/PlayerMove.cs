using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour //tac dung nhu mot cai gamecontroller
{
    public Transform level1, level2, level3, level4, level5, level6, level7, level8;
    public float speed;
    Animator anim, levelPanelAnim;
    public Animator blackSceneAnim, exitBtnAnim;
    float xdir, ydir;
    public int level, levelTarget;

    public bool isCanMove;

    public GameObject levelPanel;
    public Text levelText;

    public AudioClip pressButtonClip, stretchClip, shrinkClip;
    AudioSource music;

    public Transform moveBtnTrans;
    private void Awake()
    {
        music = gameObject.GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        levelPanelAnim = levelPanel.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        isCanMove = true;
        if (GameData.currentLevel != 0)
        {
            levelTarget = GameData.currentLevel;
        }
        else speed = speed / 3;

        Save();
    }

    // Update is called once per frame
    void Update()
    {
        //xdir = Input.GetAxisRaw("Horizontal");
        //ydir = Input.GetAxisRaw("Vertical");

        CanPlaySound();
        if (isCanMove)
        {
            
            if (xdir > 0)
            {
                if (GameData.isCompleteLevel[level] == true)
                {
                    if (level == 1) levelTarget = 2;
                    else if (level == 2) levelTarget = 3;
                    else if (level == 3) levelTarget = 4;
                    else if (level == 4) levelTarget = 5;
                    else if (level == 5) levelTarget = 7;
                    else if (level == 6) levelTarget = 7;
                    else if (level == 7 && GameData.numOfFairy()>=5) levelTarget = 8;
                }
                //anim.Play("MoveRight");
            }
            if (xdir < 0)
            {
                if (level == 2) levelTarget = 1;
                else if (level == 3) levelTarget = 2;
                else if (level == 4) levelTarget = 3;
                else if (level == 5) levelTarget = 4;
                else if (level == 6) levelTarget = 4;
                else if (level == 7) levelTarget = 5;
                else if (level == 8) levelTarget = 7;
            }
            if (ydir > 0)
            {
                if (level == 7) levelTarget = 5;
                if (GameData.isCompleteLevel[level] == true)
                {
                    if (level == 4) levelTarget = 5;
                    if (level == 6) levelTarget = 7;
                }
                else
                {
                    if (level == 6 && GameData.isCompleteLevel[7] == true) levelTarget = 7;
                }
            }
            if (ydir < 0)
            {
                if (level == 5) levelTarget = 4;
                if (level == 7) levelTarget = 6;
                if (level == 4) levelTarget = 6;
            }
        }
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    GameData.isCompleteLevel[level] = true;
        //}
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    GameData.isGetFairy[level] = true;
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    SceneManager.LoadScene(1);
        //}


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (level != 0 && isCanMove) EnterLevel();
        }        
    }

    private void FixedUpdate()
    {
        MoveToLevelPanel(levelTarget);
    }

    //------------------------------------------------------------Button Only
    Vector3 mousePos;
    public void MoveBtnDown()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z += 10;
        if (mousePos.x - moveBtnTrans.position.x < 2.5f)
        {
            if (mousePos.x - moveBtnTrans.position.x > 0.5f)
            {
                xdir = 1;
            }
            else if (mousePos.x - moveBtnTrans.position.x < -0.5f)
            {
                xdir = -1;
            }
            else xdir = 0;
            if (mousePos.y - moveBtnTrans.position.y < -0.5f) ydir = -1;
            else if (mousePos.y - moveBtnTrans.position.y > 0.5f) ydir = 1;
            else ydir = 0;
        }
    }
    public void PlaySound()
    {
        music.clip = pressButtonClip;
        music.Play();
    }
    public void MoveBtnUp()
    {
        xdir = 0;
        ydir = 0;
    }
    public void ChooseBtn()
    {
        if (level != 0 && isCanMove) EnterLevel();
    }
    //------------------------------------------------------------------

    void CanPlaySound()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)
            || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            music.clip = pressButtonClip;
            music.Play();
        }
    }
    void MoveToLevelPanel(int i)
    {
        if (i == 1) transform.position = Vector3.Lerp(transform.position, level1.position, speed * Time.fixedDeltaTime);
        if (i == 2) transform.position = Vector3.Lerp(transform.position, level2.position, speed * Time.fixedDeltaTime);
        if (i == 3) transform.position = Vector3.Lerp(transform.position, level3.position, speed * Time.fixedDeltaTime);
        if (i == 4) transform.position = Vector3.Lerp(transform.position, level4.position, speed * Time.fixedDeltaTime);
        if (i == 5) transform.position = Vector3.Lerp(transform.position, level5.position, speed * Time.fixedDeltaTime);
        if (i == 6) transform.position = Vector3.Lerp(transform.position, level6.position, speed * Time.fixedDeltaTime);
        if (i == 7) transform.position = Vector3.Lerp(transform.position, level7.position, speed * Time.fixedDeltaTime);
        if (i == 8) transform.position = Vector3.Lerp(transform.position, level8.position, speed * Time.fixedDeltaTime);
    }

    void EnterLevel()
    {
        music.clip = pressButtonClip;
        music.Play();
        blackSceneAnim.Play("EndWorldMap");
        GameData.currentLevel = level;
        StartCoroutine(LoadScene(1.5f, level+2));
        print("EnterLevel " + level);
    }
    IEnumerator LoadScene(float sec, int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(sec);
        SceneManager.LoadScene(sceneIndex);
    }

    public void ToMenu()
    {
        music.clip = pressButtonClip;
        music.Play();
        blackSceneAnim.Play("EndWorldMap");
        StartCoroutine(LoadScene(1.5f, 0));
    }

    public void onHoverExitBtn()
    {
        music.clip = stretchClip;
        music.Play();
        exitBtnAnim.SetBool("isHover", true);
    }
    public void onExitExitBtn()
    {
        music.clip = shrinkClip;
        music.Play();
        exitBtnAnim.SetBool("isHover", false);
    }

    void Save()
    {
        print("Save");
        SaveSystem.SaveData();
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelIcon"))
        {
            levelPanelAnim.Play("LevelPanelAppear");

            LevelIcon levelIconScr = collision.GetComponent<LevelIcon>();
            level = levelIconScr.id;
            levelText.text = "Level " + level.ToString();
        }
        //if (collision.gameObject.name == "Level1") level = 1;
        //if (collision.gameObject.name == "Level2") level = 2;
        //if (collision.gameObject.name == "Level3") level = 3;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelIcon"))
        {
            levelPanelAnim.Play("LevelPanelDisappear");
            level = 0;
        }
    }
}
