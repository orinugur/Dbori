using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBox : MonoBehaviour
{
    public int Price;

    void Start()
    {
        Price = 0;
    }

    public void InsertBox(int price)
    {
        Price += price;

    }

    public void ExitSuccess()
    {
        GameManager.Instance.price = Price;

    }
    public void ExitFail()
    {
        GameManager.Instance.price = 0;

    }
}
