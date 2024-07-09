using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NightVision : MonoBehaviour
{
    public float timmer = 30;
    private void OnEnable()
    {
        StartCoroutine(die(timmer));
    }

    IEnumerator die(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        gameObject.SetActive(false);    
    }
}
