using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Texts
    public Text levelText,  hitpointText, upgradeCostText, xpText, goldText, dmgText;

 
    public Image weaponSprite;
    public RectTransform xpBar;

    
   
    public void OnUpgradeClick()
    {
       
        if(GameManager.instance.tryUpgradeWeapon())
        {
            Debug.Log("clickyes");
            UpdateMenu();
        }
    }

    public void UpdateMenu()
    {
        //player
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "MAX";
        }
        else{
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }
        goldText.text = GameManager.instance.gold.ToString();

        //xp bar
        int currentLvl = GameManager.instance.GetCurrentLevel();
        if(currentLvl == GameManager.instance.xpTable.Count)
        {
            xpText.text = "Max lvl";
            xpBar.localScale = Vector3.one;
        }
        else{
            int prevLvlXp= GameManager.instance.GetXpToLevel(currentLvl-1);
            int currentLvlXp =GameManager.instance.GetXpToLevel(currentLvl);
            
            int diff = currentLvlXp -prevLvlXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLvlXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString()+ " / " + diff;
            dmgText.text = GameManager.instance.weapon.damagePoint[GameManager.instance.weapon.weaponLevel].ToString();
        }

        //weapon
        weaponSprite.sprite = GameManager.instance.weaponClass[GameManager.instance.PlayerChoosed].weaponSprites[GameManager.instance.weapon.weaponLevel];
    }
    
}
