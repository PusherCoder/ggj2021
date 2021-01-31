using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SparksPsSpawner : MonoBehaviour
{
    public static SpawnSparksParticlesEvent OnSpawnSparksParticles = new SpawnSparksParticlesEvent();
    public static SpawnExplosionParticlesEvent OnSpawnExplosion = new SpawnExplosionParticlesEvent();

    [SerializeField] private ParticleSystem sparksPsPrefab;
    [SerializeField] private int maxParticleSystems = 10;

    [SerializeField] private ParticleSystem explosionPrefab;
    [SerializeField] private int maxExplosions = 10;

    private Queue<ParticleSystem> sparks = new Queue<ParticleSystem>();
    private Queue<ParticleSystem> explosions = new Queue<ParticleSystem>();

    private void Awake()
    {
        OnSpawnSparksParticles.AddListener(SpawnSparksParticles);
        OnSpawnExplosion.AddListener(SpawnExplosion);

        for (int i = 0; i < maxParticleSystems; i++)
        {
            ParticleSystem ps = Instantiate(sparksPsPrefab, transform);
            sparks.Enqueue(ps);
        }

        for (int i = 0; i < maxExplosions; i++)
        {
            ParticleSystem ps = Instantiate(explosionPrefab, transform);
            explosions.Enqueue(ps);
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

    private void SpawnExplosion(Vector3 position)
    {
        ParticleSystem ps = explosions.Dequeue();

        ps.Clear();
        ps.transform.position = position;
        ps.gameObject.GetComponent<AudioSource>().Play();
        ps.Play();

        explosions.Enqueue(ps);
    }
}

public class SpawnSparksParticlesEvent : UnityEvent<Vector3, Vector3> { }
public class SpawnExplosionParticlesEvent : UnityEvent<Vector3> { }