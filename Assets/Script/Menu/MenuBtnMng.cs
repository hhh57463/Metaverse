using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuBtnMng : MonoBehaviour
{
    public Transform selectFrame;
    public TMP_InputField RoomNameField;
    public TMP_InputField NickNameField;

    public void CreateRoomBtn()
    {
        if (!RoomNameField.text.Equals("") && !NickNameField.text.Equals(""))
        {
            userSetting(true, false);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Map");
        }
    }
    public void JoinRoomBtn()
    {
        if (!RoomNameField.text.Equals("") && !NickNameField.text.Equals(""))
        {
            userSetting(false, false);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Map");
        }
    }

    public void RandomRoomBtn()
    {
        if (!NickNameField.text.Equals(""))
        {
            userSetting(false, true);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Map");
        }
    }

    public void CharSelect(int num)
    {
        Mng.I.charNum = num;
        switch (num)
        {
            case 0:
                selectFrame.localPosition = new Vector3(-350f, 20f, 0f);
                break;
            case 1:
                selectFrame.localPosition = new Vector3(-175f, 20f, 0f);
                break;
            case 2:
                selectFrame.localPosition = new Vector3(0f, 20f, 0f);
                break;
            case 3:
                selectFrame.localPosition = new Vector3(175f, 20f, 0f);
                break;
            case 4:
                selectFrame.localPosition = new Vector3(350f, 20f, 0f);
                break;
            case 5:
                selectFrame.localPosition = new Vector3(525f, 20f, 0f);
                break;
        }
    }

    public void SwapScreen()
    {
        Mng.I.fullScreenMode = !Mng.I.fullScreenMode;
        Screen.SetResolution(1280, 720, Mng.I.fullScreenMode);
    }

    void userSetting(bool create, bool random)
    {
        Mng.I.roomName = RoomNameField.text;
        Mng.I.nickName = NickNameField.text;
        Mng.I.createRoom = create;
        Mng.I.randomRoom = random;
    }
}
