using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Zone SpawnerZone; 

    [SerializeField] private GameObject visual;
    [SerializeField] private SkeletonMissingArm skeletonMissingArmPrefab;
    [SerializeField] private SkeletonMage skeletonMagePrefab;

    private void Awake()
    {
        visual.SetActive(false);
    }

    public void SpawnSkeletonMissingArm()
    {
        SkeletonMissingArm skeletonMissingArm = Instantiate(skeletonMissingArmPrefab);
        skeletonMissingArm.transform.position = transform.position;
    }

    public void SpawnSkeletonMage()
    {
        SkeletonMage skeletonMage = Instantiate(skeletonMagePrefab);
        skeletonMage.transform.position = transform.position;
    }
}

public enum Zone { Zone1, Zone2, Zone3 }