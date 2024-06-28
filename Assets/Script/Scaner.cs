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
        // 처음에는 Item Camera를 비활성화합니다.
        itemCamera.gameObject.SetActive(false);

        // 원래의 Clipping Planes 값을 저장합니다.
        originalNearClip = baseCamera.nearClipPlane;
        originalFarClip = baseCamera.farClipPlane;
    }


    void Update()
    {
        // 우클릭 상태를 확인합니다.
        if (Input.GetMouseButtonDown(1))
        {
            isItemCameraActive = true;
            itemCamera.gameObject.SetActive(true);

            // Clipping Planes 값을 변경합니다.
            baseCamera.nearClipPlane = targetNearClip;
            baseCamera.farClipPlane = targetFarClip;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isItemCameraActive = false;
            itemCamera.gameObject.SetActive(false);

            // Clipping Planes 값을 원래대로 복귀합니다.
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
