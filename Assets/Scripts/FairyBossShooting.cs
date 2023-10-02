using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyBossShooting : Shooting

{
    public float cadence;
    public int random;
    public Transform Direction;
    private Vector2 dir1 = new Vector2(0,1);
    private Vector2 dir2 = new Vector2(0,-1);
    private Vector2 dir3 = new Vector2(1,0);
    private Vector2 dir4 = new Vector2(-1,0);


    public void StartBossShooting()
    {
        random = Random.Range(0,4);
        StartCoroutine("Fade");
        InvokeRepeating("doHability", 2.0f, 7f);
    }
    public void doHability()
    {   
        random = Random.Range(0,4);
        StartCoroutine("Fade");
    }
    IEnumerator Fade() 
    {
        Debug.Log("random: "+random);
        InvokeRepeating("ShootBullet", 2.0f, 0.5f);
        yield return new WaitForSeconds(5f); 
        CancelInvoke("ShootBullet");  
    }

    void Update()
    {
        if(Direction==null && GameObject.Find("Player")!=null)
        {
            Direction = GameObject.Find("Player").GetComponent<Transform>();
        }
    }

    void ShootBullet(){

        if(random==0)
        {
            Shoot(dir1);
            dir1 = Quaternion.AngleAxis(-20f, Vector3.forward) * dir1;
            Shoot(dir2);
            dir2 = Quaternion.AngleAxis(-20f, Vector3.forward) * dir2;
            Shoot(dir3);
            dir3 = Quaternion.AngleAxis(-20f, Vector3.forward) * dir3;
            Shoot(dir4);
            dir4 = Quaternion.AngleAxis(-20f, Vector3.forward) * dir4;
        }
        if(random==1)
        {
            Shoot(dir1);
            dir1 = Quaternion.AngleAxis(20f, Vector3.forward) * dir1;
            Shoot(dir2);
            dir2 = Quaternion.AngleAxis(20f, Vector3.forward) * dir2;
            Shoot(dir3);
            dir3 = Quaternion.AngleAxis(20f, Vector3.forward) * dir3;
            Shoot(dir4);
            dir4 = Quaternion.AngleAxis(20f, Vector3.forward) * dir4;
        }
        if(random==2){
            Shoot(firePoint.up);
            Shoot(firePoint.right);
            Shoot(-firePoint.up);
            Shoot(-firePoint.right);
        }
        if(random==3){
            Shoot(Direction.position-firePoint.position);
        }
    }
}
