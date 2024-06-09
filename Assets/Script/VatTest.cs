using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class VatTest : NetworkBehaviour
{
    public float restPosition = 0;
    public float pressedPosition = 45f;
    public float hitStrength = 10000f;
    public float flipperDamper = 150f;
    HingeJoint hinge;
    public string inputName;
    Rigidbody rb;

    [SyncVar]
    public bool isLeftFlipper; // true for left, false for right

    // [SyncVar]
    //   public NetworkIdentity playerIdentity;  // Flipper를 조종할 플레이어

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //   if (playerIdentity == null || !playerIdentity.isLocalPlayer) return;

        bool isLeftInput = Input.GetKey(KeyCode.LeftArrow);
        bool isRightInput = Input.GetKey(KeyCode.RightArrow);

        if (Input.GetKey(KeyCode.Space))
        {
            CmdSetFlipperPosition(pressedPosition);
        }
        else
        {
            CmdSetFlipperPosition(restPosition);
        }
    }

    [Command]
    void CmdSetFlipperPosition(float position)
    {
        RpcSetFlipperPosition(position);
    }

    [ClientRpc]
    void RpcSetFlipperPosition(float position)
    {
        JointSpring spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipperDamper;
        spring.targetPosition = position;
        hinge.spring = spring;
        hinge.useLimits = true;
    }

    //[Command]
    //public void AssignToPlayer(NetworkIdentity player)
    //{
    //    playerIdentity = player;
    //}
}
