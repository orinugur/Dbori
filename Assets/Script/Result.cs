using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{

    string qurry;
    public int Price;
    public int U_Num;

    public void Start()
    {
        U_Num = GameManager.Instance.U_Num;
        //스타트시 게임매니저로부터 U_Num을 가져옴
    }
    void ResultLand()
    {
        //DBManager.Instance.UpdateMoney(qurry, Price, U_Num);
    }
}
