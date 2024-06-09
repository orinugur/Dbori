using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BallCube : NetworkBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    [ClientRpc]
    void RpcUpdateBallPosition(Vector3 position, Vector3 velocity)
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody is null. Cannot update ball position.");
            return;
        }

        rb.position = position;
        rb.velocity = velocity;
    }
}
