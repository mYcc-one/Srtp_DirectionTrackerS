using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float timer; //旋转间隔
    private int counter; //计算旋转次数
    private bool isRotating;
    private int direction; //-1 or 1

    public GameObject BoxManager;

    public void Rotate(int Direction)
    {
        direction = Direction;
        isRotating = true;
    }

    void Start()
    {
        timer = 0.35f;
        counter = 0;
        isRotating = false;
    }

    void Update()
    {
        if(!isRotating) { return; }
        if(counter < 15) //另：counter-9，rotate-10
        {
            transform.Rotate(0, 6 * direction, 0);
            counter++;
        }
        else
        {
            timer -= Time.deltaTime;
            while (timer <= 0)
            {
                timer = 0.35f;
                counter = 0;
                isRotating = false;
                BoxManager.SendMessage("NextRotation"); //是否改为由changeValue函数来调用该函数？
            }
        }
    }
}
