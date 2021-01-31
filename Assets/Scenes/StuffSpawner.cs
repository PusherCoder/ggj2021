using UnityEngine;
using UnityEngine.AI;

public class StuffSpawner : MonoBehaviour
{
    public Vector2 Extents;
    public int Amount = 100;

    public SpawnableGreeble[] StuffPrefabs;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Extents.x, 0f, Extents.y));   
    }

    public void SpawnStuff()
    {
        for (int i = 0; i < Amount; i++)
        {
            Vector3 position = transform.position;
            position.x += Random.Range(-Extents.x, Extents.x);
            position.z += Random.Range(-Extents.y, Extents.y);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(position, out hit, 1f, NavMesh.AllAreas))
            {
                int index = Random.Range(0, StuffPrefabs.Length);
                GameObject thing = Instantiate(StuffPrefabs[index].Prefab, transform);
                thing.transform.position = hit.position + (Vector3.up * StuffPrefabs[index].YOffset);
                thing.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            }
        }
    }
}

[System.Serializable]
public class SpawnableGreeble
{
    public GameObject Prefab;
    public float YOffset;
}