using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomControl: MonoBehaviour
{
    public IEnemySpawner Spawner;
    private RoomBehaviour roomBehaviour;
    public bool StartRoom;
    //up,down,right,left
    public GameObject[] BlockDoors;
    public GameObject[] walls;
    public GameObject CenterOfRoom;
    public bool inRoom;
    public bool finishRoom;

    public GameObject[] Enemies;

    bool m_Started;
    public LayerMask m_LayerMask;


    // Start is called before the first frame update
    void Start()
    {
        StartRoom = false;
        finishRoom = false;
        Spawner = this.gameObject.GetComponent<IEnemySpawner>();
        //Añadir un Ignore Collision.
    }

    // Update is called once per frame
    void Update()
    {
        if(Spawner && !finishRoom)
        {
            // Si la habitacion se inicia se bloquean las salidas entonces se puede iniciar la pelea.
            // Si la habitacion termina se desbloquean las salidas.
            switch (inRoom)
            {
                case true:
                    StartCoroutine(StartingRoom());
                    break;
                case false:
                    //RoomFinish();
                    break;
            }
        }
        if (finishRoom && StartRoom)
            RoomFinish();
        else { }
    }
    void EnemiesCount()
    {
        if (Enemies.Length == 0)
        {
            Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        if (Enemies.Length > 0)
        {
            var i = 0;
            var alive = 0;
            if (Enemies[i] == null)
            {
                alive -= 1;
                i++;
            }
            if (alive == 0)
            {
                finishRoom = true;
            }
        }
    }
    void RoomFinish()
    {
        for (int i = 0; i < walls.Length - 1; i++)
        {
            if (!walls[i].activeSelf)
            {
                BlockDoors[i].SetActive(false);
            }
            else { }
        }
        StartRoom = false;
    }
    void RoomInit()
    {
        StartRoom = true;    
        if (inRoom)
        {
            for (int i = 0; i < walls.Length - 1; i++)
            {
                if (!walls[i].activeSelf)
                {
                    BlockDoors[i].SetActive(true);
                }
                else { }
            }
        }
    }
    IEnumerator StartingRoom()
    {
        yield return new WaitForSeconds(1f);
        RoomInit();
        EnemiesCount();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartRoom = true;
            inRoom = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inRoom = false;
        }
    }
}
