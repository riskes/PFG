using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Collidable
{
    public int damagePoint = 5;
    public float pushForce = 4.0f;
    // Start is called before the first frame update
       public void destroyme()
       {
            Destroy(gameObject);
       }
       protected override void OnCollide(Collider2D coll){
       
        
            //Debug.Log("TOCO: "+ coll.tag + " / "+ coll.name);
        if( coll.tag == "Fighter" && coll.name!= "Player"){
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,    
                pushForce = pushForce
            };
            coll.SendMessage("ReceiveDamage", dmg);
        
        }
    }
}
