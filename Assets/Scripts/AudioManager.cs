using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("----------- Audio Background -----------")]
    [SerializeField] public AudioSource BSO;
    public Slider sliderBSO;
    [SerializeField] public AudioSource SFX;
    public Slider sliderSFX;
    [Header("----------- Audio Effects -----------")]
    
    public AudioClip BSOmenu;
    public AudioClip BSOdemon;
    public AudioClip BSOfairy;
    
    //public AudioClip BSOwin;
    public AudioClip BSOdemonBoss;
    public AudioClip BSOfairyBoss;
    public AudioClip hit;
    public AudioClip swing;
    public AudioClip explode;
    public AudioClip invoke;
    public AudioClip click;

    

    private void Start()
    {
        BSO.clip=BSOmenu;
        BSO.Play();
    }
    public void canviBSO(AudioClip name)
    {
        BSO.clip=name;
        BSO.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }
    public void changeVolumeBso()
    {
        BSO.volume=sliderBSO.value;
    }
    public void changeVolumeSFX()
    {
        SFX.volume=sliderSFX.value;
    }
}
