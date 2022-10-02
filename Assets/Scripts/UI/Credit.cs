using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Ending", 3.5f);
        Invoke("Hide", 3);
    }

    void Hide()
    {
        gameObject.GetComponent<Text>().text = "";
    }
    void Ending()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().LoadEndingScene();
    }
}
