using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ParticleSystem vfx_Explosion;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(vfx_Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
