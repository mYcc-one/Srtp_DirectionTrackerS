using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 题目格式：
   1.\t\t\t\tIncorrect..(Correct !)
   2.\t\tWhat's the value of
   3.\t\t\t\t  Box X ?
 */

public class AnswerController : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;
    //public GameObject btn1; //也可使用数组
    //public GameObject btn2;
    //public GameObject btn3;
    //public GameObject btn4;

    public GameObject GameManager;
    public Text question;
    public Text judge; //\tCorrect !(\tIncorrect..)

    private int answerId;
    private int correctId;
    private float timer;
    private bool isDisplayJudge;

    public void OnPause(bool isPaused)
    {
        if (isPaused)
        {
            btn1.interactable = false;
            btn2.interactable = false;
            btn3.interactable = false;
            btn4.interactable = false;
        }
        else
        {
            btn1.interactable = true;
            btn2.interactable = true;
            btn3.interactable = true;
            btn4.interactable = true;
        }
    }

    public void ShowQuestion(object[] message) //正式版本
    {
        string id = message[0].ToString();
        question.text = "\t\tWhat's the value of\n\t\t\t\tBox " + id + " ?";
    }

    public void ShowQuestionCrack(object[] message) //破解版本
    {
        string id = message[0].ToString();
        string answer = message[1].ToString();
        string temp = null;
        switch (correctId + 1)
        {
            case 1:
                temp = "up";
                break;
            case 2:
                temp = "down";
                break;
            case 3:
                temp = "left";
                break;
            case 4:
                temp = "right";
                break;
        }
        question.text = "\t\tWhat's the direction of\n\t\t\t\tBox " + id + " ?"
            + " (" + temp + ")"; //同时显示方向序号(1, 2\n 3, 4);
    }

    public void GiveOptions(int answer)
    {
        /* answer对应value,而correctId根据Button的序号顺序 */
        switch (answer)
        {
            case 1:
                correctId = 0;
                break;
            case 2:
                correctId = 3;
                break;
            case 3:
                correctId = 1;
                break;
            case 4:
                correctId = 2;
                break;
        }
        /* 0204-设置按钮可点击 */
        btn1.interactable = true;
        btn2.interactable = true;
        btn3.interactable = true;
        btn4.interactable = true;
    }

    public void GetAnswer(int id)
    {
        /* 0204-设置按钮不可点击 */
        btn1.interactable = false;
        btn2.interactable = false;
        btn3.interactable = false;
        btn4.interactable = false;
        /* 原逻辑 */
        answerId = id;
        if(answerId == correctId) //显示完成后，提示GameManager是否结束游戏，或者进行下一步
        {
            judge.text = "<color=#E6F0F0>\tCorrect !</color>";
            isDisplayJudge = true;
            GameManager.SendMessage("IsAnswerCorrect", true);
        }
        else
        {
            judge.text = "<color=#FF78AA>\tIncorrect..</color>";
            isDisplayJudge = true;
            GameManager.SendMessage("IsAnswerCorrect", false);
        }
    }

    public void GameOver(int level)
    {
        question.text = "\t\t\tGame is Over !\n\t  You've reached level " + level + ".";
    }

    void Start()
    {
        isDisplayJudge = false;
        timer = 0.25f;
        judge.text = "";
    }

    void Update()
    {
        if (isDisplayJudge)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                judge.text = "";
                timer = 0.25f;
                isDisplayJudge = false;
                GameManager.SendMessage("CanSetFalseToCanvas"); //通知可隐藏Canvas(或者直接灭活？)
            }
        }
    }
}
