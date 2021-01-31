using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public static SpawnEvent SpawnZone1 = new SpawnEvent();
    public static SpawnEvent SpawnZone1And2 = new SpawnEvent();
    public static SpawnEvent SpawnZone1And2And3 = new SpawnEvent();

    List<EnemySpawner> zone1Spawners = new List<EnemySpawner>();
    List<EnemySpawner> zone2Spawners = new List<EnemySpawner>();
    List<EnemySpawner> zone3Spawners = new List<EnemySpawner>();

    private void Awake()
    {
        EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();
        foreach (EnemySpawner spawner in spawners)
        {
            switch (spawner.SpawnerZone)
            {
                case Zone.Zone1:
                    zone1Spawners.Add(spawner);
                    break;
                case Zone.Zone2:
                    zone2Spawners.Add(spawner);
                    break;
                case Zone.Zone3:
                    zone3Spawners.Add(spawner);
                    break;
            }
        }

        SpawnZone1.AddListener(SpawnInZone1);
        SpawnZone1And2.AddListener(SpawnInZone1And2);
        SpawnZone1And2And3.AddListener(SpawnInZone1And2And3);
    }

    private void SpawnInZone1(int count)
    {
        List<EnemySpawner> spawners = new List<EnemySpawner>();
        foreach (EnemySpawner spawner in zone1Spawners)
            spawners.Add(spawner);
        Spawn(spawners, count);
    }

    private void SpawnInZone1And2(int count)
    {
        List<EnemySpawner> spawners = new List<EnemySpawner>();
        foreach (EnemySpawner spawner in zone1Spawners)
            spawners.Add(spawner);
        foreach (EnemySpawner spawner in zone2Spawners)
            spawners.Add(spawner);
        Spawn(spawners, count);
    }

    private void SpawnInZone1And2And3(int count)
    {
        List<EnemySpawner> spawners = new List<EnemySpawner>();
        foreach (EnemySpawner spawner in zone1Spawners)
            spawners.Add(spawner);
        foreach (EnemySpawner spawner in zone2Spawners)
            spawners.Add(spawner);
        foreach (EnemySpawner spawner in zone3Spawners)
            spawners.Add(spawner);
        Spawn(spawners, count);
    }

    private void Spawn(List<EnemySpawner> spawners, int count)
    {
        Shuffle(spawners);

        for (int i = 0; i < Mathf.Min(count, spawners.Count); i++)
        {
            int type = Random.Range(0, 2);
            if (type == 0)
                spawners[i].SpawnSkeletonMissingArm();
            else
                spawners[i].SpawnSkeletonMage();
        }
    }

    private void Shuffle(List<EnemySpawner> list)
    {
        System.Random rng = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            EnemySpawner value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public class SpawnEvent : UnityEvent<int> { }