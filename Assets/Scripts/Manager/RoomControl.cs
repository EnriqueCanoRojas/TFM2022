using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    bool m_Started;
    public LayerMask m_LayerMask;

    // Start is called before the first frame update
    void Start()
    {
        StartRoom = false;
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
                    RoomInit();
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
    void FixedUpdate()
    {
        //MyCollisions();
    }
    void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(CenterOfRoom.transform.position, CenterOfRoom.transform.localScale*5.5f, Quaternion.identity, m_LayerMask);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            //Output all of the collider names
            Debug.Log("Hit : " + hitColliders[i].name + i);
            //Increase the number of Colliders in the array
            if (hitColliders[i].gameObject.CompareTag("Player"))
            {
                StartRoom = true;
                inRoom = true;
                //SpawnEnemies.
            }
            else {
                StartRoom = false;
                inRoom = false;
            }
            i++;
        }

    }
    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(CenterOfRoom.transform.position, CenterOfRoom.transform.localScale);
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
        for (int i = 0; i < walls.Length-1; i++)
        {
            if(!walls[i].activeSelf)
            {
                BlockDoors[i].SetActive(true);
            }
            else { }
        }
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
