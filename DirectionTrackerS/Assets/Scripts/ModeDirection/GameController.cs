using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject AnswerCanvas;
    public GameObject PauseCanvas;
    public GameObject BoxManager;
    public GameObject ParamManager;

    public AudioClip CorrectAudio;
    public AudioClip CorrectAudio2;
    public AudioClip IncorrectAudio;

    private AudioSource Audio;

    private int mode; //1-2

    private int state;
    private int oldState;
    private bool isCorrect;

    private bool isPaused;

    private int lives;
    private int level; //也可以传值得到

    public void ShowQuestion(object[] ob)
    {
        string id = (int.Parse(ob[0].ToString()) + 1).ToString();
        int answer = int.Parse(ob[1].ToString());
        AnswerCanvas.SetActive(true);
        AnswerCanvas.SendMessage("GiveOptions", answer);

        //AnswerCanvas.SendMessage("ShowQuestion", id);

        object[] message = new object[2];
        message[0] = id;
        message[1] = ob[1].ToString();
        if (mode == 2)
            AnswerCanvas.SendMessage("ShowQuestionCrack", message);
        else AnswerCanvas.SendMessage("ShowQuestion", message); //mode==1

        BoxManager.SendMessage("ShowOrder");
    }

    public void IsAnswerCorrect(bool IsCorrect)
    {
        isCorrect = IsCorrect;
        /* 处理生命，计算分数等 */
        if (isCorrect) //加分
        {
            level++;
            if(level <= 8)
            {
                Audio.clip = CorrectAudio;
            }
            else
            {
                Audio.clip = CorrectAudio2;
            }
            if (!Audio.isPlaying) { Audio.Play(); }
        }
        else
        {
            lives--;
            Audio.clip = IncorrectAudio;
            if (!Audio.isPlaying) { Audio.Play(); }
        }
        if(lives <= 0) //GameOver
        {
            state = 3;
            AnswerCanvas.SendMessage("GameOver", level);
            BoxManager.SendMessage("ShowValue");
        }
        else
        {
            BoxManager.SendMessage("AnswerResponse", isCorrect);
        }
    }

    public void CanSetFalseToCanvas()
    {
        if(state != 3) //游戏未结束
        {
            AnswerCanvas.SetActive(false);
        }
    }

    public void ChangeState(int State)
    {
        oldState = state;
        state = State;
    }

    public void Continue()
    {
        isPaused = false;
        Time.timeScale = 1F;
        PauseCanvas.SetActive(false);
        if (AnswerCanvas.activeSelf)
        {
            AnswerCanvas.SendMessage("OnPause", false);
        }
        state = oldState;
    }

    void Awake()
    {
        ParamManager = GameObject.Find("ParamManager");
        mode = ParamManager.GetComponent<ParamController>().GetMode();
        DontDestroyOnLoad(ParamManager);

        Audio = GetComponent<AudioSource>();
        state = 0; //
        oldState = state;
        lives = 3; //changeable
        level = 0;
        isCorrect = false;
        isPaused = false;
        AnswerCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
    }

    void Update() //所有按键点击由GameController控制
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
                Time.timeScale = 1F;
                PauseCanvas.SetActive(false);
                if (AnswerCanvas.activeSelf)
                {
                    AnswerCanvas.SendMessage("OnPause", false);
                }
                state = oldState; //Restore
            }
            else
            {
                isPaused = true;
                Time.timeScale = 0F;
                PauseCanvas.SetActive(true);
                if (AnswerCanvas.activeSelf)
                {
                    AnswerCanvas.SendMessage("OnPause", true);
                }
                oldState = state;
                state = 0;//
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            switch (state)
            {
                case 0:
                    break;
                case 1: //wait to rotate
                    BoxManager.SendMessage("StartRotation"); //晋升大关时需要暂停以显示值
                    oldState = state;
                    state = 2; //旋转，显示题目，无事可做
                    break;
                case 2: //answerQuestion
                    break;
                case 3: //GameOver
                    SceneManager.LoadScene("Title"); //游戏结束，返回标题
                    break;
            }
        }
    }
}
