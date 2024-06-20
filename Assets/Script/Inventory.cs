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
    //public List<GameObject> inventoryIndex = new List<GameObject>();
    public GameObject[] inventoryIndex = new GameObject[4];

    public int selectedSlot = 1; // 선택된 슬롯 번호

    public void Awake()
    {
        items.Clear(); // 게임이 시작시 아이템 인벤토리를 클리어
        inventoryIndex = new GameObject[4];
    }


    //    void OnInteract(InputValue inputValue) //Oninteract를 발생시 실행
    //    {
    //        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f); //카메라의 뷰포인트로 레이케스트 실행
    //        Debug.Log("oninteract");
    //        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);


    //        if (Physics.Raycast(ray, out hit, range))
    //        {

    //            // 일정 거리 이하의 충돌 무시
    //            //if (hit.distance >= 1f && hit.transform.tag == "Item") //히트한  물체의 태그가 Item 일 경우에 실행
    //            if (hit.transform.tag == "Item") //히트한  물체의 태그가 Item 일 경우에 실행
    //            {
    //                hit.transform.SetParent(transform);
    //                Debug.Log("tag is Item");
    //                //inventoryIndex[1].Add(hit);
    //            }
    //            else
    //            {
    //                Debug.Log(hit.transform.tag);
    //            }


    //        }
    //        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
    //    }
    //    void OnSelect(InputValue input)
    //    {
    //        Debug.Log(input.ToString());
    //        Debug.Log("OnSelect");
    //    }


    void OnInteract(InputValue inputValue) // F 키를 눌렀을 때 실행
    {
        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f); // 카메라의 뷰포인트로 레이케스트 실행
        Debug.Log("OnInteract");
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform.tag == "Item") // 히트한 물체의 태그가 Item일 경우에 실행
            {
                if (selectedSlot >= 0 && selectedSlot < inventoryIndex.Length) // 유효한 슬롯이 선택된 경우
                {
                    if (inventoryIndex[selectedSlot] == null) // 선택된 슬롯이 비어있는 경우
                    {
                        inventoryIndex[selectedSlot] = hit.transform.gameObject;
                        hit.transform.SetParent(transform);
                        Debug.Log($"Item added to slot {selectedSlot + 1}");
                    }
                    else
                    {
                        Debug.Log("Selected slot is already occupied.");
                    }
                }
                else
                {
                    Debug.Log("No valid slot selected.");
                }
            }
            else
            {
                Debug.Log(hit.transform.tag);
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
    }

    void OnSelect(InputValue input) // 1~5 키를 눌렀을 때 실행
    {

        string inputstr = input.ToString();
        Debug.Log(inputstr); //쓰바 이거 값을어캐받지?
        

        switch (inputstr)
        {
            case "1":
                selectedSlot = 0;
                break;
            case "2":
                selectedSlot = 1;
                break;
            case "3":
                selectedSlot = 2;
                break;
            case "4":
                selectedSlot = 3;
                break;
            case "5":
                selectedSlot = 4;
                break;
            default:
                selectedSlot = -1;
                Debug.Log("Invalid slot selection.");
                break;
        }
        Debug.Log($"Selected slot: {selectedSlot + 1}");
    }


}



