using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 2f;

    AudioManager audioManager;
    protected float immuneTime = 0.2f;
    protected float lastImmune;

    protected Vector2 pushDirection;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    protected virtual void ReceiveDamage(Damage dmg){
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            audioManager.PlaySFX(audioManager.hit);
          
            GameManager.instance.ShowText(dmg.damageAmount.ToString()+" dmg", 15, Color.red, transform.position, Vector3.up * 100, 0.5f);

            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }
    protected virtual void Death()
    {

    }
}
