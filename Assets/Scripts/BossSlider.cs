using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSlider : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    public GameObject handleObj;
    Animator handleAnim;
    public Enemy bossScr;

    public ParticleSystem vfx_Decrease;

    private void Awake()
    {
        handleAnim = handleObj.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        hpSlider.maxValue = bossScr.HP;
        hpSlider.value = bossScr.HP;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = bossScr.HP;
        if (hpSlider.value <= 0)
        {
            //to do
        }
    }

    public void IncreaseHP()
    {
        hpSlider.value = bossScr.HP;
        handleAnim.Play("Increase");
    }
    public void DecreaseHP()
    {
        hpSlider.value = bossScr.HP;
        handleAnim.Play("Decrease");
        Instantiate(vfx_Decrease, handleObj.transform.position, Quaternion.identity);
    }

}
