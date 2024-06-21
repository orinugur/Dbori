using Mirror.Examples.BilliardsPredicted;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    public Camera mainCamera;
    Ray ray;
    RaycastHit hit;
    [SerializeField]
    public float range = 100f;
    //public List<GameObject> inventoryIndex = new List<GameObject>();
    [SerializeField]
    public GameObject[] inventoryIndex = new GameObject[5];
    private UnityEngine.InputSystem.PlayerInput playerInput;

    public string path;
    [SerializeField]
    public int selectedSlot = 0; // 선택된 슬롯 번호
    public Transform inventory;
    public float throwSpeed;

    public void Awake()
    {
        items.Clear(); // 게임이 시작시 아이템 인벤토리를 클리어
        inventoryIndex = new GameObject[5];
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
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
        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f); // 카메라의 뷰포인트로 레이캐스트 실행
        Debug.Log("OnInteract");
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

        if (inventoryIndex[selectedSlot] != null) // 선택된 슬롯이 비어있지 않은 경우
        {
            // 아이템의 부모를 해제하고 독립 객체로 만듦
            GameObject item = inventoryIndex[selectedSlot];
            item.transform.SetParent(null);
            item.SetActive(true);

            // Ray를 쏜 지점의 트랜스폼에 해당 물건을 놓음
            if (Physics.Raycast(ray, out hit, range))
            {
                Vector3 point = (hit.point - item.transform.position).normalized;
                Debug.Log(point.sqrMagnitude);
                Debug.Log(point);
                Rigidbody rd = item.GetComponent<Rigidbody>();
                rd.AddForce(point* throwSpeed, ForceMode.Impulse);
                //item.transform.position = hit.point;
                Debug.Log($"Item placed at {hit.point}");
            }
            else
            {
                Vector3 point = ray.direction.normalized;
                Rigidbody rd = item.GetComponent<Rigidbody>();
                rd.AddForce(point * throwSpeed, ForceMode.Impulse);
            }
            inventoryIndex[selectedSlot] = null; // 슬롯 비우기
        }
        else // 선택된 슬롯이 비어있는 경우
        {
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
                            hit.transform.position = inventory.position;
                            hit.transform.gameObject.SetActive(false);
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
            else
            {
                Debug.Log("Nothing hit.");
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
    }
    private void OnSelect(InputValue value)
    {
        // 현재 액션맵과 액션을 가져옴
        var action = playerInput.actions["Select"];

        // 액션이 실행 중인지 확인
        if (action.phase == InputActionPhase.Performed)
        {
            // 입력된 키의 Path 가져오기
            path = action.activeControl.path;

            // Path 값을 기반으로 switch 문 실행
            Debug.Log(path);

            switch (path)
            {
                case "/Keyboard/1":
                    Debug.Log("1을 입력했습니다.");
                    selectedSlot = 0;
                    break;
                case "/Keyboard/2":
                    Debug.Log("2를 입력했습니다.");
                    selectedSlot = 1;
                    break;
                case "/Keyboard/3":
                    Debug.Log("3을 입력했습니다.");
                    selectedSlot = 2;
                    break;
                case "/Keyboard/4":
                    Debug.Log("4을 입력했습니다.");
                    selectedSlot = 3;
                    break;
                case "/Keyboard/5":
                    Debug.Log("5을 입력했습니다.");
                    selectedSlot = 4;
                    break;
                default:
                    break;
            }
        }
    }
    //private void OnSelect(InputValue value)
    //{
    //    // 입력 값을 float로 가져옴
    //    float input = value.Get<float>();
    //    int inputstr = value.Get<int>();
    //    Debug.Log(input);
    //    Debug.Log("value.int = " + inputstr);
    //    // float 값을 정수로 변환
    //    int inputNumber = (int)input;

    //    //string inputstr = input.Get<string>();
    //    //Debug.Log(inputstr); //쓰바 이거 값을어캐받지?

    //    switch (inputNumber.ToString())
    //    {
    //        case "1":
    //            selectedSlot = 0;
    //            break;
    //        case "2":
    //            selectedSlot = 1;
    //            break;
    //        case "3":
    //            selectedSlot = 2;
    //            break;
    //        case "4":
    //            selectedSlot = 3;
    //            break;
    //        case "5":
    //            selectedSlot = 4;
    //            break;
    //        default:
    //            selectedSlot = -1;
    //            Debug.Log("Invalid slot selection.");
    //            break;
    //    }
    //    Debug.Log($"Selected slot: {selectedSlot + 1}");
    //}


}



