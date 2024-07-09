using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject[] monsters; // 변수 이름을 소문자로 시작하도록 변경
    public int randomNum;
    public int monsterNum;
    public int MaxMonster;
    public int MonsterCount;

    private void Awake()
    {
        if (SpawnManager.Instance != null)
        {
            SpawnManager.Instance.Spawner.Add(this);
        }
        else
        {
            Debug.LogError("SpawnManager instance is null!");
        }
        MaxMonster = 2;
    }

    public void SpawnMonster()
    {
        if (MonsterCount < MaxMonster)
        {

            randomNum = Random.Range(0, 10);
            if (randomNum > 8)
            {
                if (monsters.Length > 0)
                {
                    monsterNum = Random.Range(0, monsters.Length);
                    GameObject spawnedMonster = Instantiate(monsters[monsterNum], transform.position, Quaternion.identity);
                    MonsterCount++;
                }
                else
                {
                    Debug.LogError("No monsters available to spawn!");
                }
            }
            
        }

    }
}
