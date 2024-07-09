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
            // 리스트에서 삭제할 요소를 담을 리스트
            List<MonsterSpawn> toRemove = new List<MonsterSpawn>();

            // 리스트를 순회하면서 몬스터 스폰
            for (int i = 0; i < Spawner.Count; i++)
            {
                if (Spawner[i] != null)
                {
                    Spawner[i].SpawnMonster();
                }
                else
                {
                    // null인 경우 삭제할 요소로 추가
                    toRemove.Add(Spawner[i]);
                }
            }

            // 삭제할 요소를 원래 리스트에서 제거
            foreach (var item in toRemove)
            {
                Spawner.Remove(item);
            }

            SpawnCount = 0;
        }
    }
}
