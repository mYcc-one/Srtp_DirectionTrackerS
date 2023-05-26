using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController2 : MonoBehaviour
{
    public GameObject GameManager;

    public void OnBack()
    {
        GameManager.SendMessage("Continue");
    }

    public void OnRestart()
    {
        Time.timeScale = 1F;
        SceneManager.LoadScene("MainNum");
    }

    public void OnReturn()
    {
        Time.timeScale = 1F;
        SceneManager.LoadScene("Title");
    }
}
