using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyLocalPos : MonoBehaviour
{
    public float r, speed;
    Vector3 localTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Magnitude(transform.localPosition - localTarget)<= 0.2f) RandomPos();
        transform.localPosition = Vector3.Lerp(transform.localPosition, localTarget, speed * Time.deltaTime);
    }

    void RandomPos()
    {
        localTarget = new Vector3(Random.Range(-r, r), Random.Range(-r, r), 0);
    }
}
