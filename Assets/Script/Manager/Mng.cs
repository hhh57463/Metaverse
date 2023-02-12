using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mng : MonoBehaviour
{
    private static Mng _instance;

    public static Mng I
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
        DontDestroyOnLoad(gameObject);

        Screen.SetResolution(1280, 720, true);
    }

    public string version = "1.0";
    public string nickName;
    public string roomName;
    public bool createRoom;
    public bool randomRoom;
    public int charNum = 0;
    public bool fullScreenMode = true;
}
