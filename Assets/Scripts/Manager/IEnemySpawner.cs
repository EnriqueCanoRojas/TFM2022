using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemySpawner : MonoBehaviour
{
    public bool done;
    public RoomControl roomC;
    // The GameObject to instantiate.
    public GameObject entityToSpawn;

    // An instance of the ScriptableObject defined above.
    public SpawnManager spawnManagerValues;

    public GameObject[] drops;
    public GameObject[] spawnsManual;
    // This will be appended to the name of the created entities and increment when each is created.
    int instanceNumber = 1;

    void Start()
    {
        roomC = this.gameObject.GetComponent<RoomControl>();
        done = false;
    }
    void Update()
    {
        if (roomC.StartRoom && !done)
        {
            SpawnEntities();
            done = true;
        }
    }
    public void CreateScriptablesInRuntime()
    {
        var obj = (IEnemies)ScriptableObject.CreateInstance(typeof(IEnemies));
    }
    public void SpawnEntities()
    {
        int currentSpawnPointIndex = 0;

        for (int i = 0; i < spawnManagerValues.numberOfPrefabsToCreate; i++)
        {
            // Creates an instance of the prefab at the current spawn point.
            //GameObject currentEntity = Instantiate(entityToSpawn, spawnManagerValues.spawnPoints[currentSpawnPointIndex], Quaternion.identity);

            GameObject currentEntity = Instantiate(entityToSpawn, spawnsManual[currentSpawnPointIndex].transform.position, Quaternion.identity);
            // Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number.
            if(spawnManagerValues.prefabName=="Slime")
            {
                var obj = (IEnemies)ScriptableObject.CreateInstance(typeof(IEnemies));
                obj.TypeEnemy = EnemyClass.Basic;
                obj.Experience = 20;
                obj.Power = 10;
                obj.MaxEnemyHP = (int)Random.Range(80, 120);
                obj.currentHP = obj.MaxEnemyHP;
                obj.isSpecialDrop = false;
                obj.drops = drops;
                obj.name = spawnManagerValues.prefabName + instanceNumber;
                if (currentEntity.GetComponent<Enemy>())
                {
                    currentEntity.GetComponent<Enemy>().thisEnemy = obj;
                }
            }
            if(spawnManagerValues.prefabName=="Boss")
            {
                var obj = (IEnemies)ScriptableObject.CreateInstance(typeof(IEnemies));
                obj.TypeEnemy = EnemyClass.Boss;
                obj.Experience = 1000;
                obj.Power = 20;
                obj.MaxEnemyHP = 300;
                obj.currentHP = obj.MaxEnemyHP;
                obj.isSpecialDrop = false;
                obj.drops = drops;
                obj.name = spawnManagerValues.prefabName + instanceNumber;
                if (currentEntity.GetComponent<Enemy>())
                {
                    currentEntity.GetComponent<Enemy>().thisEnemy = obj;
                    currentEntity.GetComponent<Enemy>().EndGame = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().EndGame;
                }
            }
            currentEntity.name = spawnManagerValues.prefabName + instanceNumber;

            // Moves to the next spawn point index. If it goes out of range, it wraps back to the start.
            currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnManagerValues.spawnPoints.Length;

            instanceNumber++;
        }
    }
}