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

    public int selectedSlot = 1; // ���õ� ���� ��ȣ

    public void Awake()
    {
        items.Clear(); // ������ ���۽� ������ �κ��丮�� Ŭ����
        inventoryIndex = new GameObject[4];
    }


    //    void OnInteract(InputValue inputValue) //Oninteract�� �߻��� ����
    //    {
    //        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f); //ī�޶��� ������Ʈ�� �����ɽ�Ʈ ����
    //        Debug.Log("oninteract");
    //        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);


    //        if (Physics.Raycast(ray, out hit, range))
    //        {

    //            // ���� �Ÿ� ������ �浹 ����
    //            //if (hit.distance >= 1f && hit.transform.tag == "Item") //��Ʈ��  ��ü�� �±װ� Item �� ��쿡 ����
    //            if (hit.transform.tag == "Item") //��Ʈ��  ��ü�� �±װ� Item �� ��쿡 ����
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


    void OnInteract(InputValue inputValue) // F Ű�� ������ �� ����
    {
        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f); // ī�޶��� ������Ʈ�� �����ɽ�Ʈ ����
        Debug.Log("OnInteract");
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.transform.tag == "Item") // ��Ʈ�� ��ü�� �±װ� Item�� ��쿡 ����
            {
                if (selectedSlot >= 0 && selectedSlot < inventoryIndex.Length) // ��ȿ�� ������ ���õ� ���
                {
                    if (inventoryIndex[selectedSlot] == null) // ���õ� ������ ����ִ� ���
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

    void OnSelect(InputValue input) // 1~5 Ű�� ������ �� ����
    {

        string inputstr = input.ToString();
        Debug.Log(inputstr); //���� �̰� ������ĳ����?
        

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



