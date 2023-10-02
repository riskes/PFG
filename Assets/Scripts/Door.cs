using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D bc;
    public Sprite closedDoor;
    public Sprite openDoor;
    public bool functional = true;
    public enum DoorType
    {
        left,right,top,bottom
    }
    public DoorType doorType;
    
    public void deactiveBC()
    {
        
        if(transform.childCount >1 && functional==true)
        {

            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = openDoor;
            bc = transform.GetChild(1).GetComponent<BoxCollider2D>();
            bc.enabled = false;
        }
        
    }
    public void activeBC()
    {
        
        if(transform.childCount >1 && functional==true)
        {
            Debug.Log("INSIDEID");
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = closedDoor;
            bc = transform.GetChild(1).GetComponent<BoxCollider2D>();
            bc.enabled   = true;
        }
        
    }
}
