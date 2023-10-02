using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeWorld : Collidable
{
    public string sceneName;
    private Animator anim;
    AudioManager audioManager;
    public AudioClip BSOwin;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
   protected override void OnCollide(Collider2D coll)
   {

        anim.SetTrigger("ChangeWorld");
        if(coll.name == "Player" && SceneManager.GetActiveScene().name == "World2")
        {
            GameManager.instance.ChangeWorld("Win");
            audioManager.canviBSO(BSOwin);
        }else if(coll.name == "Player"){
            GameManager.instance.ChangeWorld(sceneName);
        }
   }
  
}
