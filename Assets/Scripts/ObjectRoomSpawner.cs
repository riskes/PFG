using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }
    
    public Vector2 roomCenter = new Vector2 (0,0);
    public GridController grid;

    public RandomSpawner[] spawnerData;

    /*void Start() 
    {
        //grid = GetComponentInChildren<GridController>();
    }*/
    public void InitObjectSpawner()
    {
        foreach(RandomSpawner rs in spawnerData)
        {
            SpawnObjects(rs);
        }
    }
    void SpawnObjects(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

        for(int i = 0; i < randomIteration; i++)
        {
            int  randomPosition = Random.Range(0, grid.availablePoints.Count - 1);
            GameObject go = Instantiate(data.spawnerData.itemToSpawn[Random.Range(0, data.spawnerData.itemToSpawn.Count)], grid.availablePoints[randomPosition]+roomCenter, Quaternion.identity, transform) as GameObject;
            grid.availablePoints.RemoveAt(randomPosition);
        }
    }
}
