using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int U_Num;
    public int price;
    public void SetUserNum(int Num)
    {
        U_Num = Num;
    }
    public void ExitGame()
    {

    }
}
