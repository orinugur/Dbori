using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Camera mainCamera;
    Ray ray;
    RaycastHit hit;
    [SerializeField]
    public float range = 100f;
    [SerializeField]
    public GameObject[] inventoryIndex = new GameObject[5];
    public UnityEngine.InputSystem.PlayerInput playerInput;

    public string path;
    [SerializeField]
    public int selectedSlot = 0; // ���õ� ���� ��ȣ
    public Transform inventory;
    public float throwSpeed;

    public Image[] inventoryImage;
    private Vector3 rayStart;
    private Vector3 rayEnd;

    public void Awake()
    {
        inventoryIndex = new GameObject[5];
        inventoryImage = new Image[5];

        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
    }

    public void Start()
    {
        inventory=transform.GetChild(0);
        GameObject inventoryUI = GameObject.Find("InventoryUi");
        for (int i = 0; i < inventoryImage.Length; i++)
        {
            inventoryImage[i] = inventoryUI.transform.GetChild(i).GetChild(0).GetComponent<Image>();
            inventoryImage[i].sprite = null;
            inventoryImage[i].enabled = false;
        }
    }

    void Update()
    {
        // �� �����Ӹ��� ����ĳ��Ʈ�� ������Ʈ
        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
    }

    void OnInteract(InputValue inputValue) // F Ű�� ������ �� ����
    {

        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f);// ī�޶��� ������Ʈ�� ����ĳ��Ʈ ����

        // LayerMask ���� - Player ���̾ ������ ��� ���̾�
        int layerMask = ~(1 << LayerMask.NameToLayer("Player"));
        Debug.Log("OnInteract");

        // RaycastAll�� ����Ͽ� ��� ��Ʈ ����� �����ɴϴ�.
        RaycastHit[] hits = Physics.RaycastAll(ray, range, layerMask);

        // ù ��°�� ��Ʈ�� "Item" �±׸� ���� ������Ʈ�� ã���ϴ�.
        GameObject hitItem = null;
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.tag == "Item")
            {
                hitItem = hit.transform.gameObject;
                break;
            }
        }

        if (inventoryIndex[selectedSlot] != null) // ���õ� ������ ������� ���� ���
        {
            // �������� �θ� �����ϰ� ���� ��ü�� ����
            GameObject item = inventoryIndex[selectedSlot];
            item.transform.SetParent(null);
            item.SetActive(true);
            inventoryImage[selectedSlot].sprite = null; // �ش� ������ �̹����� Null�� ����
            inventoryImage[selectedSlot].enabled = false;

            // Ray�� �� ������ Ʈ�������� �ش� ������ ����
            //if (hitItem != null)
            //{
            //    Vector3 point = (hit.point - item.transform.position).normalized;
            //    Rigidbody rd = item.GetComponent<Rigidbody>();
            //    rd.AddForce(point * throwSpeed, ForceMode.Impulse);
            //    Debug.Log($"Item placed at {hit.point}");
            //    Debug.Log($"Item placed at {hitItem.name}");
            //}
            //else
            //{
                Vector3 point = ray.direction.normalized;
                Rigidbody rd = item.GetComponent<Rigidbody>();
                rd.velocity = Vector3.zero;
                rd.AddForce(point * throwSpeed, ForceMode.Impulse);
            //}
            inventoryIndex[selectedSlot] = null; // ���� ����
        }
        else // ���õ� ������ ����ִ� ���
        {
            if (hitItem != null) // ��Ʈ�� "Item" �±׸� ���� ������Ʈ�� ���� ��쿡 ����
            {
                if (selectedSlot >= 0 && selectedSlot < inventoryIndex.Length) // ��ȿ�� ������ ���õ� ���
                {
                    if (inventoryIndex[selectedSlot] == null) // ���õ� ������ ����ִ� ���
                    {
                        inventoryIndex[selectedSlot] = hitItem;
                        hitItem.transform.SetParent(inventory);
                        hitItem.transform.position = inventory.position;
                        hitItem.transform.rotation= Quaternion.Euler(0f, 0f, 0f);
                        hitItem.SetActive(false);
                        Debug.Log($"Item added to slot {selectedSlot + 1}");
                        inventoryImage[selectedSlot].sprite = hitItem.GetComponent<ItemS>().asdf2;
                        inventoryImage[selectedSlot].enabled = true;
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
                Debug.Log("Nothing hit.");
            }
        }
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

    void OnDrawGizmos()
    {
        // ����ĳ��Ʈ�� �������� ������ ����ؼ� �׸��ϴ�.
        if (ray.origin != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * range);
        }
    }
}


//using Mirror.Examples.BilliardsPredicted;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.UI;
//using static UnityEngine.Rendering.DebugUI;

//public class Inventory : MonoBehaviour
//{
//    //public List<Item> items;
//    public Camera mainCamera;
//    Ray ray;
//    RaycastHit hit;
//    [SerializeField]
//    public float range = 100f;
//    //public List<GameObject> inventoryIndex = new List<GameObject>();
//    [SerializeField]
//    public GameObject[] inventoryIndex = new GameObject[5];
//    public  UnityEngine.InputSystem.PlayerInput playerInput;

//    public string path;
//    [SerializeField]
//    public int selectedSlot = 0; // ���õ� ���� ��ȣ
//    public Transform inventory;
//    public float throwSpeed;

