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
        PhotonNetwork.IsMessageQueueRunning = true;     // �����̺�Ʈ ó��
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
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);            // �� �����鿡�� �޽��� ����
        ReceiveMsg(msg);                                                        // ���� �Է��� �޽����� �߰�
        chatInputField.ActivateInputField();                // �޼��� ���� �� ��Ŀ���� Input Field�� ��ȯ (���Ǳ��)
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
