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

    public Animator panelAnim;
    public GameObject whitePlayer, blackPlayer;

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

        //sec = 0.1f;
        //isEndDialogue = false;

        panelAnim.Play("Start");
        ResetStart();
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTalking == false)
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
                print(cameraScr.minY);
                print("End Dialogue");
            }
            else if (sentences.Count != 0)
            {
                DisplayDialogue();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isEndDialogue == false) music.Play();
        if (Input.GetKey(KeyCode.Space))
        {
            sec = 0.01f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            sec = 0.05f;
        }
        if (isEndDialogue && Time.timeScale == 0 && isTalking == false)
        {
            Time.timeScale = 1;
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

        sec = 0.1f;
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
        isTalking = false;
    }
}
