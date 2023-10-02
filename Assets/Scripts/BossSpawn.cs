using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : ObjectRoomSpawner
{
    AudioManager audioManager;
    public float spawnInterval;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        roomCenter = GetComponentInParent<Room>().GetRoomCenter();
    }
    public void ActivarSpawners()
    {
        roomCenter = GetComponentInParent<Room>().GetRoomCenter();
        InvokeRepeating("SpawnMonster",0.5f,spawnInterval);
    }
    public void SpawnMonster()
    {
        
        if(GameObject.Find("Boss")!=null)
        {
            audioManager.PlaySFX(audioManager.invoke);
            InitObjectSpawner();
            GetComponentInParent<Room>().MoveEnemy();
        }
    }

}
