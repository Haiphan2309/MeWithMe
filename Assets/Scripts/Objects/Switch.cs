using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Switch : MonoBehaviour
{
    public Tile blockTile, nonBlockTile;
    public Tilemap blockTilemap;
    CompositeCollider2D blockTilemapColl;

    public Sprite pressSprite, idleSprite;
    SpriteRenderer sprRen;
    BoxCollider2D coll;

    public LayerMask layerTouching;
    public bool isBlockOn, isPressing; // kiem tra on/off cua block

    AudioSource music;

    private void Awake()
    {
        music = gameObject.GetComponent<AudioSource>();
        blockTilemapColl = blockTilemap.GetComponent<CompositeCollider2D>();
        sprRen = gameObject.GetComponent<SpriteRenderer>();
        coll = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coll.IsTouchingLayers(layerTouching))
        {
            if (isPressing == false) SwitchBlock();
            isPressing = true;
            sprRen.sprite = pressSprite;
        }
        else
        {
            isPressing = false;
            sprRen.sprite = idleSprite;
        }
    }

    void SwitchBlock()
    {
        music.Play();
        if (isBlockOn)
        {
            //print(blockTilemap);
            blockTilemap.SwapTile(blockTile, nonBlockTile);
            blockTilemap.gameObject.layer = LayerMask.NameToLayer("Default");
            blockTilemapColl.isTrigger = true;
            isBlockOn = false;
        }
        else
        {
            blockTilemap.SwapTile(nonBlockTile, blockTile);
            blockTilemap.gameObject.layer = LayerMask.NameToLayer("Ground");
            blockTilemapColl.isTrigger = false;
            isBlockOn = true;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (isPressing == false)
    //        SwitchBlock();       
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    isPressing = true;
    //    sprRen.sprite = pressSprite;
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    isPressing = false;
    //    sprRen.sprite = idleSprite;
    //}
}
