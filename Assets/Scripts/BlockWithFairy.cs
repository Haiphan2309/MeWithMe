using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWithFairy : MonoBehaviour
{
    public GameObject fairy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isQuitting = false;
    void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void OnDestroy()
    {
        if (!isQuitting)
        {
            fairy.SetActive(true);
        }
    }
}
