using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Collidable
{
    
    public int damagePoint = 1;
    public float pushForce = 2.0f;
    // Start is called before the first frame update
       protected override void OnCollide(Collider2D coll){
        if( coll.tag == "Fighter" && coll.name == "Player"){
            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,    
                pushForce = pushForce
            };
            coll.SendMessage("ReceiveDamage", dmg);
        
        }
        if(coll.tag != "Fighter" || coll.tag == "Fighter" && coll.name == "Player")
        {
            if(coll.tag != "Bullet"){
                Destroy(gameObject);
            }
        }
    }
}
