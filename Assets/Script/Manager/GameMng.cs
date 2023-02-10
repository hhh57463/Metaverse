using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameMng : MonoBehaviour
{
    private static GameMng _instance = null;

    public static GameMng I
    {
        get
        {
            if (_instance.Equals(null))
                Debug.Log("Instance is null");
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    public Camera mainCam;
    public CinemachineVirtualCamera vCam;
    public CinemachineFreeLook flCam;

    public Transform spawnPoint;

    public bool isChatting = false;
}
