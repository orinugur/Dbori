using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class ProceduralRoomGenerator2 : MonoBehaviour
{
    public List<GameObject> roomPrefabs; // �� Prefab���� Inspector���� �Ҵ��ϼ���.
    public Vector3 startPosition; // ���� ��ġ
    public float roomOffset = 10.0f; // �� ���� �Ÿ� (������ ���� �ʿ�)
    public int maxRooms = 10; // �ִ� �� ���� (�ʱⰪ 10)

    private Dictionary<GameObject, List<Transform>> roomPortals = new Dictionary<GameObject, List<Transform>>();
    private List<GameObject> generatedRooms = new List<GameObject>(); // ������ ����� ����
    
    public NavMeshSurface navMeshSurface; // NavMeshSurface ������Ʈ�� �Ҵ��ϼ���.
    void Start()
    {
        InitializeAndGenerateRooms();
    }

    void InitializeAndGenerateRooms()
    {
        while (generatedRooms.Count < maxRooms)
        {
            InitializeRoomPortals();
            GenerateInitRooms();

            if (generatedRooms.Count < maxRooms)
            {
                //Debug.Log("Not enough rooms generated. Restarting...");
                ClearGeneratedRooms();
            }
        }
        //  BakeNavMesh(); // �� ������ �Ϸ�� �� NavMesh�� ����ũ�մϴ�.
        StartCoroutine(Bake());
    }

    IEnumerator Bake()
    {
        yield return new WaitForSecondsRealtime(5f);
        BakeNavMesh(); // �� ������ �Ϸ�� �� NavMesh�� ����ũ�մϴ�.
    }
    void InitializeRoomPortals()
    {
        foreach (var room in roomPrefabs)
        {
            List<Transform> portals = FindPortals(room);
            roomPortals.Add(room, portals);
        }
    }

    List<Transform> FindPortals(GameObject room)
    {
        List<Transform> portals = new List<Transform>();
        Transform portalsTransform = room.transform.Find("Portals");

        if (portalsTransform != null)
        {
            foreach (Transform portal in portalsTransform)
            {
                portals.Add(portal);
            }
        }
        else
        {
            //Debug.LogWarning($"Room {room.name} does not have a 'Portals' object.");
        }

        return portals;
    }

    void GenerateInitRooms()
    {
        GameObject initialRoomPrefab = roomPrefabs[0];
        GameObject initialRoom = Instantiate(initialRoomPrefab, startPosition, Quaternion.identity);
        initialRoom.AddComponent<BoxCollider>().isTrigger = true; // Ʈ���� Collider �߰�

        generatedRooms.Add(initialRoom);

        List<Transform> initialRoomPortals = roomPortals[initialRoomPrefab];

        GenerateRooms(initialRoom, initialRoomPortals);

    }

    bool CheckCollision(GameObject newRoom)
    {
        BoxCollider newCollider = newRoom.GetComponent<BoxCollider>();
        if (newCollider == null)
        {
            return false;
        }

        foreach (var existingRoom in generatedRooms)
        {
            BoxCollider existingCollider = existingRoom.GetComponent<BoxCollider>();
            if (existingCollider == null)
            {
                continue;
            }

            Vector3 worldCenterNew = newCollider.transform.TransformPoint(newCollider.center);
            Vector3 worldCenterExisting = existingCollider.transform.TransformPoint(existingCollider.center);
            Vector3 worldSizeNew = newCollider.transform.TransformVector(newCollider.size);
            Vector3 worldSizeExisting = existingCollider.transform.TransformVector(existingCollider.size);

            worldSizeNew = new Vector3(Mathf.Abs(worldSizeNew.x), Mathf.Abs(worldSizeNew.y), Mathf.Abs(worldSizeNew.z));
            worldSizeExisting = new Vector3(Mathf.Abs(worldSizeExisting.x), Mathf.Abs(worldSizeExisting.y), Mathf.Abs(worldSizeExisting.z));

            Bounds boundsNew = new Bounds(worldCenterNew, worldSizeNew);
            Bounds boundsExisting = new Bounds(worldCenterExisting, worldSizeExisting);

            if (boundsNew.Intersects(boundsExisting))
            {
                //Debug.Log($"Collision Detected between new room and existing room {existingRoom.name}");
                return true;
            }
        }
        return false;
    }

    bool MatchPortalPositionAndRotation(Transform portalA, Transform portalB, GameObject currentRoom, GameObject newRoom)
    {
        // ��Ż A�� ��Ż B�� ���� �������� ���
        Vector3 portalAWorldPosition = currentRoom.transform.TransformPoint(portalA.localPosition);
        Vector3 portalBWorldPosition = newRoom.transform.TransformPoint(portalB.localPosition);

        // ��Ż B�� ��ġ�� ��Ż A�� ��ġ�� ���߱� ���� �������� ���
        Vector3 positionOffset = portalAWorldPosition - portalBWorldPosition;
        newRoom.transform.position += positionOffset;

        // ���ο� ��Ż B�� ���� �������� ���
        portalBWorldPosition = newRoom.transform.TransformPoint(portalB.localPosition);

        // ��Ż A�� ��Ż B�� ��ġ�� ��ġ�ϴ��� Ȯ��
        if (Vector3.Distance(portalAWorldPosition, portalBWorldPosition) < 0.01f)
        {
            for (int i = 0; i < 4; i++)
            {
                newRoom.transform.rotation = Quaternion.Euler(0, i * 90, 0);

                // ȸ�� �� ��Ż B�� ���� �������� �ٽ� ���
                portalBWorldPosition = newRoom.transform.TransformPoint(portalB.localPosition);
                positionOffset = portalAWorldPosition - portalBWorldPosition;

                // ��ġ�� ��ġ���� ������ �ٽ� ����
                if (Vector3.Distance(portalAWorldPosition, portalBWorldPosition) >= 0.01f)
                {
                    newRoom.transform.position += positionOffset;
                    portalBWorldPosition = newRoom.transform.TransformPoint(portalB.localPosition);
                }

                // ���� ��ġ�� ȸ�� �� �浹 Ȯ��
                if (Vector3.Distance(portalAWorldPosition, portalBWorldPosition) < 0.01f && !CheckCollision(newRoom))
                {
                    return true; // ��ġ�ϰ� �浹�� ������ true ��ȯ
                }
            }
        }

        return false; // ��� ȸ������ ��ġ���� �ʰų� �浹�� ������ false ��ȯ
    }


    void GenerateRooms(GameObject initialRoom, List<Transform> initialRoomPortals)
    {
        Queue<(GameObject, List<Transform>)> roomsToProcess = new Queue<(GameObject, List<Transform>)>();
        roomsToProcess.Enqueue((initialRoom, initialRoomPortals));

        while (roomsToProcess.Count > 0 && generatedRooms.Count < maxRooms)
        {
            var (currentRoom, currentRoomPortals) = roomsToProcess.Dequeue();

            foreach (Transform portal in currentRoomPortals)
            {
                if (generatedRooms.Count >= maxRooms)
                {
                    break;
                }

                // ù ��° �ε����� �������� ������� �ʵ��� ó��
                GameObject newRoomPrefab;
                do
                {
                    newRoomPrefab = roomPrefabs[Random.Range(1, roomPrefabs.Count)];
                } while (newRoomPrefab == roomPrefabs[0]);

                GameObject newRoom = Instantiate(newRoomPrefab, Vector3.zero, Quaternion.identity);
                newRoom.AddComponent<BoxCollider>().isTrigger = true; // Ʈ���� Collider �߰�

                Transform newRoomPortalsParent = newRoom.transform.Find("Portals");
                if (newRoomPortalsParent != null && newRoomPortalsParent.childCount > 0)
                {
                    // �ڽ� ��Ż �� �����ϰ� ����
                    //int randomIndex = Random.Range(0, newRoomPortalsParent.childCount);
                    Transform newRoomPortal = newRoomPortalsParent.GetChild(0);

                    if (MatchPortalPositionAndRotation(portal, newRoomPortal, currentRoom, newRoom))
                    {
                        generatedRooms.Add(newRoom);

                        List<Transform> newRoomPortals = roomPortals[newRoomPrefab];
                        newRoomPortals.Remove(newRoomPortal);
                        roomsToProcess.Enqueue((newRoom, newRoomPortals));
                    }
                    else
                    {
                        Destroy(newRoom);
                    }
                }
            }
        }


    }

    void ClearGeneratedRooms()
    {
        foreach (var room in generatedRooms)
        {
            Destroy(room);
        }
        generatedRooms.Clear();
        roomPortals.Clear();
    }
    void BakeNavMesh()
    {
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
        }
        else
        {
            Debug.LogError("NavMeshSurface is not assigned.");
        }
    }
}
