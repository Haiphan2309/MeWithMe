using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FairyText : MonoBehaviour
{
    public Text fairyText;
    // Start is called before the first frame update
    void Start()
    {
        fairyText.text = "x " + GameData.numOfFairy().ToString();
    }
}
