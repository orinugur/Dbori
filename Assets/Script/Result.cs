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
        //��ŸƮ�� ���ӸŴ����κ��� U_Num�� ������
    }
    void ResultLand()
    {
        //DBManager.Instance.UpdateMoney(qurry, Price, U_Num);
    }
}
