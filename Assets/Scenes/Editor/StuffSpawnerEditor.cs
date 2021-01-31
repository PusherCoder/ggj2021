using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StuffSpawner))]
public class StuffSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Spawn Stuff"))
        {
            StuffSpawner spawner = target as StuffSpawner;
            spawner.SpawnStuff();
        }
    }
}
