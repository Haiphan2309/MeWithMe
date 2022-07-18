using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGhost : MonoBehaviour
{
    PolygonCollider2D coll;

    private void Awake()
    {
        coll = gameObject.GetComponent<PolygonCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coll.TryUpdateShapeToAttachedSprite();
    }
}
