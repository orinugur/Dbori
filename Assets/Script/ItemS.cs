using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ItemS : MonoBehaviour
{
    public Sprite Image;
    public string DataClassName;
    public int Price;

    public void Start()
    {
        getPrice(); 
    }
    public void getPrice()
    {
        DataClassName = Image.name;
        Debug.Log(DataClassName);
        GetTooltip(DataClassName);
    }

    public void GetTooltip(string itemClassName)
    {
        var itemData = DataManager.Inst.GetIteminfo(DataClassName);
        if (itemData == null)
            return;
        Price=itemData.Price;
    }
}

