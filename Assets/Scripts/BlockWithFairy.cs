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

    private void OnDestroy()
    {
        fairy.SetActive(true);
    }
}
