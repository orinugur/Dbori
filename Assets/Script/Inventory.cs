using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    public Camera mainCamera;
    Ray ray;
    RaycastHit hit;
    public float range = 100f;
    public List<GameObject> inventoryIndex = new List<GameObject>();


    public void Awake()
    {
        items.Clear();//게임이 시작시 아이템 인벤토리를 클리어
    }
    void OnInteract(InputValue inputValue) //Oninteract를 발생시 실행
    {
        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f); //카메라의 뷰포인트로 레이케스트 실행
        Debug.Log("oninteract");
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);


        if (Physics.Raycast(ray, out hit, range))
        {

            // 일정 거리 이하의 충돌 무시
            //if (hit.distance >= 1f && hit.transform.tag == "Item") //히트한  물체의 태그가 Item 일 경우에 실행
            if (hit.transform.tag == "Item") //히트한  물체의 태그가 Item 일 경우에 실행
            {
                hit.transform.SetParent(transform);
                Debug.Log("tag is Item");
                //inventoryIndex[1].Add(hit);
            }
            else
            {
                Debug.Log(hit.transform.tag);
            }
            

        }
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
    }
    void OnSelect(InputValue input)
    {
        Debug.Log(input.ToString());
        Debug.Log("OnSelect");
    }
}
