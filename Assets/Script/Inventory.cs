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
        items.Clear();//������ ���۽� ������ �κ��丮�� Ŭ����
    }
    void OnInteract(InputValue inputValue) //Oninteract�� �߻��� ����
    {
        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f); //ī�޶��� ������Ʈ�� �����ɽ�Ʈ ����
        Debug.Log("oninteract");
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);


        if (Physics.Raycast(ray, out hit, range))
        {

            // ���� �Ÿ� ������ �浹 ����
            //if (hit.distance >= 1f && hit.transform.tag == "Item") //��Ʈ��  ��ü�� �±װ� Item �� ��쿡 ����
            if (hit.transform.tag == "Item") //��Ʈ��  ��ü�� �±װ� Item �� ��쿡 ����
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
