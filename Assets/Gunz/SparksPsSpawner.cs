using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SparksPsSpawner : MonoBehaviour
{
    public static SpawnSparksParticlesEvent OnSpawnSparksParticles = new SpawnSparksParticlesEvent();

    [SerializeField] private ParticleSystem sparksPsPrefab;
    [SerializeField] private int maxParticleSystems = 10;

    private Queue<ParticleSystem> sparks = new Queue<ParticleSystem>();

    private void Awake()
    {
        OnSpawnSparksParticles.AddListener(SpawnSparksParticles);

        for (int i = 0; i < maxParticleSystems; i++)
        {
            ParticleSystem ps = Instantiate(sparksPsPrefab, transform);
            sparks.Enqueue(ps);
        }
    }

    private void SpawnSparksParticles(Vector3 position, Vector3 normal)
    {
        ParticleSystem ps = sparks.Dequeue();

        ps.Clear();
        ps.transform.position = position;
        ps.transform.LookAt(position + normal);
        ps.Play();

        sparks.Enqueue(ps);
    }
}

public class SpawnSparksParticlesEvent : UnityEvent<Vector3, Vector3> { }