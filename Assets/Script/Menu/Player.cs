using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviour
{
    CharacterController controller;
    Animator anime;

    PhotonView pv;

    Vector3 moveDir;

    float moveSpeed;
    float jumpPower;
    float gravity;
    float viewDirX;
    float rotSpeed;

    bool freeLook;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anime = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();

        moveSpeed = 5.0f;
        jumpPower = 3.0f;
        gravity = 20.0f;
        rotSpeed = 3.0f;

        anime.SetBool("Idle", true);

        if (pv.IsMine)
            ViewSetting();
    }

    void Update()
    {
        if (pv.IsMine)
        {
            Move();
            SightChange();
            if ((!GameMng.I.isChatting && Input.GetKeyDown(KeyCode.R)) || transform.localPosition.y <= -30f)
                Respawn();
        }
    }

    float h => Input.GetAxis("Horizontal");
    float v => Input.GetAxis("Vertical");
    void Move()
    {
        if (!freeLook)
            viewDirX += Input.GetAxis("Mouse X") * rotSpeed;

        if (GameMng.I.isChatting)
        {
            moveSpeed = 0f;
            rotSpeed = 0f;
            jumpPower = 0f;
        }
        else
        {
            moveSpeed = 5.0f;
            rotSpeed = 3.0f;
            jumpPower = 3.0f;
        }
        if (controller.isGrounded)
        {
            anime.SetBool("Jump", false);
            moveDir = new Vector3(h, 0f, v);
            moveDir = transform.TransformDirection(moveDir);

            moveDir *= moveSpeed;

            if (Input.GetButton("Jump") && !GameMng.I.isChatting)
            {
                moveDir.y = jumpPower;
                anime.SetBool("Run", false);
                anime.SetBool("Jump", true);
            }
            if (controller.velocity.Equals(Vector3.zero))
            {
                anime.SetBool("Run", false);
                anime.SetBool("Idle", true);
            }
            else
            {
                anime.SetBool("Idle", false);
                anime.SetBool("Run", true);
            }
        }
        transform.rotation = Quaternion.Euler(0f, viewDirX, 0f);
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    bool view3;
    void SightChange()
    {
        if (!GameMng.I.isChatting)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (!view3)
                {
                    GameMng.I.vCam.gameObject.SetActive(true);
                    view3 = true;
                }
                else
                {
                    GameMng.I.vCam.gameObject.SetActive(false);
                    GameMng.I.mainCam.transform.localPosition = new Vector3(0f, 1.35f, 0.1f);
                    GameMng.I.mainCam.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    view3 = false;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (view3)
                {
                    GameMng.I.flCam.gameObject.SetActive(true);
                    freeLook = true;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (view3)
                {
                    GameMng.I.flCam.gameObject.SetActive(false);
                    freeLook = false;
                }
            }
        }
    }

    void Respawn()
    {
        int idx = Random.Range(0, GameMng.I.points.Length);
        transform.position = GameMng.I.points[idx].position;
    }

    void ViewSetting()
    {
        view3 = true;
        freeLook = false;

        GameMng.I.mainCam.transform.parent = transform;

        GameMng.I.vCam.Follow = transform;
        GameMng.I.vCam.LookAt = transform;

        GameMng.I.flCam.Follow = transform;
        GameMng.I.flCam.LookAt = transform;

        GameMng.I.flCam.gameObject.SetActive(false);
    }
}
