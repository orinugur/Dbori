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
        if(SpawnCount>30)
        {
            foreach (var spawn in Spawner)
            {
                if (spawn != null)
                {
                    spawn.SpawnMonster();
                }
                if(spawn == null)
                {
                    Spawner.Remove(spawn);
                }
            }

            SpawnCount = 0;
        }


    }
}
