using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMaterials : MonoBehaviour
{
    public GameObject[] Pieces;
    public Transform SpawnPos;
    public bool spawned;
    public int areaInXNotAffected;
    public int areaInZNotAffected;
    public int areaInX;
    public int areaInZ;
    public int TimetoSpawn;
    public Vector3 areaS;
    // Update is called once per frame
    void Start()
    {
        SpawnPos = this.gameObject.transform;
        spawned = false;
        InvokeRepeating("Spawn", 2.0f, 1f);
    }
    public void AreaOfSpawn(float areaX, float areaZ, float notInX, float notInZ)
    {
        var i = new float();
        i = Random.Range(-areaX, areaX);
        if(i>0 && i<notInX)
        {
            i = Random.Range(notInX, areaX);
        }
        if(i<0 && i>-notInX)
        {
            i = Random.Range(-notInX, -areaX);
        }
        var j = new float();
        j = Random.Range(-areaZ, areaZ);
        if (j > 0 && i < notInZ)
        {
            j = Random.Range(notInZ, areaZ);
        }
        if (j < 0 && i > -notInZ)
        {
            j = Random.Range(-notInZ, -areaZ);
        }
        areaS = new Vector3(i, 150, j);
    }
    public void Spawn()
    {
        AreaOfSpawn(areaInX, areaInZ, areaInXNotAffected, areaInZNotAffected);
        var i = (int)Random.Range(0, Pieces.Length - 1);
        Instantiate(Pieces[i], areaS, Pieces[i].transform.rotation);
    }
    public IEnumerator SpawnedCD(int SpawnRate)
    {
        yield return new WaitForSeconds(SpawnRate);
        spawned = false;
    }
}