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
    public int currentMoney;
    public int newMoney;

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
        if (!ConnectLogin())
            return null;

        try
        {
            using (_dbConnection = new MySqlConnection(GetConnectionString()))
            {
                _dbConnection.Open();
                using (MySqlCommand sqlCommand = new MySqlCommand(query, _dbConnection))
                {
                    foreach (var param in parameters)
                    {
                        sqlCommand.Parameters.AddWithValue(param.Key, param.Value);
                    }
                    return sqlCommand.ExecuteScalar();
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error: {e}");
            return null;
        }
        finally
        {
            CloseConnection();
        }
    }

    public int ExecuteNonQuery(string query, Dictionary<string, object> parameters)
    {
        if (!ConnectLogin())
            return 0;

        try
        {
            using (_dbConnection = new MySqlConnection(GetConnectionString()))
            {
                _dbConnection.Open();
                using (MySqlCommand sqlCommand = new MySqlCommand(query, _dbConnection))
                {
                    foreach (var param in parameters)
                    {
                        sqlCommand.Parameters.AddWithValue(param.Key, param.Value);
                    }
                    return sqlCommand.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error: {e}");
            return 0;
        }
        finally
        {
            CloseConnection();
        }
    }

    private string GetConnectionString()
    {
        return $"Server={_ip};Database={_dbName};Uid={_uid};Pwd={_pws};";
    }

    private void CloseConnection()
    {
        if (_dbConnection != null)
        {
            _dbConnection.Close();
            _dbConnection = null;
            Debug.Log("Database connection closed successfully.");
        }
    }

    public void UpdateMoney(int U_Num, int Price)
    {
        // U_Num에 해당하는 U_Money 값을 조회하는 쿼리
        string selectQuery = "SELECT U_Money FROM user_info WHERE U_Num = @U_Num";
        var selectParams = new Dictionary<string, object> { { "@U_Num", U_Num } };

        // ExecuteScalarQuery를 통해 U_Money 값을 조회
        object result = ExecuteScalarQuery(selectQuery, selectParams);

        // 조회된 값이 null이 아닌 경우
        if (result != null)
        {
            // object 타입의 result를 int 타입으로 변환
            currentMoney = Convert.ToInt32(result);
            Debug.Log($"Current U_Money: {currentMoney}");

            // 현재 U_Money 값에 Price를 더함
            newMoney = currentMoney + Price;
            Debug.Log($"New U_Money: {newMoney}");

            // U_Money 값을 업데이트하는 쿼리
            string updateQuery = "UPDATE user_info SET U_Money = @NewMoney WHERE U_Num = @U_Num";
            var updateParams = new Dictionary<string, object>
            {
             { "@NewMoney", newMoney },
             { "@U_Num", U_Num }
            };

            // ExecuteNonQuery를 통해 업데이트 쿼리를 실행
            int rowsAffected = ExecuteNonQuery(updateQuery, updateParams);
            if (rowsAffected > 0)
            {
                Debug.Log("U_Money updated successfully.");
            }
            else
            {
                Debug.Log("U_Money update failed.");
            }
        }
        else
        {
            Debug.Log("No record found for U_Num: " + U_Num);
        }
    }
}
