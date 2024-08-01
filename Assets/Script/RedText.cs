using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedText : MonoBehaviour
{
    public Material TextFont;
    public float max;
    public float currnet;
    public float startValue = -1f; // ���� ��
    public float endValue = 0f; // ���� ��
    public float duration = 0.8f; // ������ �ð� (��)
    private float currentValue; // ���� ��

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
            timer += Time.deltaTime; // �ð� ����
            currentValue = Mathf.Lerp(startValue, endValue, timer / duration); // �ð��� ���� ���������� �� ����
            Debug.Log("Current Value: " + currentValue);
            yield return null;
        }
        currentValue = endValue; // ���� ������ ����
        Debug.Log("Final Value Reached: " + currentValue);
    }


}
