using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager> 
{
    public List<MonsterSpawn> Spawner;
    public float SpawnCount;

    private void Update()
    {
        
        SpawnCount += Time.deltaTime;

        if (SpawnCount > 30)
        {
            // ����Ʈ���� ������ ��Ҹ� ���� ����Ʈ
            List<MonsterSpawn> toRemove = new List<MonsterSpawn>();

            // ����Ʈ�� ��ȸ�ϸ鼭 ���� ����
            for (int i = 0; i < Spawner.Count; i++)
            {
                if (Spawner[i] != null)
                {
                    Spawner[i].SpawnMonster();
                }
                else
                {
                    // null�� ��� ������ ��ҷ� �߰�
                    toRemove.Add(Spawner[i]);
                }
            }

            // ������ ��Ҹ� ���� ����Ʈ���� ����
            foreach (var item in toRemove)
            {
                Spawner.Remove(item);
            }

            SpawnCount = 0;
        }
    }
}
