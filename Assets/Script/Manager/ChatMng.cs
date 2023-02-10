using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class ChatMng : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI chatLog;
    public TMP_InputField chatInputField;
    public ScrollRect chatScroll;

    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;     // 수신이벤트 처리
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (GameMng.I.isChatting)
                MsgSend();
            else
                chatInputField.ActivateInputField();
        }
        GameMng.I.isChatting = chatInputField.isFocused;
    }

    void MsgSend()
    {
        if (chatInputField.text.Equals(""))
        {
            Debug.Log("Empty msg");
            return;
        }
        string msg = string.Format("[{0}] {1}", PhotonNetwork.LocalPlayer.NickName, chatInputField.text);
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);            // 각 유저들에게 메시지 전송
        ReceiveMsg(msg);                                                        // 내가 입력한 메시지도 추가
        chatInputField.ActivateInputField();                // 메세지 전송 후 포커스를 Input Field로 변환 (편의기능)
        chatInputField.text = "";
    }

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        if (chatLog.text.Equals(""))
            chatLog.text = msg;
        else
            chatLog.text += "\n" + msg;
        chatScroll.verticalNormalizedPosition = 0.0f;
    }
}
