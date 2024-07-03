using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCam : MonoBehaviour
{

    public Transform target; // 타겟 오브젝트의 Transform

    public Vector3 offset; // 카메라와 타겟 사이의 거리

    public Vector3 startVector;
    public float smoothSpeed = 0.125f; // 카메라 이동 속도


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
