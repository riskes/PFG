using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeLocationPath : MonoBehaviour
{
    AstarPath astar;
    Pathfinding.AstarData data;
    Pathfinding.GridGraph gg;
    public CameraController camera;

    void Start()
    {
     	GameObject A = GameObject.Find ("Astar");
        astar = A.GetComponent("AstarPath") as AstarPath;
        data = AstarPath.active.astarData;
        gg = data.AddGraph(typeof(Pathfinding.GridGraph)) as Pathfinding.GridGraph;

        gg.is2D = true;
        gg.width = 25;  
        gg.depth = 12;
        gg.nodeSize = 1f;
        gg.collision.use2D = true;
        gg.center=camera.GetCameraTargetPosition();
        gg.center.z=3f;
        gg.UpdateSizeFromWidthDepth();
        AstarPath.active.Scan();
    }
    private void Update()  
    {
        if(camera.IsSwitchingScene())
        {   
            ChangeGridLocation();
        }    
    }
    private void ChangeGridLocation()
    {
        gg.center = camera.GetCameraTargetPosition();
        gg.center.z=3f;
        AstarPath.active.Scan();
    }
}
