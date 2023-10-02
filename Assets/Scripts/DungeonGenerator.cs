using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    public string StartRoom;
    public List<string> Rooms;
    public List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom(StartRoom,0, 0);
        foreach(Vector2Int roomLocation in rooms)
        {

            RoomController.instance.LoadRoom(Rooms[Random.Range(0,Rooms.Count)], roomLocation.x, roomLocation.y);
            
        }
    }
}
