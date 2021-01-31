using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Zone SpawnerZone; 

    [SerializeField] private SkeletonMissingArm skeletonMissingArmPrefab;
    [SerializeField] private SkeletonMage skeletonMagePrefab;

    public void SpawnSkeletonMissingArm()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1f, NavMesh.AllAreas))
        {
            SkeletonMissingArm skeletonMissingArm = Instantiate(skeletonMissingArmPrefab);
            skeletonMissingArm.GetComponent<NavMeshAgent>().Warp(hit.position);
            Debug.Log($"Spawning missing arm skeleton at {hit.position} {transform.name}");
        }
        else
        {
            Debug.LogError($"Spawner {transform.name} failed");
        }
    }

    public void SpawnSkeletonMage()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1f, NavMesh.AllAreas))
        {
            SkeletonMage skeletonMage = Instantiate(skeletonMagePrefab);
            skeletonMage.GetComponent<NavMeshAgent>().Warp(hit.position);
            Debug.Log($"Spawning skeleton mage at {hit.position} {transform.name}");
        }
        else
        {
            Debug.LogError($"Spawner {transform.name} failed");
        }
    }
}

public enum Zone { Zone1, Zone2, Zone3 }