using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawn : Singleton<ItemSpawn>
{
    public GameObject[] items; // ������ ������ �迭
    public int minItems = 10; // �ּ� ������ ��
    public int maxItems = 50; // �ִ� ������ ��
    public int maxAttempts = 500; // ���� ��ġ ã�� �õ� Ƚ��


    public void SpawnItemsInNavMesh()
    {
        int itemsToSpawn = Random.Range(minItems, maxItems + 1);

        for (int i = 0; i < itemsToSpawn; i++)
        {
            Vector3 randomPosition;
            if (TryGetRandomNavMeshPosition(out randomPosition))
            {
                int itemIndex = Random.Range(0, items.Length);
                Instantiate(items[itemIndex], randomPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Failed to find a valid NavMesh position after multiple attempts.");
            }
        }
    }

    private bool TryGetRandomNavMeshPosition(out Vector3 result)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomPosition = GetRandomPositionWithinBounds();
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    private Vector3 GetRandomPositionWithinBounds()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int vertexIndex = Random.Range(0, navMeshData.vertices.Length);
        Vector3 randomPosition = navMeshData.vertices[vertexIndex];

        return randomPosition;
    }
}
