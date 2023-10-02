using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake(){
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }    

    //Ressources
    public int PlayerChoosed = 0;
    public List<GameObject> players;
    [System.Serializable]
    public struct weaponPath
    {
        public List<Sprite> weaponSprites;
    }
    public List<weaponPath> weaponClass = new List<weaponPath>();
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    // Logic
    public int gold;
    public int experience;


    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {

        floatingTextManager.Show(msg,fontSize,color,position,motion,duration);
    }

    //Upgrade Weapon
    public bool tryUpgradeWeapon()
    {
        if(weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }
        if(gold >= weaponPrices[weapon.weaponLevel])
        {
            gold -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }
    private void Update()
    {
        if(floatingTextManager==null)
        {   
            floatingTextManager = GameObject.Find("FloatingTextManager").GetComponent<FloatingTextManager>();
        }
        if(player==null && GameObject.Find("Player")!=null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        if(weapon==null && GameObject.Find("Weapon") != null)
        {
            weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
        }
    }

    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while ( experience >= add){
            add += xpTable[r];
            r++;
            if (r == xpTable.Count)
            {
                return r;
            }
        }
        return r;
    }
    public int GetXpToLevel(int lvl)
    {
        int r=0;
        int xp=0;
        while(r<lvl)
        {
            xp +=xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantXp(int xp)
    {
        int currentLvl = GetCurrentLevel();
        experience+= xp;
        if (experience >= GetXpToLevel(currentLvl)&&currentLvl<3)
        {
            LvlUp();
        }
    }
    public void LvlUp()
    {
        player.maxHitpoint= System.Convert.ToInt32((player.maxHitpoint*GetCurrentLevel())/1.5);
        player.hitpoint = player.maxHitpoint;
        ShowText(" LvlUp", 15, Color.yellow, transform.position, Vector3.up * 100, 0.5f);
    }
    public void ChangeWorld(string newScene)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if(player==null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        if(weapon==null)
        {
            weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
        }
        player.transform.position = new Vector3(0,0,0);
        SceneManager.LoadScene(newScene);        
    }
  
}
