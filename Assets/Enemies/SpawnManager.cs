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
        Spawn(spawners, count, 1);
    }

    private void SpawnInZone1And2(int count)
    {
        List<EnemySpawner> spawners = new List<EnemySpawner>();
        foreach (EnemySpawner spawner in zone1Spawners)
            spawners.Add(spawner);
        foreach (EnemySpawner spawner in zone2Spawners)
            spawners.Add(spawner);
        Spawn(spawners, count, 2);
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
        Spawn(spawners, count, 3);
    }

    private void Spawn(List<EnemySpawner> spawners, int count, int maxCount)
    {
        for (int i = AllEnemies.Enemies.Count - 1; i >= 0; i--)
            Destroy(AllEnemies.Enemies[i].gameObject);

        AllEnemies.Enemies.Clear();

        Shuffle(spawners);

        for (int i = 0; i < Mathf.Min(count, spawners.Count); i++)
        {
            int type = Random.Range(0, 7);
            if (type == 0)
                spawners[i].SpawnSkeletonMissingArm();
            else if (type == 1)
                spawners[i].SpawnSkeletonMage();
            else if (type == 2)
            {
                spawners[i].SpawnSkeletonMissingArm();
                if (maxCount > 1) spawners[i].SpawnSkeletonMage();
            }
            else if (type == 3)
            {
                spawners[i].SpawnSkeletonMissingArm();
                if (maxCount > 1) spawners[i].SpawnSkeletonMissingArm();
            }
            else if (type == 4)
            {
                spawners[i].SpawnSkeletonMage();
                if (maxCount > 1) spawners[i].SpawnSkeletonMage();
            }
            else if (type == 5)
            {
                spawners[i].SpawnSkeletonMage();
                if (maxCount > 1) spawners[i].SpawnSkeletonMage();
                if (maxCount > 2) spawners[i].SpawnSkeletonMissingArm();

            }
            else if (type == 6)
            {
                spawners[i].SpawnSkeletonMage();
                if (maxCount > 1) spawners[i].SpawnSkeletonMissingArm();
                if (maxCount > 2) spawners[i].SpawnSkeletonMissingArm();
            }
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