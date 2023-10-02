using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalSounds : MonoBehaviour
{
    AudioManager audioManager;
	GameObject[] buttons;
	private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start () 
{
	buttons = GameObject.FindGameObjectsWithTag("Button");
	
	foreach (var b in buttons) 
	{ 
		Button c = b.GetComponent<Button>();
		c.onClick.AddListener( OnClick ); 
	} 
}

void OnClick()
{
	audioManager.PlaySFX(audioManager.click);
}
}
