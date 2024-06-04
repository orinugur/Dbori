using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Runtime.InteropServices;
using static UnityEditor.ShaderData;
using System.Collections.Generic;

public class AccountUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] InputField Input_Id;
    [SerializeField] InputField Input_Passward;
    [SerializeField] internal Text Text_DBResult;
    [SerializeField] internal Text Text_Log;


    [Header("connectionInfo")]
    [SerializeField] string _ip = "127.0.0.1";
    [SerializeField] string _dbName = "test";
    [SerializeField] string _uid = "root";
    [SerializeField] string _pws = "1234";

    List<string> DB_UserName;

    private bool _isconnectTestComplete;

    private static MySqlConnection _dbConnection;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    private void SendQuery(string querystr, string tableName)
    {
        if (querystr.Contains("SELECT")) //있으면 Select 관련 함수 호출 
        {
            DataSet dataSet = OnSelectRequest(querystr, tableName);
            Text_DBResult.text = DeformatResult(dataSet);
            Text_DBResult.gameObject.SetActive(true);
        }
        else //없다면 Insert 또는 Update 관련 커리
        {
            Text_DBResult.gameObject.SetActive(true);
            Text_DBResult.text = OnInsertOnUpdateRequest(querystr) ? "성공" : "실패";
        }
        //return dataSet.GetXml().ToString();

    }

    public bool OnInsertOnUpdateRequest(string query)
    {
        try
        {
            MySqlCommand sqlCommand = new MySqlCommand();
            sqlCommand.Connection = _dbConnection;
            sqlCommand.CommandText = query;
            _dbConnection.Open();
            sqlCommand.ExecuteNonQuery();
            _dbConnection.Close();
            return true;
        }
        catch (Exception e)
        {
            string Log = e.ToString();
            if (Log.Contains("Duplicate"))
            {
                Debug.Log("중복");
                //Text_DBResult.text = "중복된 ID";
                //Text_DBResultOb.SetActive(true);
            }
            _dbConnection.Close();
            return false;
        }
    }

    private string DeformatResult(DataSet dataSet)
    {
        string resultStr = string.Empty;
        foreach (DataTable table in dataSet.Tables)
        {
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {
                    resultStr += $"{column.ColumnName} : {row[column]}\n";
                }
            }
        }
        return resultStr;
    }
    public static DataSet OnSelectRequest(string query, string tableName)
    {
        try
        {
            _dbConnection.Open();
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = _dbConnection;
            sqlCmd.CommandText = query;

            MySqlDataAdapter sd = new MySqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            sd.Fill(dataSet, tableName);

            _dbConnection.Close();
            return dataSet;
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            return null;
        }
    }
    public bool ConnectDB() //데이터베이스 연결
    {
        string connectStr = $"Server={_ip};Database={_dbName};Uid={_uid};Pwd={_pws};";
        Debug.Log(connectStr);
        Debug.Log("connectDB");
        try
        {
            using (MySqlConnection conn = new MySqlConnection(connectStr))
            {
                _dbConnection = conn;
                conn.Open();
            }
            
            Text_Log.text = "DB 연결을 성공했습니다.";
            
            gameObject.SetActive(true);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error : {e.ToString()}");
            Text_Log.text = "서버연결 실패.";
            return false;
        }
    }

    public void OnClick_TestDBConnect()
    {
        _isconnectTestComplete = ConnectDB();
    }

    public void OnSubmit_SendQuery()
    {
        if (_isconnectTestComplete == false)
        {
            Text_Log.text = "DB 연결을 먼저 시도해주세요.";

            return;
        }
        Text_Log.text = string.Empty;
        if (Input_Id.text != string.Empty && Input_Passward.text != string.Empty) //값이 비지 않았을시에 등록을 실행
        {


            string query = "SELECT COUNT(*) FROM user_info WHERE U_Name = @UserName";

                SendQuery(query, "user_info");





                string query2 = string.IsNullOrWhiteSpace($"INSERT INTO user_info(U_Name, U_Pass) VALUES('{Input_Id.text}', '{Input_Passward.text}')")
                ? "SELECT U_Name, U_Pass FROM user_info" : $"INSERT INTO user_info(U_Name, U_Pass) VALUES('{Input_Id.text}', '{Input_Passward.text}')";
            SendQuery(query2, "user_info");
            Debug.Log(query);
        }




    }

    public void OnClick_OpenDatabaseUI()
    {
        this.gameObject.SetActive(true);
    }

    public void OnClick_CloseUI()
    {
        this.gameObject.SetActive(false);
    }

}
