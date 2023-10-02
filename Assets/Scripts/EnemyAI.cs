using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : Fighter
{

    public float speed = 200f;
    public int xpValue = 1;
    public int gold = 1;
    //public float triggerLength = 1;
    public Transform playerTransform;
    //private Vector2 startingPosition;
    public float DistanceFromPlayer= 0;
    Vector2 direction;
    //private Collider2D[] hits = new Collider2D[10];

    public float nextWaypointDistance = 3f;

    public Animator animator;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndofPath = false;
    Seeker seeker;
    Rigidbody2D rb;
   
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        
        playerTransform = GameObject.Find("Player").transform;
        //startingPosition = transform.position;
        
    }
    public void StartMoving()
    {
        InvokeRepeating("UpdatePath", 0f, .5f);
    }
     void UpdatePath(){
        if(seeker.IsDone()){
            seeker.StartPath (rb.position, playerTransform.position, OnpathComplete);
        }
    }
    void OnpathComplete(Path p)
    {
        if(!p.error){
            path = p;
            currentWaypoint = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform!=null)
        {
            animator.SetFloat("TargetPos", playerTransform.position.x-rb.position.x);
        }
    }
    void FixedUpdate()
    {

        if(path==null){
            return;
        }
        
        if(currentWaypoint >= path.vectorPath.Count){
            reachedEndofPath = true;
            return;
        }else{
            reachedEndofPath = false;
        }
        if(Vector2.Distance((Vector2)playerTransform.position,rb.position)<DistanceFromPlayer){
            direction = -((Vector2)path.vectorPath[currentWaypoint]-rb.position).normalized;
    
        }else{
            if(((Vector2)path.vectorPath[currentWaypoint]-rb.position)!=Vector2.zero)
            {
                if(((Vector2)path.vectorPath[currentWaypoint]-rb.position)!=Vector2.zero)
                {
                    direction = ((Vector2)path.vectorPath[currentWaypoint]-rb.position).normalized;
                }
            }
        }
        Vector2 force = direction * speed * Time.deltaTime;
        if(Mathf.Abs(pushDirection.x) + Mathf.Abs(pushDirection.y) > 0.3 ){
            force += (pushDirection-direction) * speed * Time.fixedDeltaTime;
            
        }
        if(pushRecoverySpeed<0)
        {
        }
        pushDirection = Vector2.Lerp(pushDirection, Vector2.zero, pushRecoverySpeed);
        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance){
            currentWaypoint++;
        }
    }
    protected override void Death(){
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        if(xpValue>0)
        {
            GameManager.instance.ShowText("+"+xpValue+" exp",30,  Color.cyan,transform.position , Vector3.up*40, 1.0f );
        }
        GameManager.instance.gold += gold;
        if(gold>0)
        {
            GameManager.instance.ShowText("+"+gold+" gold",30,   Color.yellow, transform.position ,Vector3.up*20, 1.0f );
        }
    }
}
