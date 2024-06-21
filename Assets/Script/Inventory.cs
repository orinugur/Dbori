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
    public int selectedSlot = 0; // ���õ� ���� ��ȣ
    public Transform inventory;
    public float throwSpeed;

    public void Awake()
    {
        items.Clear(); // ������ ���۽� ������ �κ��丮�� Ŭ����
        inventoryIndex = new GameObject[5];
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
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
        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f); // ī�޶��� ������Ʈ�� ����ĳ��Ʈ ����
        Debug.Log("OnInteract");
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

        if (inventoryIndex[selectedSlot] != null) // ���õ� ������ ������� ���� ���
        {
            // �������� �θ� �����ϰ� ���� ��ü�� ����
            GameObject item = inventoryIndex[selectedSlot];
            item.transform.SetParent(null);
            item.SetActive(true);

            // Ray�� �� ������ Ʈ�������� �ش� ������ ����
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
            inventoryIndex[selectedSlot] = null; // ���� ����
        }
        else // ���õ� ������ ����ִ� ���
        {
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
        // ���� �׼Ǹʰ� �׼��� ������
        var action = playerInput.actions["Select"];

        // �׼��� ���� ������ Ȯ��
        if (action.phase == InputActionPhase.Performed)
        {
            // �Էµ� Ű�� Path ��������
            path = action.activeControl.path;

            // Path ���� ������� switch �� ����
            Debug.Log(path);

            switch (path)
            {
                case "/Keyboard/1":
                    Debug.Log("1�� �Է��߽��ϴ�.");
                    selectedSlot = 0;
                    break;
                case "/Keyboard/2":
                    Debug.Log("2�� �Է��߽��ϴ�.");
                    selectedSlot = 1;
                    break;
                case "/Keyboard/3":
                    Debug.Log("3�� �Է��߽��ϴ�.");
                    selectedSlot = 2;
                    break;
                case "/Keyboard/4":
                    Debug.Log("4�� �Է��߽��ϴ�.");
                    selectedSlot = 3;
                    break;
                case "/Keyboard/5":
                    Debug.Log("5�� �Է��߽��ϴ�.");
                    selectedSlot = 4;
                    break;
                default:
                    break;
            }
        }
    }
    //private void OnSelect(InputValue value)
    //{
    //    // �Է� ���� float�� ������
    //    float input = value.Get<float>();
    //    int inputstr = value.Get<int>();
    //    Debug.Log(input);
    //    Debug.Log("value.int = " + inputstr);
    //    // float ���� ������ ��ȯ
    //    int inputNumber = (int)input;

    //    //string inputstr = input.Get<string>();
    //    //Debug.Log(inputstr); //���� �̰� ������ĳ����?

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



