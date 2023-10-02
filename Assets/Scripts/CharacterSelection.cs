using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    public Text msText,  hitpointText;
    public TextMeshProUGUI classText;
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    private void Start() 
    {
        characterSelectionSprite.sprite = GameManager.instance.players[currentCharacterSelection].GetComponent<SpriteRenderer>().sprite;
        weaponSprite.sprite = GameManager.instance.weaponClass[currentCharacterSelection].weaponSprites[0];
        hitpointText.text = GameManager.instance.players[currentCharacterSelection].GetComponent<Player>().hitpoint.ToString();
        classText.text = GameManager.instance.players[currentCharacterSelection].GetComponent<Player>().name;
        msText.text = GameManager.instance.players[currentCharacterSelection].GetComponent<Player>().moveSpeed.ToString();
    }
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            if(currentCharacterSelection == GameManager.instance.players.Count)
            {
                currentCharacterSelection = 0;
            }
            OnSelectionChange();
        }
        else
        {
            currentCharacterSelection--;
            if(currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.instance.players.Count-1;
            }
            OnSelectionChange();
        }
    }
    
     public void OnSelectionChange() 
    {
        characterSelectionSprite.sprite = GameManager.instance.players[currentCharacterSelection].GetComponent<SpriteRenderer>().sprite;
        weaponSprite.sprite = GameManager.instance.weaponClass[currentCharacterSelection].weaponSprites[0];
        hitpointText.text = GameManager.instance.players[currentCharacterSelection].GetComponent<Player>().hitpoint.ToString();
        classText.text = GameManager.instance.players[currentCharacterSelection].GetComponent<Player>().name;
        msText.text = GameManager.instance.players[currentCharacterSelection].GetComponent<Player>().moveSpeed.ToString();
    }
    public void LoadPlayer()
    {
        GameManager.instance.PlayerChoosed = currentCharacterSelection;
        GameObject ply = Instantiate(GameManager.instance.players[currentCharacterSelection], transform.position, Quaternion.identity);
        ply.name = "Player";
    }
}
