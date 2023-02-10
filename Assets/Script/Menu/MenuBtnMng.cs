using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBtnMng : MonoBehaviour
{
    public void PlayBtn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Map");
    }
}
