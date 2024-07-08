using Org.BouncyCastle.Asn1.TeleTrust;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public InBox inbox;
    public TextMeshProUGUI inturectText;
    void Start()
    {
            
        if(inbox == null)
        {
            transform.GetChild(2).GetComponent<InBox>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inturectText.text = " Psees 'G' to Exit";
            if (Input.GetKeyDown(KeyCode.G))
            {
                Debug.Log(other.transform.gameObject);
                inbox.ExitSuccess(other.transform.gameObject);
                //other.gameObject.tag = "Default";
            }
          
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        inturectText.text = null;
    }
}
