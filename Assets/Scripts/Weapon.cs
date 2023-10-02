using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int[] damagePoint = {1,2,3};
    public float[] pushForce = {2.0f,2.0f,2.0f};

    // Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;
    private Animator playerAnim;
    public float cooldown = .5f;
    private Animator anim;
    private float lastswing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            if(Time.time  - lastswing > cooldown){
                lastswing = Time.time;
                Swing();
            }
        }
        if(playerAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("mirrored"))
        {
            anim.SetFloat("Direction", -1f);
        }else{
            anim.SetFloat("Direction", 1f);
        }
    }
    protected override void OnCollide(Collider2D coll){
       
            //Debug.Log("TOCO: "+ coll.tag + " / "+ coll.name);
        if( coll.tag == "Fighter" && coll.name != "Player"){
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,    
                pushForce = pushForce[weaponLevel]
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
    private void Swing(){
        anim.SetTrigger("Swing");
        audioManager.PlaySFX(audioManager.swing);
    }
    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponClass[GameManager.instance.PlayerChoosed].weaponSprites[weaponLevel];
    }
}
