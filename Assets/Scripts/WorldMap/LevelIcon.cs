using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIcon : MonoBehaviour
{
    public Sprite winSprite;
    SpriteRenderer sprRen;
    public int id;

    public GameObject cage, fairy;
    private void Awake()
    {
        sprRen = gameObject.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameData.isCompleteLevel[id]) sprRen.sprite = winSprite;
        if (GameData.isGetFairy[id]) fairy.SetActive(true);
        else cage.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameData.isCompleteLevel[id]) sprRen.sprite = winSprite;
    }
}
