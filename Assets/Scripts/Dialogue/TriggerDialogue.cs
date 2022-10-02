using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    Queue<string> sentences = new Queue<string>();

    [SerializeField] Text nameText;
    [SerializeField] Text sentenceText;

    string str; //trich sentence tu sentences
    float sec;
    public bool isTalking = false;
    public bool isEndDialogue;
    bool isEndFirstTime = false;

    public Animator panelAnim;
    public GameObject whitePlayer, blackPlayer, transparentPauseButton;

    AudioSource music;

    private void Awake()
    {
        music = gameObject.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //nameText.text = dialogue.name;
        //foreach (string sentence in dialogue.sentences)
        //{
        //    sentences.Enqueue(sentence);
        //}

        sec = 0.1f;
        //isEndDialogue = false;

        panelAnim.Play("Start");
        ResetStart();
    }
   
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isTalking == false)
        {
            if (sentences.Count == 0 && isEndDialogue == false)
            {
                isEndDialogue = true;
                panelAnim.Play("End");
                blackPlayer.SetActive(true);
                blackPlayer.transform.position = whitePlayer.transform.position - new Vector3(0, 12, 0);
                CameraController cameraScr = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
                cameraScr.isFollow2Player = true;
                cameraScr.minY = -10;
                cameraScr.maxX = 105;
                print("End Dialogue");
                Time.timeScale = 1;
                transparentPauseButton.SetActive(false);
            }
            else if (sentences.Count != 0)
            {
                DisplayDialogue();
            }
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isEndDialogue == false)
        {
            music.Play();
            sec = 0.01f;
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            sec = 0.01f;
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            sec = 0.05f;
        }
        if (isEndDialogue && Time.timeScale == 0 && isTalking == false && isEndDialogue == false)
        {
            isEndFirstTime = true;
            Time.timeScale = 1;
            transparentPauseButton.SetActive(false);
        }
    }

    public void ResetStart()
    {
        Time.timeScale = 0;
        nameText.text = dialogue.name;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        //sec = 0.1f;
        isEndDialogue = false;
        DisplayDialogue();
    }
    public void Talk()
    {
        if (sentences.Count == 0)
        {
            print("End Dialogue here");
        }
        else
        {
            if (isTalking == false)
            {
                DisplayDialogue();
            }
        }
    }    

    void DisplayDialogue()
    {
        str = sentences.Dequeue();

        sentenceText.text = str;
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(str));
    }

    IEnumerator TypeSentence(string str)
    {
        isTalking = true;
        sentenceText.text = "";
        foreach (char letter in str.ToCharArray())
        {
            sentenceText.text += letter;
            yield return new WaitForSecondsRealtime(sec);
        }
        //yield return new WaitForSecondsRealtime(0.5f);
        isTalking = false;
    }
}
