using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMenu : MonoBehaviour
{
     public void returnMainMenu(string sceneName)
   {
      foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
        if(o.GetComponent<Mantenir>() != null)
        {
          Destroy(o);
        }
        
      }
      GameManager.instance.experience = 0;
      GameManager.instance.gold = 0;
      GameManager.instance.ChangeWorld(sceneName);
      if(Time.timeScale ==0)
      {
        Time.timeScale = 1;
      }
   }
}
