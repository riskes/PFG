using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTrigger : MonoBehaviour
{
    public Transform player;
    public Vector2 relativePoint;
    private float distance;
    private Animator anim;
    public float cooldown = 5f;
    private float cooldownCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player==null && GameObject.Find("Player")!=null)
        {
            player = GameObject.Find("Player").GetComponent<Transform>();
        }
        distance = Vector3.Distance(player.position, transform.position);
        Debug.Log("DISTANCIA: "+distance);
        relativePoint = transform.InverseTransformPoint(player.position);
        if(cooldownCounter <= 0)
        {
            if(relativePoint.x < 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y) && distance < 4f)
            {
                anim.SetTrigger("SlashLeft");

            }
            if(relativePoint.x > 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y) && distance < 4f)
            {
                anim.SetTrigger("SlashRight");
            }
            if(relativePoint.y > 0f && Mathf.Abs(relativePoint.x) < Mathf.Abs(relativePoint.y) && distance < 4f)
            {
                anim.SetTrigger("SlashUp");
            }
            if (relativePoint.y < 0f && Mathf.Abs(relativePoint.x) < Mathf.Abs(relativePoint.y) && distance < 4f)
            {
                anim.SetTrigger("SlashDown");
            }
            cooldownCounter = cooldown;
        }
        if(cooldownCounter > 0)
        {
            cooldownCounter -= Time.deltaTime;
        }
    }
}
