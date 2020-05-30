using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public bool showing;

    public static Dialog instance;

    public GameObject dialogObj;
    public Text dialogText;


    float timerPressed;

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (showing && Input.GetKeyDown("e") && timerPressed < Time.time)
        {
            StopShow();
        }
    }

    public void Show(string msg)
    {
        timerPressed = Time.time + 0.1f;
        showing = true;
        dialogObj.SetActive(true);
        dialogText.text = msg;
    }

    public void StopShow()
    {
        showing = false;
        dialogObj.SetActive(false);
        dialogText.text = "";
    }
}
