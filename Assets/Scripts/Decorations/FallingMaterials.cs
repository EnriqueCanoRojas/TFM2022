using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMaterials : MonoBehaviour
{
    public GameObject[] Pieces;
    public Transform SpawnPos;
    // Update is called once per frame
    void Start()
    {
        SpawnPos = this.gameObject.transform;
    }
    public void AreaOfSpawn(float Delay, float areaX, float areaZ, Vector3 areaS, bool Spawned)
    {
        float i = new float();
        i = Random.Range(-areaX, areaX);
        float j = new float();
        j = Random.Range(-areaZ, areaZ);
        areaS = new Vector3(i, 150, j);
    }
}