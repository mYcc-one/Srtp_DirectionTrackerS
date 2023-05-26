using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    public GameObject modeButton;
    public Text modeText;

    public GameObject ParamManager; //用于存储模式等参数
    private int mode;

    public void StartGame()
    {
        if (mode >= 3) SceneManager.LoadScene("MainNum");
        else SceneManager.LoadScene("Main");
    }

    public void ShowHelp()
    {
        SceneManager.LoadScene("Help");
    }

    public void ChangeMode()
    {
        ParamManager.GetComponent<ParamController>().ChangeMode();
        mode = ParamManager.GetComponent<ParamController>().GetMode();
        ChangeModeText(mode);
    }

    public void ChangeModeText(int mode)
    {
        switch (mode)
        {
            case 1:
                modeText.text = "Direction";
                break;
            case 2:
                modeText.text = "DirectionCrack";
                break;
            case 3:
                modeText.text = "Number";
                break;
            case 4:
                modeText.text = "NumberCrack";
                break;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void Awake()
    {
        ParamManager = GameObject.Find("ParamManager");
        modeText = modeButton.transform.Find("Text").GetComponent<Text>();
        mode = ParamManager.GetComponent<ParamController>().GetMode(); //重返标题界面要检查mode的值
        ChangeModeText(mode);
        DontDestroyOnLoad(ParamManager); //不要销毁
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
