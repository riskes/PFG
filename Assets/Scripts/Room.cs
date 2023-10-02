using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float Width;

    public float Height;

    //private GameObject player;

    public int X;

    public int Y;

    public bool enemiesMoving = false;
    
    public bool updatedDoors = false;

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }
    public GameObject Shadow;

    public List<Door> doors = new List<Door>();
    // Start is called before the first frame update
    void Start()
    {

        if(RoomController.instance == null)
        {
            Debug.Log("Wrong Scene");
            return;
        }
        Door[] drs = GetComponentsInChildren<Door>();
        foreach(Door d in drs)
        {
            doors.Add(d);
        }
        RoomController.instance.RegisterRoom(this);
    }

    void Update()
    {
        if(name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }

    public void RemoveUnconnectedDoors()
    {
        foreach(Door door in doors)
        {
            
            switch(door.doorType)
            {
                case Door.DoorType.right:
                if(GetRight() == null)
                {
                    door.functional=false;
                }
                break;
                case Door.DoorType.left:
                if(GetLeft() == null)
                {
                    door.functional=false;
                }
                break;
                case Door.DoorType.top:
                if(GetTop() == null)
                {
                    door.functional=false;
                }
                break;
                case Door.DoorType.bottom:
                if(GetBottom() == null)
                {
                    door.functional=false;
                }
                break;
            }
        }
    }
    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(X+1,Y))
        {
            return RoomController.instance.FindRoom(X+1,Y);
        }
        else{
            return null;
        }
    }
    public Room GetLeft()
    {
        if(RoomController.instance.DoesRoomExist(X-1,Y))
        {
            return RoomController.instance.FindRoom(X-1,Y);
        }
        else{
            return null;
        }
    }
    public Room GetTop()
    {
        if(RoomController.instance.DoesRoomExist(X,Y+1))
        {
            return RoomController.instance.FindRoom(X,Y+1);
        }
        else{
            return null;
        }
    }
    public Room GetBottom()
    {
        if(RoomController.instance.DoesRoomExist(X,Y-1))
        {
            return RoomController.instance.FindRoom(X,Y-1);
        }
        else{
            return null;
        }
    }
    /*public void noEnemies()
    {
        if(enemyCounter()<=0)
        {
            RoomController.instance.openRoom();
        }
    }*/

   public Vector3 GetRoomCenter()
   {
        return new Vector3(X*Width,Y*Height);
   }
   void OnTriggerEnter2D(Collider2D other)
   {
        if(other.tag == "Fighter" && other.name == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
   }

    public int enemyCounter()
    {
        return GetComponentsInChildren<EnemyAI>().Length;
    }
   public void activeDoors()
   {
        foreach(Door door in doors) {
        {
            door.activeBC();
        }
    }

   }

   public void deactiveDoors()
   {
        foreach(Door door in doors) 
        {
            door.deactiveBC();
        }
    
   }
   public void DestroyShadow()
   {
        Destroy(Shadow.gameObject);
   }
   public void MoveEnemy()
   {    enemiesMoving=true;
        EnemyAI[] enemies = GetComponentsInChildren<EnemyAI>();
        foreach(EnemyAI enemy in enemies)
        {
            enemy.StartMoving();
        }
        EnemyShooting[] enemiesgun = GetComponentsInChildren<EnemyShooting>();
        foreach(EnemyShooting enemy in enemiesgun)
        {
            Debug.Log("enemyStartShooting");
            enemy.StartShooting();
        }
        FairyBossShooting[] enemiesbossgun = GetComponentsInChildren<FairyBossShooting>();
        foreach(FairyBossShooting enemy in enemiesbossgun)
        {
            enemy.StartBossShooting();
        }
   }
}
