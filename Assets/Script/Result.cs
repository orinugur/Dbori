using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Result : MonoBehaviour
{

    string qurry;
    public int Price;
    public int U_Num;
    public TextMeshProUGUI resultTextName;
    public TextMeshProUGUI resultTextPrice;
    StringBuilder Namesb = new StringBuilder();
    StringBuilder Pricesb = new StringBuilder();
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;  // 커서 잠금

        U_Num = GameManager.Instance.U_Num;
        Price = GameManager.Instance.price;
        DBManager.Instance.UpdateMoney(U_Num, Price);
        Namesb.Clear();
        Pricesb.Clear();
        Namesb.AppendLine(("Item"));
        Pricesb.AppendLine(("Price"));
        resultTextName.text = Namesb.ToString();
        resultTextPrice.text = Pricesb.ToString();
        StartCoroutine(ResultText());
    }
    void ResultLand()
    {
        //DBManager.Instance.UpdateMoney(qurry, Price, U_Num);
    }

    IEnumerator ResultText()
    {


        foreach (var item in GameManager.Instance.names)
        {
            Debug.Log(item);
            Namesb.AppendLine(item);
            resultTextName.text = Namesb.ToString();
            yield return new WaitForSecondsRealtime(0.5f);

        }
        foreach (var itemprie in GameManager.Instance.itemPrice)
        {
            Debug.Log(itemprie);
            Pricesb.AppendLine(itemprie.ToString());
            resultTextPrice.text = Pricesb.ToString();
            yield return new WaitForSecondsRealtime(0.15f);
        }
        Namesb.AppendLine(DBManager.Instance.currentMoney.ToString() + " + " + Price.ToString() + " = ");
        resultTextName.text=Namesb.ToString();
        Pricesb.Append(DBManager.Instance.newMoney.ToString());
        resultTextPrice.text=Pricesb.ToString();
        GameManager.Instance.names.Clear();
        GameManager.Instance.itemPrice.Clear();
        GameManager.Instance.items.Clear();
    }


}
