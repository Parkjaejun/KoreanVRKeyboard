using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Keyfeedback : MonoBehaviour
{
    public bool keyHit = false;
    public bool keyCanBeHitAgain = false;

    private float originalYPosition;
    public bool isClick;
    public Text button;
    public string buttonName;
    void Start()
    {
        originalYPosition = transform.position.y;
    }


    void Update()
    {

    }

    public void onLookItem(bool isLookAt)
    {
        transform.position += new Vector3(0, -1, 0);
        //UnityEngine.Debug.Log("Key Clicked");
        button = transform.GetComponent<Text>();
        //buttonName = button.text;
        isClick = true;
        //UnityEngine.Debug.Log(buttonName);
    }

    public void OutLookItem(bool isLookAt)
    {
        transform.position += new Vector3(0, 1, 0);
        //UnityEngine.Debug.Log("Key upped");
        isClick = false;
        //UnityEngine.Debug.Log(isClick);
    }


}