//    public Image[] inventoryImage;
//    public void Awake()
//    {
//       // items.Clear(); // ������ ���۽� ������ �κ��丮�� Ŭ����
//        inventoryIndex = new GameObject[5];
//        //inventoryUIIndex = new GameObject[5];
//        inventoryImage = new Image[5];

//        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
//    }
//    public void Start()
//    {
//        GameObject inventoryUI = GameObject.Find("InventoryUi");
//        for (int i = 0; i < inventoryImage.Length; i++)
//        {
//            inventoryImage[i] = inventoryUI.transform.GetChild(i).GetChild(0).GetComponent<Image>();
//            inventoryImage[i].sprite = null;
//            inventoryImage[i].enabled= false;
//        }
//    }

//    void OnInteract(InputValue inputValue) // F Ű�� ������ �� ����
//    {
//        ray = mainCamera.ViewportPointToRay(Vector2.one * 0.5f); // ī�޶��� ������Ʈ�� ����ĳ��Ʈ ����
//        //ray = mainCamera.ViewportPointToRay(Vector2.one); // ī�޶��� ������Ʈ�� ����ĳ��Ʈ ����
//        Debug.Log("OnInteract");
//        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

//        if (inventoryIndex[selectedSlot] != null) // ���õ� ������ ������� ���� ���
//        {
//            // �������� �θ� �����ϰ� ���� ��ü�� ����
//            GameObject item = inventoryIndex[selectedSlot];
//            item.transform.SetParent(null);
//            item.SetActive(true);
//            inventoryImage[selectedSlot].sprite=null;//�ش罽���� �̹����� Null�� ����
//            inventoryImage[selectedSlot].enabled = false;

//            // Ray�� �� ������ Ʈ�������� �ش� ������ ����
//            if (Physics.Raycast(ray, out hit, range))
//            {
//                Vector3 point = (hit.point - item.transform.position).normalized;
//                Debug.Log(point.sqrMagnitude);
//                Debug.Log(point);
//                Rigidbody rd = item.GetComponent<Rigidbody>();
//                rd.AddForce(point* throwSpeed, ForceMode.Impulse);
//                //item.transform.position = hit.point;
//                Debug.Log($"Item placed at {hit.point}");
//            }
//            else
//            {
//                Vector3 point = ray.direction.normalized;
//                Rigidbody rd = item.GetComponent<Rigidbody>();
//                rd.AddForce(point * throwSpeed, ForceMode.Impulse);
//            }
//            inventoryIndex[selectedSlot] = null; // ���� ����
//        }
//        else // ���õ� ������ ����ִ� ���
//        {
//            if (Physics.Raycast(ray, out hit, range))
//            {
//                if (hit.transform.tag == "Item") // ��Ʈ�� ��ü�� �±װ� Item�� ��쿡 ����
//                {
//                    if (selectedSlot >= 0 && selectedSlot < inventoryIndex.Length) // ��ȿ�� ������ ���õ� ���
//                    {
//                        if (inventoryIndex[selectedSlot] == null) // ���õ� ������ ����ִ� ���
//                        {

//                            inventoryIndex[selectedSlot] = hit.transform.gameObject;
//                            hit.transform.SetParent(transform);
//                            hit.transform.position = inventory.position;
//                            hit.transform.gameObject.SetActive(false);
//                            Debug.Log($"Item added to slot {selectedSlot + 1}");
//                            inventoryImage[selectedSlot].sprite = hit.transform.GetComponent<Image>().sprite;
//                            inventoryImage[selectedSlot].enabled = true;
//                        }
//                        else
//                        {
//                            Debug.Log("Selected slot is already occupied.");
//                        }
//                    }
//                    else
//                    {
//                        Debug.Log("No valid slot selected.");
//                    }
//                }
//                else
//                {
//                    Debug.Log(hit.transform.tag);
//                }
//            }
//            else
//            {
//                Debug.Log("Nothing hit.");
//            }
//        }

//        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
//    }
//    private void OnSelect(InputValue value)
//    {
//        // ���� �׼Ǹʰ� �׼��� ������
//        var action = playerInput.actions["Select"];

//        // �׼��� ���� ������ Ȯ��
//        if (action.phase == InputActionPhase.Performed)
//        {
//            // �Էµ� Ű�� Path ��������
//            path = action.activeControl.path;

//            // Path ���� ������� switch �� ����
//            Debug.Log(path);

//            switch (path)
//            {
//                case "/Keyboard/1":
//                    Debug.Log("1�� �Է��߽��ϴ�.");
//                    selectedSlot = 0;
//                    break;
//                case "/Keyboard/2":
//                    Debug.Log("2�� �Է��߽��ϴ�.");
//                    selectedSlot = 1;
//                    break;
//                case "/Keyboard/3":
//                    Debug.Log("3�� �Է��߽��ϴ�.");
//                    selectedSlot = 2;
//                    break;
//                case "/Keyboard/4":
//                    Debug.Log("4�� �Է��߽��ϴ�.");
//                    selectedSlot = 3;
//                    break;
//                case "/Keyboard/5":
//                    Debug.Log("5�� �Է��߽��ϴ�.");
//                    selectedSlot = 4;
//                    break;
//                default:
//                    break;
//            }
//        }
//    }


//}



