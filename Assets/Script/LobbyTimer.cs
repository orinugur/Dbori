using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyTimer : MonoBehaviour
{
    public TextMeshProUGUI DayTxt;
    public TextMeshProUGUI TimeTxT;


    void Update()
    {
        GetCurrentDate();
    }
    public void GetCurrentDate()
    {
        //string MonthAndDay = DateTime.Now.ToString(("MM�� dd��"));
        //DayTxt.text = "��¥ : " + MonthAndDay;
        //public DateTime(int year, int month, int day, int hour, int minute, int second);
        string DayTime = DateTime.Now.ToString(("yyyy-MM-dd HH:mm:ss"));
        //string DayTime = DateTime.Now.ToString("t");
        TimeTxT.text = DayTime;
    }
}
