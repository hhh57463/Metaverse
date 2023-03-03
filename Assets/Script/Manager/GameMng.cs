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

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Camera mainCam;
    public CinemachineVirtualCamera vCam;
    public CinemachineFreeLook flCam;

    public Transform spawnPoint;
    public Transform[] points;
    public bool isChatting = false;

    void Start()
    {
        // 캐릭터 출현 정보를 배열에 저장
        points = spawnPoint.GetComponentsInChildren<Transform>();
    }
}
