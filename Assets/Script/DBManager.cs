using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : Singleton<DBManager>
{
    [Header("connectionInfo")]
    [SerializeField] public string _ip = "127.0.0.1";
    [SerializeField] public string _dbName = "test";
    [SerializeField] public string _uid = "root";
    [SerializeField] public string _pws = "1234";
    private static MySqlConnection _dbConnection;
    public string mesege;

    public bool ConnectDB()
    {
        string connectStr = $"Server={_ip};Database={_dbName};Uid={_uid};Pwd={_pws};";
        Debug.Log(connectStr);
        Debug.Log("connectDB");
        try
        {
            _dbConnection = new MySqlConnection(connectStr);
            _dbConnection.Open();
            gameObject.SetActive(true);
            _dbConnection.Close();
            mesege = "저장성공";
            Debug.Log("connectDBsusekse");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error : {e.ToString()}");
            Debug.Log("fall");
            mesege = "저장실패";
            return false;
        }

    }

}
