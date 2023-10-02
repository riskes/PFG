using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    // Start is called before the first frame update
    public Room room;

    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;

        public float verticalOffset, horitzontalOffset;
    }

    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new List<Vector2>();

    void Awake()
    {
        room = GetComponentInParent<Room>();
        if(room == null)
        {
            Debug.Log("NULL");
        }else{  
            Debug.Log("NO NULL");
        }
        grid.columns = (int)room.Width -4;
        grid.rows = (int)room.Height -6;
        GenerateGrid();    
    }
    public void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horitzontalOffset += room.transform.localPosition.x;
        Debug.Log("ofsset grid "+ grid.verticalOffset+grid.horitzontalOffset);
        for(int y = 0; y < grid.rows ; y++)
        {
            for(int x = 0; x < grid.columns ; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.transform.position = new Vector2(x -(grid.columns -grid.horitzontalOffset),y - (grid.rows - grid.verticalOffset));
                go.name = "X: "+ x + "Y: " + y;
                availablePoints.Add(go.transform.position);
            }
        }
        if(GetComponentInParent<Room>().GetComponent<BossSpawn>() == null)
        {
            GetComponentInParent<ObjectRoomSpawner>().InitObjectSpawner();
        }
    }
}
