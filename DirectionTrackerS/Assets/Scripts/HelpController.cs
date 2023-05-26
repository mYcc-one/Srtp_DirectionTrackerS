using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpController : MonoBehaviour
{
    public GameObject ParamManager;
    public GameObject DirectionText;
    public GameObject NumberText;

    void Awake()
    {
        ParamManager = GameObject.Find("ParamManager");
        int mode = ParamManager.GetComponent<ParamController>().GetMode();
        DontDestroyOnLoad(ParamManager);
        if (mode <= 2)
        {
            DirectionText.SetActive(true);
            NumberText.SetActive(false);
        }
        else
        {
            DirectionText.SetActive(false);
            NumberText.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }        
    }
}
