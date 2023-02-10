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
    }

    public string version = "1.0";
    public string nickName;
    public string RoomName;
    public bool MakingRoom;
    public bool RandomRoom;
}
