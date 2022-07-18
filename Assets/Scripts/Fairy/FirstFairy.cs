using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFairy : MonoBehaviour
{
    public GameObject dialogueCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BeginDialogue", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginDialogue()
    {
        dialogueCanvas.SetActive(true);
    }
}
