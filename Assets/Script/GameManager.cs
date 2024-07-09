using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int U_Num;
    public int price;
    public List<ItemS> items;
    public List<string> names;
    public List<int> itemPrice;
    public void SetUserNum(int Num)
    {
        U_Num = Num;
    }
    public void ExitGame()
    {

    }
}
