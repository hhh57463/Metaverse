using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    CharacterController controller;
    Animator anime;

    CinemachineVirtualCamera vCamera;
    Camera mainCam;

    Vector3 moveDir;
    float moveSpeed;
    float jumpPower;
    float gravity;
    float viewDirX;
    //float viewDirY;
    float rotSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anime = GetComponent<Animator>();
        vCamera = FindObjectOfType<CinemachineVirtualCamera>();
        mainCam = Camera.main;
        mainCam.transform.parent = transform;
        moveDir = Vector3.zero;
        moveSpeed = 5.0f;
        jumpPower = 8.0f;
        gravity = 20.0f;
        rotSpeed = 3.0f;
        //viewDirY = 0.0f;
        anime.SetBool("Idle", true);
        anime.SetBool("Run", false);
        anime.SetBool("Jump", false);
        view3 = true;
    }

    void Update()
    {
        Move();
        SightChange();
    }

    float h => Input.GetAxis("Horizontal");
    float v => Input.GetAxis("Vertical");
    void Move()
    {
        viewDirX += Input.GetAxis("Mouse X") * rotSpeed;
        //if (vCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y >= 0f && vCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y <= 3f)
        //    viewDirY += Input.GetAxis("Mouse Y");
        //else
        //{
        //    if (vCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y > 3.0f)
        //        vCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 3.0f;
        //    else if (vCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y < 0.0f)
        //        vCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 0.0f;
        //}
        if (controller.isGrounded)
        {
            anime.SetBool("Jump", false);
            moveDir = new Vector3(h, 0f, v);
            moveDir = transform.TransformDirection(moveDir);

            moveDir *= moveSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDir.y = jumpPower;
                anime.SetBool("Run", false);
                anime.SetBool("Jump", true);
            }
            if (controller.velocity == Vector3.zero)
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
        //vCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = viewDirY;
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    bool view3;
    void SightChange()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!view3)
            {
                vCamera.gameObject.SetActive(true);
                view3 = true;
            }
            else
            {
                vCamera.gameObject.SetActive(false);
                view3 = false;
                mainCam.transform.localPosition = new Vector3(0f, 1.35f, 0f);
                mainCam.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }
}
