using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OnRoomUi : MonoBehaviour
{

    private GameObject crateRoomUI;

    public void OnClickEnterGameRoom()
    {
        var manager = BallRoomManager.singleton;
        manager.StartClient();
    }
    
}
