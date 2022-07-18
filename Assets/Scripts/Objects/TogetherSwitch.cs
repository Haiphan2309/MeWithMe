using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TogetherSwitch : MonoBehaviour
{
    public TogetherSwitch anotherSwitchScr;
    BoxCollider2D coll;
    public LayerMask layerTouching;
    public Tilemap togetherBlockTilemap;

    Animator anim;
    SpriteRenderer sprRen;

    public ParticleSystem vfx_Destroy;

    public bool isPressing;
    private void Awake()
    {
        coll = gameObject.GetComponent<BoxCollider2D>();
        sprRen = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coll.IsTouchingLayers(layerTouching))
        {
            isPressing = true;
            anim.Play("Pressing");
        }
        else
        {
            isPressing = false;
            anim.Play("Idle");
        }
        if (anotherSwitchScr != null)
        {
            if (isPressing && anotherSwitchScr.isPressing) DestroyTogetherBlock();
        }
    }

    void DestroyTogetherBlock()
    {
        //BoundsInt bounds = togetherBlockTilemap.cellBounds;
        //TileBase[] allTile = togetherBlockTilemap.GetTilesBlock(bounds);
        //for (int x=0; x<bounds.size.x; x++)
        //{
        //    for (int y=0; y<bounds.size.y; y++)
        //    {
        //        TileBase tile = allTile[x + y * bounds.size.x];
        //        if (tile!=null)
        //        {
        //            togetherBlockTilemap.gettile
        //        }
        //    }
        //}
        foreach (var position in togetherBlockTilemap.cellBounds.allPositionsWithin)
        {
            if (togetherBlockTilemap.HasTile(position))
            {
                Instantiate(vfx_Destroy, position, Quaternion.identity);
            }
        }

        togetherBlockTilemap.ClearAllTiles();
        Destroy(anotherSwitchScr.gameObject);
        Destroy(gameObject);
    }
}
