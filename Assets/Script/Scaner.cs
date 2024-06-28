using Mirror.Examples.BilliardsPredicted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scaner : MonoBehaviour
{
    public Camera baseCamera;
    public Camera itemCamera;
    private bool isItemCameraActive = false;
    private float originalNearClip;
    private float originalFarClip;
    private float targetNearClip = 0.1f;
    private float targetFarClip = 100f;

    void Start()
    {
        // ó������ Item Camera�� ��Ȱ��ȭ�մϴ�.
        itemCamera.gameObject.SetActive(false);

        // ������ Clipping Planes ���� �����մϴ�.
        originalNearClip = baseCamera.nearClipPlane;
        originalFarClip = baseCamera.farClipPlane;
    }


    void Update()
    {
        // ��Ŭ�� ���¸� Ȯ���մϴ�.
        if (Input.GetMouseButtonDown(1))
        {
            isItemCameraActive = true;
            itemCamera.gameObject.SetActive(true);

            // Clipping Planes ���� �����մϴ�.
            baseCamera.nearClipPlane = targetNearClip;
            baseCamera.farClipPlane = targetFarClip;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isItemCameraActive = false;
            itemCamera.gameObject.SetActive(false);

            // Clipping Planes ���� ������� �����մϴ�.
            baseCamera.nearClipPlane = originalNearClip;
            baseCamera.farClipPlane = originalFarClip;
        }
    }


    public void OnScan()
    {

        itemCamera.gameObject.SetActive(true);
        baseCamera.nearClipPlane = Mathf.Lerp(0.1f, 100f, Time.deltaTime);
    }
}
