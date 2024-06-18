using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSell : MonoBehaviour
{
    Vector3 curruntTransform;
    Quaternion currutRotation;
    Rigidbody rb;
    public GameObject sell;
    private void Awake()
    {
        //transform.position=Vector3.zero;
        //transform.rotation=Quaternion.identity;
        rb = sell.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }
    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        ExitSell();
    //    }
    //    else if(Input.GetKeyDown(KeyCode.V))
    //    {
    //        InsertSell();

    //    }
    //}

    public void ExitSell()
    {
        rb.useGravity=true;
    }
    public void InsertSell()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.isKinematic = false;
        sell.transform.localPosition = Vector3.zero;
        //transform.rotation = Quaternion.Euler(0f, 0f, 0f); 
        sell.transform.localRotation = Quaternion.Euler(0f, 0f, 0f); 
    }


}
