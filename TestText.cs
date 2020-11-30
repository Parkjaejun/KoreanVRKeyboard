using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TestText : MonoBehaviour
{
    //public GameObject targetObject;
    public Text sentence;
    public string tempText = null;
    public float timer = 0;
    public int[] hanguel = new int[4] { 99, 99, 99, 99 };
    public int[] tempHanguel = new int[4];

    public static string chosung = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
    public static string jungsung = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
    public static string jongsung = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";

    public ushort UnicodeHanguel = 0xAC00;
    public int code;
    public char hanguelOutput;

    public char temp;
    public int tempint;
    public bool shift = true;
    void Start()
    {
        tempText = null;

        for (int i = 0; i < 4; i++)
        {
            hanguel[i] = 99;
            tempHanguel[i] = 99;
        }
    }

    int showTime;
    public bool writeKoreanMode = true;

    void Update()
    {
        sentence.text = tempText;          // 입력한 문장이 저장되는 변수.

        timer += Time.deltaTime;
        showTime = (int)timer;
        //UnityEngine.Debug.Log(showTime);
       
        if(showTime > 2)
        {
            clickKeyA();            clickKeyB();            clickKeyC();            clickKeyD();            clickKeyE();
            clickKeyF();            clickKeyG();            clickKeyH();            clickKeyI();            clickKeyJ();
            clickKeyK();            clickKeyL();            clickKeyM();            clickKeyN();            clickKeyO();
            clickKeyP();            clickKeyQ();            clickKeyR();            clickKeyS();            clickKeyT();
            clickKeyU();            clickKeyV();            clickKeyW();            clickKeyX();            clickKeyY();
            clickKeyZ();            

            clickReset();           clickErase();           clickChange();          clickSpace();           clickShift();
        }
           
    }

    void seeHanguel()
    {
        for(int i=0; i<4; i++)
        {
            UnityEngine.Debug.Log(hanguel[i]);
        }
    }

    void resetArray()
    {
        for (int i = 0; i < 4; i++)
        {
            hanguel[i] = 99;
        }
    }

    void seeBackupHanguel()
    {
        for(int i=0; i<4; i++)
        {
            UnityEngine.Debug.Log(tempHanguel[i]);
        }
    }

    void resetBackup()
    {
        for (int i = 0; i < 4; i++)
        {
            tempHanguel[i] = 99;
        }

    }

    void makingHanguel(char a) // 한글을 조합하는 메서드
    {
        /////////////////초성 입력
        if (hanguel[0] == 99)   // 초성이 입력되지 않았을 때
        {
            if(chosung.IndexOf(a) != -1)  // 초성을 입력한 경우
            {
                hanguel[0] = chosung.IndexOf(a);    // 초성에 a를 입력
                tempHanguel[0] = chosung.IndexOf(a);

            }

            else if(jungsung.IndexOf(a) != -1)      // 버퍼가 비었는데 처음부터 모음을 입력할 시 직전 단어의 종성을 초성으로 사용한다고 간주.
            {
                if(tempHanguel[2] != 99 && tempText != null && tempHanguel[3] == 99)          // 종성이 있었을 경우 그리고 겹받침이 아닌 경우
                {
                    tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 두글자를 제거. 직전의 단어 수정을 위해.
                    code = UnicodeHanguel + (tempHanguel[0] * 21 + tempHanguel[1]) * 28;        // 초,중성을 조합한다.
                    hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                    tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다. 직전의 단어에서 종성을 빼고 출력한다.

                    tempint = tempHanguel[2];                                                   //기존의 종성이 뭐였는지 파악하기 위해 인덱스를 따고
                    temp = jongsung[tempint];                                                   //종성의 인덱스로 검색해서 종성이 뭐였는지 파악하고
                    hanguel[0] = chosung.IndexOf(temp);                                         //해당 종성이 초성에서 어떤 인덱스인지 검색해서 한글 배열에 넣는다.

                    hanguel[1] = jungsung.IndexOf(a);                                           //한글 배열에 중성 추가
                    code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28;                // 초,중성을 조합한다.
                    hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                    tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.

                    resetBackup();
                    seeBackupHanguel();

                    Array.Copy(hanguel, 0, tempHanguel, 0, 1);                                  //hanguel의 0번째를 tempHanguel의 0번째로 복사
                    Array.Copy(hanguel, 1, tempHanguel, 1, 1);                                  //hanguel의 1번째를 tempHanguel의 1번째로 복사
                }
            }
        }

        ////////////////////중성 입력
        else if(hanguel[0] != 99) // 초성이 입력되었을 때
        {
            if(hanguel[1] == 99)  //중성이 입력되지 않았을 때
            {
                if (jungsung.IndexOf(a) != -1)   //입력된 a가 중성이라면
                {
                    tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 두글자를 제거한다. 초성과 중성을 제거하고 문자를 출력.
                    hanguel[1] = jungsung.IndexOf(a);                                           //한글 배열에 중성 추가
                    tempHanguel[1] = jungsung.IndexOf(a);

                    code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28             ;   // 초,중성을 조합한다.
                    hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                    tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                }
                else                            // 자음을 한번 더 입력했을 때 
                {
                    if(chosung.IndexOf(a) == 0)
                    {
                        hanguel[0] = 1;
                        tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 두글자를 제거한다. 초성과 중성을 제거하고 문자를 출력.
                        tempText += "ㄲ";
                    }

                    else if(chosung.IndexOf(a) == 3)
                    {
                        hanguel[0] = 4;
                        tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 두글자를 제거한다. 초성과 중성을 제거하고 문자를 출력.
                        tempText += "ㄸ";
                    }

                    else if (chosung.IndexOf(a) == 7)
                    {
                        hanguel[0] = 8;
                        tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 두글자를 제거한다. 초성과 중성을 제거하고 문자를 출력.
                        tempText += "ㅃ";
                    }

                    else if (chosung.IndexOf(a) == 9)
                    {
                        hanguel[0] = 10;
                        tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 두글자를 제거한다. 초성과 중성을 제거하고 문자를 출력.
                        tempText += "ㅆ";
                    }

                    else if (chosung.IndexOf(a) == 12)
                    {
                        hanguel[0] = 13;
                        tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 두글자를 제거한다. 초성과 중성을 제거하고 문자를 출력.
                        tempText += "ㅉ";
                    }

                    else
                    {
                        hanguel[0] = chosung.IndexOf(a);    // 초성에 a를 입력  -> 초성을 기존의 것에서 a로 변환
                        tempHanguel[0] = chosung.IndexOf(a);
                    }

                }
            }

            /////////////////종성 입력
            else if(hanguel[1] != 99)         //중성이 입력되어 있다면
            {
                if(hanguel[2] == 99)          //종성이 입력되지 않았을 때
                {
                    if(jongsung.IndexOf(a) != -1) //입력된 a가 종성이라면
                    {
                        hanguel[2] = jongsung.IndexOf(a);                                           // 한글 배열에 종성 추가
                        tempHanguel[2] = jongsung.IndexOf(a);
                        tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                        code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                        hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                        tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                    }

                    else                        // 다시 모음을 입력했을 때는 그냥 기존의 입력중이던 값 종결시키고 버퍼를 비운다.
                    {
                        resetArray();                                                        // 버퍼를 비운다.
                    }
                }

                //////////////겹받침 입력
                else if(hanguel[2] != 99)       //종성이 입력되어있을 때 겹받침을 원한다면
                {
                    if (jongsung.IndexOf(a) != -1) // 입력된 a가 종성이라면                    
                    {
                        if(hanguel[2] == 1)         //종성이 ㄱ이였다면
                        {
                            if (jongsung.IndexOf(a) == 1)   // 뒤에 ㄱ이 온다면 ㄲ으로 변환
                            {
                                hanguel[3] = 2;
                                hanguel[2] = 2;
                                tempHanguel[3] = 2;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                                                               // 버퍼를 비운다.
                            }
                            else if (jongsung.IndexOf(a) == 19)     // 뒤에 ㅅ이 온다면 ㄳ으로 변환
                            {
                                hanguel[3] = 3;
                                hanguel[2] = 3;
                                tempHanguel[3] = 3;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                                                               // 버퍼를 비운다.
                            }
                            else
                            {
                                resetArray();                                                        // 버퍼를 비운다.
                            }
                        }       //종성이 ㄱ이였다면

                        else if(hanguel[2] == 4)            //종성이 ㄴ이였다면
                        {
                            if(jongsung.IndexOf(a) == 22)   //뒤에 ㅈ이 온다면 ㄵ으로 변환
                            {
                                hanguel[3] = 5;
                                hanguel[2] = 5;
                                tempHanguel[3] = 5;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                                                               // 버퍼를 비운다.
                            }
                            else if(jongsung.IndexOf(a)== 27)   // 뒤에 ㅎ이 온다면 ㄶ으로 변환
                            {
                                hanguel[3] = 6;
                                hanguel[2] = 6;
                                tempHanguel[3] = 6;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                                                               // 버퍼를 비운다.
                            }
                            else
                            {
                                resetArray();
                            }
                        }  //종성이 ㄴ이였다면

                        else if(hanguel[2] == 8)        //종성이 ㄹ이였다면
                        {
                            if(jongsung.IndexOf(a) == 1)    //뒤에 ㄱ이 온다면 ㄺ으로 변환
                            {
                                hanguel[3] = 9;
                                hanguel[2] = 9;
                                tempHanguel[3] = 9;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                                                               // 버퍼를 비운다.
                            }

                            else if (jongsung.IndexOf(a) == 16)  // 뒤에 ㅁ이  온다면 ㄻ으로 변환
                            {
                                hanguel[3] = 10;
                                hanguel[2] = 10;
                                tempHanguel[3] = 10;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                                                               // 버퍼를 비운다.
                            }

                            else if (jongsung.IndexOf(a) == 17)  // 뒤에 ㅂ이  온다면 ㄼ으로 변환
                            {
                                hanguel[3] = 11;
                                hanguel[2] = 11;
                                tempHanguel[3] = 11;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                                                               // 버퍼를 비운다.
                            }

                            else if (jongsung.IndexOf(a) == 19)  // 뒤에 ㅅ이  온다면 ㄽ으로 변환
                            {
                                hanguel[3] = 12;
                                hanguel[2] = 12;
                                tempHanguel[3] = 12;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                                                               // 버퍼를 비운다.
                            }

                            else if (jongsung.IndexOf(a) == 25)  // 뒤에 ㅌ이  온다면 ㄾ으로 변환
                            {
                                hanguel[3] = 13;
                                hanguel[2] = 13;
                                tempHanguel[3] = 13;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                                                               // 버퍼를 비운다.
                            }

                            else if (jongsung.IndexOf(a) == 26)  // 뒤에 ㅍ이  온다면 ㄿ으로 변환
                            {
                                hanguel[3] = 14;
                                hanguel[2] = 14;
                                tempHanguel[3] = 14;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                                                               // 버퍼를 비운다.
                            }

                            else if (jongsung.IndexOf(a) == 27)  // 뒤에 ㅎ이  온다면 ㅀ으로 변환
                            {
                                hanguel[3] = 15;
                                hanguel[2] = 15;
                                tempHanguel[3] = 15;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                                                               // 버퍼를 비운다.
                            }

                            else
                            {
                                resetArray();
                            }
                        }  //종성이 ㄹ이였다면

                        else if(hanguel[2] == 17)       //종성이 ㅂ이였던 경우
                        {
                            if(jongsung.IndexOf(a) == 19)       // 뒤에 ㅅ이 오면 ㅄ으로 바꾼다
                            {
                                hanguel[3] = 18;
                                hanguel[2] = 18;
                                tempHanguel[3] = 18;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                                                               // 버퍼를 비운다.

                            }

                            else
                            {
                                resetArray();
                            }
                        } //종성이 ㅂ이였다면

                        else if(hanguel[2] == 19)       //종성이 ㅅ이였을 경우
                        {
                            if(jongsung.IndexOf(a) == 19)       //ㅅ이 올 경우 ㅆ으로 변환한다.
                            {
                                hanguel[3] = 20;
                                hanguel[2] = 20;
                                tempHanguel[3] = 20;
                                tempText = tempText.Remove(tempText.Length - 2);                            // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                                code = UnicodeHanguel + (hanguel[0] * 21 + hanguel[1]) * 28 + hanguel[2];   // 초중종성을 조합한다.
                                hanguelOutput = Convert.ToChar(code);                                       // 조합한 유니코드를 문자로 변환한다.
                                tempText += hanguelOutput;                                                  // 조합된 한글을 출력한다.
                                resetArray();                          
                            }

                            else
                            {
                                resetArray();
                            }
                        } //종성이 ㅅ이였다면
                    }
                }
            }
        }
    }


    ///A
    void clickKeyA()
    {
        if (GameObject.Find("key_A").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if(shift == true)
                {
                    UnityEngine.Debug.Log("key A is clicked");
                    tempText += "A";
                    timer = 0;
                }
                else
                {
                    tempText += "a";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅁ is clicked");
                tempText += "ㅁ";
                makingHanguel('ㅁ');
                timer = 0;
            }
        }
    }

    //B
    void clickKeyB()
    {
        if (GameObject.Find("key_B").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key B is clicked");
                    tempText += "B";
                    timer = 0;
                }
                else
                {
                    tempText += "b";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅠ is clicked");
                tempText += "ㅠ";
                makingHanguel('ㅠ');
                timer = 0;
            }
        }

    }

    //C
    void clickKeyC()
    {
        if (GameObject.Find("key_C").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key C is clicked");
                    tempText += "C";
                    timer = 0;
                }
                else
                {
                    tempText += "c";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅊ is clicked");
                tempText += "ㅊ";
                makingHanguel('ㅊ');
                timer = 0;
            }
        }

    }

    //D
    void clickKeyD()
    {
        if (GameObject.Find("key_D").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key D is clicked");
                    tempText += "D";
                    timer = 0;
                }
                else
                {
                    tempText += "d";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅇ is clicked");
                tempText += "ㅇ";
                makingHanguel('ㅇ');
                timer = 0;
            }
        }

    }

    //E
    void clickKeyE()
    {
        if (GameObject.Find("key_E").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key E is clicked");
                    tempText += "E";
                    timer = 0;
                }
                else
                {
                    tempText += "e";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㄷ is clicked");
                tempText += "ㄷ";
                makingHanguel('ㄷ');
                timer = 0;
            }
        }

    }

    //F
    void clickKeyF()
    {
        if (GameObject.Find("key_F").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key F is clicked");
                    tempText += "F";
                    timer = 0;
                }
                else
                {
                    tempText += "f";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㄹ is clicked");
                tempText += "ㄹ";
                makingHanguel('ㄹ');
                timer = 0;
            }
        }

    }

    //G
    void clickKeyG()
    {
        if (GameObject.Find("key_G").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key G is clicked");
                    tempText += "G";
                    timer = 0;
                }
                else
                {
                    tempText += "g";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅎ is clicked");
                tempText += "ㅎ";
                makingHanguel('ㅎ');
                timer = 0;
            }
        }

    }

    //H
    void clickKeyH()
    {
        if (GameObject.Find("key_H").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key H is clicked");
                    tempText += "H";
                    timer = 0;
                }
                else
                {
                    tempText += "h";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅗ is clicked");
                tempText += "ㅗ";
                makingHanguel('ㅗ');
                timer = 0;
            }
        }

    }

    //I
    void clickKeyI()
    {
        if (GameObject.Find("key_I").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key I is clicked");
                    tempText += "I";
                    timer = 0;
                }
                else
                {
                    tempText += "i";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅑ is clicked");
                tempText += "ㅑ";
                makingHanguel('ㅑ');
                timer = 0;
            }
        }

    }

    //J
    void clickKeyJ()
    {
        if (GameObject.Find("key_J").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key J is clicked");
                    tempText += "J";
                    timer = 0;
                }
                else
                {
                    tempText += "j";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅓ is clicked");
                tempText += "ㅓ";
                makingHanguel('ㅓ');
                timer = 0;
            }
        }

    }

    //K
    void clickKeyK()
    {
        if (GameObject.Find("key_K").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key K is clicked");
                    tempText += "K";
                    timer = 0;
                }
                else
                {
                    tempText += "k";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅏ is clicked");
                tempText += "ㅏ";
                makingHanguel('ㅏ');
                timer = 0;
            }
        }

    }

    //L
    void clickKeyL()
    {
        if (GameObject.Find("key_L").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key L is clicked");
                    tempText += "L";
                    timer = 0;
                }
                else
                {
                    tempText += "l";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅣ is clicked");
                tempText += "ㅣ";
                makingHanguel('ㅣ');
                timer = 0;
            }
        }

    }

    //M
    void clickKeyM()
    {
        if (GameObject.Find("key_M").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key M is clicked");
                    tempText += "M";
                    timer = 0;
                }
                else
                {
                    tempText += "m";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅡ is clicked");
                tempText += "ㅡ";
                makingHanguel('ㅡ');
                timer = 0;
            }
        }

    }

    //N
    void clickKeyN()
    {
        if (GameObject.Find("key_N").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key N is clicked");
                    tempText += "N";
                    timer = 0;
                }
                else
                {
                    tempText += "n";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅜ is clicked");
                tempText += "ㅜ";
                makingHanguel('ㅜ');
                timer = 0;
            }
        }

    }

    //O
    void clickKeyO()
    {
        if (GameObject.Find("key_O").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key O is clicked");
                    tempText += "O";
                    timer = 0;
                }
                else
                {
                    tempText += "o";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅐ is clicked");
                tempText += "ㅐ";
                makingHanguel('ㅐ');
                timer = 0;
            }
        }

    }

    //P
    void clickKeyP()
    {
        if (GameObject.Find("key_P").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key P is clicked");
                    tempText += "P";
                    timer = 0;
                }
                else
                {
                    tempText += "p";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅔ is clicked");
                tempText += "ㅔ";
                makingHanguel('ㅔ');
                timer = 0;
            }
        }

    }

    //Q
    void clickKeyQ()
    {
        if (GameObject.Find("key_Q").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key Q is clicked");
                    tempText += "Q";
                    timer = 0;
                }
                else
                {
                    tempText += "q";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅂ is clicked");
                tempText += "ㅂ";
                makingHanguel('ㅂ');
                timer = 0;
            }
        }

    }

    //R
    void clickKeyR()
    {
        if (GameObject.Find("key_R").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key R is clicked");
                    tempText += "R";
                    timer = 0;
                }
                else
                {
                    tempText += "r";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㄱ is clicked");
                tempText += "ㄱ";
                makingHanguel('ㄱ');
                timer = 0;
            }
        }

    }

    //S
    void clickKeyS()
    {
        if (GameObject.Find("key_S").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key S is clicked");
                    tempText += "S";
                    timer = 0;
                }
                else
                {
                    tempText += "s";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㄴ is clicked");
                tempText += "ㄴ";
                makingHanguel('ㄴ');
                timer = 0;
            }

        }

    }

    //T
    void clickKeyT()
    {
        if (GameObject.Find("key_T").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key T is clicked");
                    tempText += "T";
                    timer = 0;
                }
                else
                {
                    tempText += "t";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅅ is clicked");
                tempText += "ㅅ";
                makingHanguel('ㅅ');
                timer = 0;
            }

        }

    }

    //U
    void clickKeyU()
    {
        if (GameObject.Find("key_U").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key U is clicked");
                    tempText += "U";
                    timer = 0;
                }
                else
                {
                    tempText += "u";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅕ is clicked");
                tempText += "ㅕ";
                makingHanguel('ㅕ');
                timer = 0;
            }

        }

    }

    //V
    void clickKeyV()
    {
        if (GameObject.Find("key_V").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key V is clicked");
                    tempText += "V";
                    timer = 0;
                }
                else
                {
                    tempText += "v";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅍ is clicked");
                tempText += "ㅍ";
                makingHanguel('ㅍ');
                timer = 0;
            }

        }

    }

    //W
    void clickKeyW()
    {
        if (GameObject.Find("key_W").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key W is clicked");
                    tempText += "W";
                    timer = 0;
                }
                else
                {
                    tempText += "w";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅈ is clicked");
                tempText += "ㅈ";
                makingHanguel('ㅈ');
                timer = 0;
            }

        }

    }

    //X
    void clickKeyX()
    {
        if (GameObject.Find("key_X").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key X is clicked");
                    tempText += "X";
                    timer = 0;
                }
                else
                {
                    tempText += "x";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅌ is clicked");
                tempText += "ㅌ";
                makingHanguel('ㅈ');
                timer = 0;
            }

        }

    }

    //Y
    void clickKeyY()
    {
        if (GameObject.Find("key_Y").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key Y is clicked");
                    tempText += "Y";
                    timer = 0;
                }
                else
                {
                    tempText += "y";
                    timer = 0;
                }
            }
            else if (writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅛ is clicked");
                tempText += "ㅛ";
                makingHanguel('ㅛ');
                timer = 0;
            }

        }

    }

    //Z
    void clickKeyZ()
    {
        if (GameObject.Find("key_Z").GetComponent<Keyfeedback>().isClick == true)
        {
            if (writeKoreanMode == false)
            {
                if (shift == true)
                {
                    UnityEngine.Debug.Log("key Z is clicked");
                    tempText += "Z";
                    timer = 0;
                }
                else
                {
                    tempText += "z";
                    timer = 0;
                }
            }
            else if(writeKoreanMode == true)
            {
                UnityEngine.Debug.Log("key ㅋ is clicked");
                tempText += "ㅋ";
                makingHanguel('ㅋ');
                timer = 0;
            }
        }
    }

    //Erase
    void clickErase()
    {
        if (GameObject.Find("key_Erase").GetComponent<Keyfeedback>().isClick == true)
        {
            if(tempText != null)
            {
                tempText = tempText.Remove(tempText.Length - 1); // 기존의 문장에서 제일 뒤의 한글자를 제거한다.
                timer = 0;
            }
            else
            {

            }
        }
    }

    //Change
    void clickChange()
    {
        if (GameObject.Find("key_Change").GetComponent<Keyfeedback>().isClick == true)
        {
            if(writeKoreanMode == true)
            {
                writeKoreanMode = false;   // writeKoreanMode의 참거짓을 바꾼다.
                UnityEngine.Debug.Log("Input English is" + writeKoreanMode);
                resetArray();
                timer = 0;
            }
            else if(writeKoreanMode == false)
            {
                writeKoreanMode = true;   // writeKoreanMode의 참거짓을 바꾼다.
                UnityEngine.Debug.Log("Input English is" + writeKoreanMode);
                timer = 0;
            }
        }
    }

    //Reset 
    void clickReset()
    {
        if (GameObject.Find("key_Reset").GetComponent<Keyfeedback>().isClick == true)
        {
            tempText = null;                // 문자열 버퍼를 삭제한다.
            timer = 0;
        }
    }

    //Space
    void clickSpace()
    {
        if (GameObject.Find("key_Space").GetComponent<Keyfeedback>().isClick == true)
        {
            tempText += "  ";
            resetArray();
            timer = 0;
        }
    }

    //Shift
    void clickShift()
    {
        if (GameObject.Find("key_Shift").GetComponent<Keyfeedback>().isClick == true)
        {
            if(shift == true)
            {
                UnityEngine.Debug.Log("Shift is clicked");
                shift = false;
                timer = 0;
            }
            else if(shift == false)
            {
                UnityEngine.Debug.Log("Shift is clicked");
                shift = true;
                timer = 0;
            }
        }
    }
}
