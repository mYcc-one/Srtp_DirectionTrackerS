using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxController : MonoBehaviour
{
    /* 难度方案：2[(0, 2, -5), (10, 2, -5)] 
       4[(0, 2, 0), (10, 2, 0), (10, 2, -10), (0, 2, -10)] 
       6[4 + (5, 2, 0), (5, 2, -10)]
       8[6 + (10, 2, -5), (0, 2, -5)] */

    public int level; //1-3: 2; 4-8: 4; 9-15: 6; 16-: 8;
    public GameObject GameManager;

    public GameObject[] BoxArray = new GameObject[8]; //changeable

    private int[] ValueArray = new int[8]; //旋转同时确定值，最后把正解及其他所需参数传递给AnswerController
    //考虑传递的参数：正解，级别（当前使用的Box数）时间等

    private bool isLevelUp;
    private int rotateStep;
    private int curStep;
    private int curSize;
    private int curId; //最近一次旋转的Box序号

    void Start()
    {
        setBoxes();
        level = 0;
        AddLevel();
        CheckLevel();
        ShowValue();
        GameManager.SendMessage("ChangeState", 1);
    }

    void Update()
    {

    }

    public void AnswerResponse(bool isCorrect) //玩家回答后的行为，可再接收一个参数表示是否游戏结束
    {
        if (isCorrect)
        {
            AddLevel();
            CheckLevel();
            ShowValue();
        }
        else
        {
            ShowValue();
            curStep = 0;
        }
        GameManager.SendMessage("ChangeState", 1);
    }

    private void setBoxes()
    {
        BoxArray[0] = GameObject.Find("Box (1)");
        BoxArray[1] = GameObject.Find("Box (2)");
        BoxArray[2] = GameObject.Find("Box (3)");
        BoxArray[3] = GameObject.Find("Box (4)");
        BoxArray[4] = GameObject.Find("Box (5)");
        BoxArray[5] = GameObject.Find("Box (6)");
        BoxArray[6] = GameObject.Find("Box (7)");
        BoxArray[7] = GameObject.Find("Box (8)");
    }

    private void AddLevel()
    {
        level++;
        if (level == 1 || level == 4
            || level == 9 || level == 16)
        {
            isLevelUp = true;
        }
        else { isLevelUp = false; }
    }

    private void SetStep() //由CheckLevel调用
    {
        if (level >= 1 && level <= 3)
        {
            rotateStep = 2 + level;
        }

        else if (level >= 4 && level <= 8)
        {
            rotateStep = level;
        }

        else if (level >= 9 && level <= 15)
        {
            rotateStep = 10;
        }

        else
        {
            rotateStep = 12;
        }

        curStep = 0;
    }

    private void setDirection(int levelType)
    {
        int counter = levelType * 2;
        for (int i = 0; i < counter; i++)
        {
            ValueArray[i] = Random.Range(1, 5);
        }
        for (int j = 0; j < counter; j++)
        {
            /* 根据值启用对应的方向图像 */
            activeRawImage(j, ValueArray[j]);
        }
    }

    private void activeRawImage(int boxId, int order)
    {
        BoxArray[boxId].transform.Find("Canvas/Direction1").GetComponent<RawImage>().enabled = false;
        BoxArray[boxId].transform.Find("Canvas/Direction2").GetComponent<RawImage>().enabled = false;
        BoxArray[boxId].transform.Find("Canvas/Direction3").GetComponent<RawImage>().enabled = false;
        BoxArray[boxId].transform.Find("Canvas/Direction4").GetComponent<RawImage>().enabled = false;
        if (order != 0)
        {
            BoxArray[boxId].transform.Find("Canvas/Direction" + order.ToString()).GetComponent<RawImage>().enabled = true;
        }
    }

    private void CheckLevel()
    {
        if (!isLevelUp) //大关晋级
        {
            SetStep();
            return;
        }

        for (int i = 0; i < 8; i++) //set for later(choose level)
        {
            BoxArray[i].SetActive(false);
        }

        if (level >= 1 && level <= 3)
        {
            BoxArray[0].SetActive(true);
            BoxArray[0].transform.position = new Vector3(0, 2, -5);
            BoxArray[0].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[1].SetActive(true);
            BoxArray[1].transform.position = new Vector3(10, 2, -5);
            BoxArray[1].transform.rotation = Quaternion.Euler(0, 0, 0);

            setDirection(1);

            rotateStep = 2 + level;
            curSize = 2;
        }

        else if (level >= 4 && level <= 8)
        {
            BoxArray[0].SetActive(true);
            BoxArray[0].transform.position = new Vector3(0, 2, 0);
            BoxArray[0].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[1].SetActive(true);
            BoxArray[1].transform.position = new Vector3(10, 2, 0);
            BoxArray[1].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[2].SetActive(true);
            BoxArray[2].transform.position = new Vector3(10, 2, -10);
            BoxArray[2].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[3].SetActive(true);
            BoxArray[3].transform.position = new Vector3(0, 2, -10);
            BoxArray[3].transform.rotation = Quaternion.Euler(0, 0, 0);

            setDirection(2);

            rotateStep = level;
            curSize = 4;
        }

        else if(level >= 9 && level <= 15)
        {
            BoxArray[0].SetActive(true);
            BoxArray[0].transform.position = new Vector3(0, 2, 0);
            BoxArray[0].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[1].SetActive(true);
            BoxArray[1].transform.position = new Vector3(5, 2, 0);
            BoxArray[1].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[2].SetActive(true);
            BoxArray[2].transform.position = new Vector3(10, 2, 0);
            BoxArray[2].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[3].SetActive(true);
            BoxArray[3].transform.position = new Vector3(10, 2, -10);
            BoxArray[3].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[4].SetActive(true);
            BoxArray[4].transform.position = new Vector3(5, 2, -10);
            BoxArray[4].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[5].SetActive(true);
            BoxArray[5].transform.position = new Vector3(0, 2, -10);
            BoxArray[5].transform.rotation = Quaternion.Euler(0, 0, 0);

            setDirection(3);

            rotateStep = 10;
            curSize = 6;
        }

        else
        {
            BoxArray[0].SetActive(true);
            BoxArray[0].transform.position = new Vector3(0, 2, 0);
            BoxArray[0].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[1].SetActive(true);
            BoxArray[1].transform.position = new Vector3(5, 2, 0);
            BoxArray[1].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[2].SetActive(true);
            BoxArray[2].transform.position = new Vector3(10, 2, 0);
            BoxArray[2].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[3].SetActive(true);
            BoxArray[3].transform.position = new Vector3(10, 2, -5);
            BoxArray[3].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[4].SetActive(true);
            BoxArray[4].transform.position = new Vector3(10, 2, -10);
            BoxArray[4].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[5].SetActive(true);
            BoxArray[5].transform.position = new Vector3(5, 2, -10);
            BoxArray[5].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[6].SetActive(true);
            BoxArray[6].transform.position = new Vector3(0, 2, -10);
            BoxArray[6].transform.rotation = Quaternion.Euler(0, 0, 0);

            BoxArray[7].SetActive(true);
            BoxArray[7].transform.position = new Vector3(0, 2, -5);
            BoxArray[7].transform.rotation = Quaternion.Euler(0, 0, 0);

            setDirection(4);

            rotateStep = 12;
            curSize = 8;
        }

        curStep = 0;
    }

    public void ShowOrder()
    {
        for (int i = 0; i < curSize; i++)
        {
            BoxArray[i].transform.rotation = Quaternion.Euler(0, 0, 0); //复位，使文字正向显示
        }

        Text[] orderArray = new Text[curSize];
        for (int i = 0; i < curSize; i++)
        {
            orderArray[i] = BoxArray[i].transform.Find("OrderCanvas/OrderText").GetComponent<Text>();
            orderArray[i].text = "\n\t" + (i + 1).ToString();
        }
    }

    public void ShowValue() //展示数值
    {
        //for (int i = 0; i < curSize; i++) //展示序号在前，已复位
        //{
        //    BoxArray[i].transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        for (int i = 0; i < curSize; i++)
        {
            activeRawImage(i, ValueArray[i]);
        }
    }

    public void ChangeValue(int direction) //direction = 1 | -1
    {
        ValueArray[curId] = ValueArray[curId] + direction;
        if (ValueArray[curId] > 4)
        {
            ValueArray[curId] = 1;
        }
        else if (ValueArray[curId] < 1)
        {
            ValueArray[curId] = 4;
        }
    }

    public void StartRotation()
    {
        for (int i = 0; i < curSize; i++)
        {
            activeRawImage(i, 0);
            BoxArray[i].transform.Find("OrderCanvas/OrderText").GetComponent<Text>().text = "";
        }
        NextRotation();
    }

    public void NextRotation() //
    {
        if (++curStep <= rotateStep)
        {
            curId = Random.Range(0, curSize);
            while(ValueArray[curId] < 1)
            {
                curId = Random.Range(0, curSize);
            }

            int direction = Random.Range(0, 2);
            if (direction == 0)
            {
                direction = -1;
            }
            BoxArray[curId].SendMessage("Rotate", direction);
            ChangeValue(direction);
        }
        else
        {
            //curStep = 0; //
            int id = Random.Range(0, curSize);
            object[] message = new object[2];
            message[0] = id;
            message[1] = ValueArray[id];
            GameManager.SendMessage("ChangeState", 2); //此时应正在显示题目，没有什么要点击的
            GameManager.SendMessage("ShowQuestion", message);
        }
    }
}
