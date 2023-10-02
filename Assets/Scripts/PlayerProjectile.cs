using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Collidable
{
    
    public int damagePoint = 1;
    public float pushForce = 2.0f;
    public GameObject explosion;
    // Start is called before the first frame update
       protected override void OnCollide(Collider2D coll){
       
            //Debug.Log("TOCO: "+ coll.tag + " / "+ coll.name);
        if( coll.tag == "Fighter" && coll.name != "Player"){
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,    
                pushForce = pushForce
            };
            coll.SendMessage("ReceiveDamage", dmg);
            
        }
        if(coll.name != "Player" && coll.name != "Weapon")
        {
            //posar explosio
                if(explosion != null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                audioManager.PlaySFX(audioManager.explode);
                
            }
                Destroy(gameObject);
        }
    }
}
