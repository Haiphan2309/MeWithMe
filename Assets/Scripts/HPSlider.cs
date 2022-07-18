using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    GameController gameControllerScr;

    [SerializeField] Slider hpSlider;
    public GameObject handleObj;
    Animator handleAnim;

    public ParticleSystem vfx_Decrease;

    private void Awake()
    {
        handleAnim = handleObj.GetComponent<Animator>();
        gameControllerScr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        hpSlider.maxValue = Player.maxHp;
        hpSlider.value = Player.hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (hpSlider.value <= 0) GameOver();
    }

    public void IncreaseHP()
    {
        hpSlider.value = Player.hp;
        handleAnim.Play("Increase");
    }
    public void DecreaseHP()
    {
        hpSlider.value = Player.hp;
        handleAnim.Play("Decrease");
        Instantiate(vfx_Decrease, handleObj.transform.position, Quaternion.identity);
    }

    void GameOver()
    {
        gameControllerScr.GameOver();
    }
}
