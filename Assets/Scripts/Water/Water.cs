using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public ParticleSystem vfx_WaterSplash;
    public bool isReverse;
    AudioSource music;

    private void Awake()
    {
        music = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReverse)
        {
            if (collision.GetComponentInParent<Rigidbody2D>().velocity.y > 0)
            {
                if (collision.CompareTag("Player")) music.Play();
                Instantiate(vfx_WaterSplash, collision.transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (collision.GetComponentInParent<Rigidbody2D>().velocity.y < 0)
            {
                Instantiate(vfx_WaterSplash, collision.transform.position, Quaternion.identity);
                if (collision.CompareTag("Player")) music.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isReverse)
        {
            if (collision.GetComponentInParent<Rigidbody2D>().velocity.y < 0)
            {
                if (collision.CompareTag("Player")) music.Play();
                Instantiate(vfx_WaterSplash, collision.transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (collision.GetComponentInParent<Rigidbody2D>().velocity.y > 0)
            {
                if (collision.CompareTag("Player")) music.Play();
                Instantiate(vfx_WaterSplash, collision.transform.position, Quaternion.identity);
            }
        }
    }
}
