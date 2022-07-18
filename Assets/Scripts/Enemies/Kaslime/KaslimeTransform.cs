using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaslimeTransform : MonoBehaviour
{
    public GameObject trueKaslime;
    AudioSource music;
    public float timePlayAudio, timeStopAudio;

    private void Awake()
    {
        music = gameObject.GetComponent<AudioSource>();
        Invoke("PlayAudio", timePlayAudio);
        Invoke("StopAudio", timeStopAudio);
    }

    void PlayAudio()
    {
        music.Play();
    }
    void StopAudio()
    {
        music.Stop();
    }
    public void AppearTrueKaslime()
    {
        trueKaslime.SetActive(true);
        Destroy(gameObject);
    }
}
