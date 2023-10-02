using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public List<GameObject> bulletprefab;
    public float bulletSpeed = 5f;
    private GameObject bullet;

    public void Shoot(Vector3 Direction)
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Direction.Normalize();
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if(GameManager.instance.weapon.weaponLevel>bulletprefab.Count-1)
        {
            bullet = Instantiate(bulletprefab[bulletprefab.Count-1], firePoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
        }else{
            bullet = Instantiate(bulletprefab[GameManager.instance.weapon.weaponLevel], firePoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Direction * bulletSpeed, ForceMode2D.Impulse);
    }
}

