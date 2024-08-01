using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedText : MonoBehaviour
{
    public Material TextFont;
    public float max;
    public float currnet;
    public float startValue = -1f; // 시작 값
    public float endValue = 0f; // 종료 값
    public float duration = 0.8f; // 감소할 시간 (초)
    private float currentValue; // 현재 값

    private void Awake()
    {

        TextFont.SetFloat("_FaceDilate", -1);
        StartCoroutine(DecreaseValueOverTime());
    }


    void Update()
    {
        TextFont.SetFloat("_FaceDilate", currentValue);
    }

    IEnumerator DecreaseValueOverTime()
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime; // 시간 누적
            currentValue = Mathf.Lerp(startValue, endValue, timer / duration); // 시간에 따라 점진적으로 값 감소
            Debug.Log("Current Value: " + currentValue);
            yield return null;
        }
        currentValue = endValue; // 최종 값으로 설정
        Debug.Log("Final Value Reached: " + currentValue);
    }


}
