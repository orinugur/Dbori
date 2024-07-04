using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : Singleton<DBManager>
{
    [Header("connectionInfo")]
    [SerializeField] public string _ip = "127.0.0.1";
    [SerializeField] public string _dbName = "test";
    [SerializeField] public string _uid = "root";
    [SerializeField] public string _pws = "1234";
    private MySqlConnection _dbConnection;
    private bool _isconnectTestComplete;

    public string message;

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
            message = "저장성공";
            Debug.Log("connectDB 성공");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error: {e}");
            Debug.Log("연결 실패");
            message = "저장실패";
            return false;
        }
    }

    public bool ConnectLogin()
    {
        string connectStr = $"Server={_ip};Database={_dbName};Uid={_uid};Pwd={_pws};";
        Debug.Log(connectStr);
        Debug.Log("connectDB");
        try
        {
            _dbConnection = new MySqlConnection(connectStr);
            _dbConnection.Open();
            Debug.Log("connectDB 성공");
            message = "성공";
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error: {e}");
            Debug.Log("연결 실패");
            message = "실패";
            return false;
        }
    }

    public object ExecuteScalarQuery(string query, Dictionary<string, object> parameters)
    {
        if (ConnectLogin())
        {
            try
            {
                using (_dbConnection = new MySqlConnection($"Server={_ip};Database={_dbName};Uid={_uid};Pwd={_pws};"))
                {
                    _dbConnection.Open();
                    using (MySqlCommand sqlCommand = new MySqlCommand(query, _dbConnection))
                    {
                        foreach (var param in parameters)
                        {
                            sqlCommand.Parameters.AddWithValue(param.Key, param.Value);
                        }
                        object result = sqlCommand.ExecuteScalar();
                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error: {e}");
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}