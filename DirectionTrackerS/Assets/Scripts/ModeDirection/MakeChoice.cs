using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeChoice : MonoBehaviour
{
    public GameObject AnswerCanvas;

    public int id;

    public void OnClick() //Remember to set it public
    {
        AnswerCanvas.SendMessage("GetAnswer", id);
    }
}
