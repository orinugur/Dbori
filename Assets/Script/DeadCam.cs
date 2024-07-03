using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCam : MonoBehaviour
{

    public Transform target; // Ÿ�� ������Ʈ�� Transform

    public Vector3 offset; // ī�޶�� Ÿ�� ������ �Ÿ�

    public Vector3 startVector;
    public float smoothSpeed = 0.125f; // ī�޶� �̵� �ӵ�


    void Start()
    {
        startVector = transform.position;
    }


    private void FixedUpdate()
    {
        FlowBus();
    }



    void FlowBus()
    {
        offset = target.transform.position + startVector;
        gameObject.transform.position = new Vector3(offset.x, startVector.y, offset.z);

    }
}
