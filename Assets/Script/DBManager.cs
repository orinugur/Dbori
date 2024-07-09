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
            message = "���强��";
            Debug.Log("connectDB ����");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error: {e}");
            Debug.Log("���� ����");
            message = "�������";
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
            Debug.Log("connectDB ����");
            message = "����";
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error: {e}");
            Debug.Log("���� ����");
            message = "����";
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
        // U_Num�� �ش��ϴ� U_Money ���� ��ȸ�ϴ� ����
        string selectQuery = "SELECT U_Money FROM user_info WHERE U_Num = @U_Num";
        var selectParams = new Dictionary<string, object> { { "@U_Num", U_Num } };

        // ExecuteScalarQuery�� ���� U_Money ���� ��ȸ
        object result = ExecuteScalarQuery(selectQuery, selectParams);

        // ��ȸ�� ���� null�� �ƴ� ���
        if (result != null)
        {
            // object Ÿ���� result�� int Ÿ������ ��ȯ
            currentMoney = Convert.ToInt32(result);
            Debug.Log($"Current U_Money: {currentMoney}");

            // ���� U_Money ���� Price�� ����
            newMoney = currentMoney + Price;
            Debug.Log($"New U_Money: {newMoney}");

            // U_Money ���� ������Ʈ�ϴ� ����
            string updateQuery = "UPDATE user_info SET U_Money = @NewMoney WHERE U_Num = @U_Num";
            var updateParams = new Dictionary<string, object>
            {
             { "@NewMoney", newMoney },
             { "@U_Num", U_Num }
            };

            // ExecuteNonQuery�� ���� ������Ʈ ������ ����
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
