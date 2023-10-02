using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string currentWorldName = "Basement";
    public string bossroom;
    Room currentRoom;
    AudioManager audioManager;

    RoomInfo currentLoadRoomData;
    public AudioClip BSOFloor;
    public AudioClip BSOBoss;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom =false;

    bool openRooms = false;

    bool spawnedBossRoom = false;

    bool updatedRooms = false;
    void Awake() {
        instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.canviBSO(BSOFloor);
    }
 
    void Update()
    {
        UpdateRoomQueue();
        if(updatedRooms)
        {
            if(currentRoom.enemyCounter()==0 && openRooms ==  false)
            {
                openRoom();
                if(currentRoom.name.Contains("End"))
                {
                    GameObject Stairs = GameObject.Find("Stairs");
                    Stairs.GetComponent<BoxCollider2D>().enabled = true;
                    Stairs.GetComponentInChildren<SpriteRenderer>().enabled = true;
                }
            }
        }
    }
    void UpdateRoomQueue()
    {
        if(isLoadingRoom)
        {
            return;
        }
        if(loadRoomQueue.Count == 0)
        {
            if(!spawnedBossRoom)
            {
                spawnedBossRoom = true;
                StartCoroutine(SpawnBossRoom());
            }
            else if(spawnedBossRoom && !updatedRooms)
            {
                foreach(Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms = true;
            }
            return;
        }
        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }
    public void openRoom()
    {
        foreach(Room room in loadedRooms)
        {
            room.deactiveDoors();
        }
        openRooms =true;
    }
    private void closeRoom()
    {
        foreach(Room room in loadedRooms)
        {
            room.activeDoors();
        }
        openRooms=false;
    }
    IEnumerator SpawnBossRoom()
    {
        yield return new WaitForSeconds(0.15f);
        if(loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count -1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Find(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom(bossroom+"End", tempRoom.X,tempRoom.Y);
        }
        
            openRoom();
    }
    public void LoadRoom(string name, int x, int y)
    {
        if(DoesRoomExist(x, y)){
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }
    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false)
        {
            yield return null;
        }
    }
    public void RegisterRoom(Room room)
    {
        if(!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(
            currentLoadRoomData.X * room.Width,
            currentLoadRoomData.Y * room.Height,
            0
            );

            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;
            if(loadedRooms.Count == 0)
            {
                CameraController.instance.currentRoom = room;
            }
            loadedRooms.Add(room); 
            //room.RemoveUnconnectedDoors();
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
        
    }

    public bool DoesRoomExist( int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }
    public Room FindRoom( int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currentRoom = room;
        currentRoom.DestroyShadow();
        if(currentRoom.enemyCounter()>0){
            closeRoom();
        }
        if(currentRoom.enemiesMoving==false)
        {
            currentRoom.MoveEnemy();
        }
        if(currentRoom.name.Contains("demonEnd"))
        {
            currentRoom.GetComponent<BossSpawn>().ActivarSpawners();
        }
        if(currentRoom.name.Contains("End")){
            
            audioManager.canviBSO(BSOBoss);
        }
        
    }
}
