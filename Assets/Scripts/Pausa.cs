using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    public Animator anim;
    Animator settingsMenu;
    void Awake()
    {
        settingsMenu = GameObject.FindGameObjectWithTag("MenuOptions").GetComponent<Animator>();
    }
    public void PauseTime(){
        Time.timeScale =0;
    }
    public void ResumeTime(){
        Time.timeScale =1;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Scene currentScene = SceneManager.GetActiveScene();
    		string sceneName = currentScene.name;
            if(currentScene.name == "Win")
            {
                return;
            }
            PauseTime();
            anim.SetTrigger("Show");
        }
    }
    public void openSettings()
    {
        settingsMenu.SetTrigger("Show");
    }
}