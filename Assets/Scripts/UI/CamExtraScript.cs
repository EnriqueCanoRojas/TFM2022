using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CamExtraScript : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public Transform PlayerLocated;
    public GameObject[] rooms;
    public GameObject CurrentRoom;
    public ToggleVariable StartGame;
    public bool ready;

    void Start()
    {
        ready = false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(StartGame.RuntimeToogle)
        {
            if(rooms.Length==0)
                SearchRooms();
            else 
            {
                ControlCamera();
            }
        }
    }
    void SearchRooms()
    {
        rooms = GameObject.FindGameObjectsWithTag("Room");
    }
    void ControlCamera()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i].GetComponent<RoomControl>().inRoom)
            {
                //do something with the first line
                CurrentRoom = rooms[i];
                PlayerLocated = CurrentRoom.transform;
                cinemachineVirtualCamera.m_Follow = PlayerLocated;
            }
            else { }
        }
    }
    void ClearArrays()
    {
        Array.Clear(rooms, 0, rooms.Length);
        ready = true;
    }
}
