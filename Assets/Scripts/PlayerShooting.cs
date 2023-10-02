using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : Shooting
{
    public float cooldown = .5f;
    private float lastswing;

    // Update is called once per frame
    void Update()
    {
        if(firePoint==null && GameObject.Find("Weapon")!=null)
        {
            firePoint = GameObject.Find("Weapon").GetComponent<Transform>();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            if(Time.time  - lastswing > cooldown){
                lastswing = Time.time;
                Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 Direction = (Vector3)(Input.mousePosition-screenPoint);
                Shoot(Direction);
            }
        }
    }
}
