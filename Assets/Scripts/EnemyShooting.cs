using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Shooting
{
    // Start is called before the first frame update
    
    public Transform Direction;
    public float cadence;
    public bool coordinates;
    public bool invertedtornado;
    public bool tornado;
    public bool player;
    private Vector2 dir1 = new Vector2(0,1);
    private Vector2 dir2 = new Vector2(0,-1);
    private Vector2 dir3 = new Vector2(1,0);
    private Vector2 dir4 = new Vector2(-1,0);


    public void StartShooting()
    {
        InvokeRepeating("ShootBullet", 2.0f, cadence);
    }
    void Update()
    {
        if(Direction==null && GameObject.Find("Player")!=null)
        {
            Direction = GameObject.Find("Player").GetComponent<Transform>();
        }
    }

    void ShootBullet(){
        if(invertedtornado)
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
        if(tornado)
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
        if(coordinates){
            Shoot(firePoint.up);
            Shoot(firePoint.right);
            Shoot(-firePoint.up);
            Shoot(-firePoint.right);
        }
        if(player){
            Shoot(Direction.position-firePoint.position);
        }
    }
}
