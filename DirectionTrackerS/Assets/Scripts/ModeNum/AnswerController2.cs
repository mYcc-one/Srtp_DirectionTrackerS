using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 题目格式：
   1.\t\t\t\tIncorrect..(Correct !)
   2.\t\tWhat's the value of
   3.\t\t\t\t  Box X ?
 */

public class AnswerController2 : MonoBehaviour
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

    public void ShowQuestionCrack(object[] message) //测试版本
    {
        string id = message[0].ToString();
        string answer = message[1].ToString();
        question.text = "\t\tWhat's the value of\n\t\t\t\tBox " + id + " ?"
            + " (" + answer + ")";
    }

    public void AssignValue(int id, int answer)
    {
        bool[] idArray = new bool[4];
        for(int i = 0; i < 4; i++) { idArray[i] = true; }
        idArray[id] = false;

        int count = 0; Text[] textArray = new Text[3];
        while(count < 3)
        {
            int temp = Random.Range(0, 4);
            while (idArray[temp] == false)
            {
                temp = Random.Range(0, 4);
            }
            idArray[temp] = false;

            if(temp == 0) { textArray[count] = btn1.transform.Find("Text").GetComponent<Text>(); }
            else if (temp == 1) { textArray[count] = btn2.transform.Find("Text").GetComponent<Text>(); }
            else if (temp == 2) { textArray[count] = btn3.transform.Find("Text").GetComponent<Text>(); }
            else { textArray[count] = btn4.transform.Find("Text").GetComponent<Text>(); }

            int t1 = Random.Range(0, 2);
            switch (count)
            {
                case 0:
                    if (t1 == 1){ textArray[count].text = (answer + 1).ToString(); }
                    else { textArray[count].text = (answer + 2).ToString(); }
                    break;
                case 1:
                    if (t1 == 1)
                    {
                        if (answer - 1 >= 0) { textArray[count].text = (answer - 1).ToString(); }
                        else { textArray[count].text = (answer + 3).ToString(); }
                    }
                    else
                    {
                        if (answer - 2 >= 0) { textArray[count].text = (answer - 2).ToString(); }
                        else { textArray[count].text = (answer + 4).ToString(); }
                    }
                    break;
                case 2:
                    if (t1 == 1)
                    {
                        if (answer - 3 >= 0) { textArray[count].text = (answer - 3).ToString(); }
                        else { textArray[count].text = (answer + 5).ToString(); }
                    }
                    else
                    {
                        if (answer - 4 >= 0) { textArray[count].text = (answer - 4).ToString(); }
                        else { textArray[count].text = (answer + 7).ToString(); }
                    }
                    break;
                default:
                    Debug.LogError("AnswerControllerError");
                    break;
            }
            count++;
        }
    }

    public void GiveOptions(int answer)
    {
        correctId = Random.Range(0, 4); //正解序号
        Text text;
        switch (correctId)
        {
            case 0:
                text = btn1.transform.Find("Text").GetComponent<Text>();
                text.text = answer.ToString();
                AssignValue(0, answer);
                break;
            case 1:
                text = btn2.transform.Find("Text").GetComponent<Text>();
                text.text = answer.ToString();
                AssignValue(1, answer);
                break;
            case 2:
                text = btn3.transform.Find("Text").GetComponent<Text>();
                text.text = answer.ToString();
                AssignValue(2, answer);
                break;
            case 3:
                text = btn4.transform.Find("Text").GetComponent<Text>();
                text.text = answer.ToString();
                AssignValue(3, answer);
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
