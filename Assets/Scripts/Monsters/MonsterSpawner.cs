using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs; // Массив префабов монстров
    public int numberOfMonsters = 3;

    public float spawnRadius = 10f;

    private void Start()
    {
        SpawnMonsters();
    }

    private void SpawnMonsters()
    {
        List<Vector3> spawnPositions = GenerateRandomSpawnPositions(numberOfMonsters);

        for (int i = 0; i < numberOfMonsters; i++)
        {
            GameObject monsterPrefab = GetRandomMonsterPrefab();
            Vector3 spawnPosition = spawnPositions[i];
            Quaternion spawnRotation = Quaternion.identity;

            GameObject monster = Instantiate(monsterPrefab, spawnPosition, spawnRotation);
            // Здесь вы можете настроить дополнительные параметры монстра, если необходимо
        }
    }

    private List<Vector3> GenerateRandomSpawnPositions(int count)
    {
        List<Vector3> spawnPositions = new List<Vector3>();

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            spawnPositions.Add(spawnPosition);
        }

        return spawnPositions;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += transform.position;
        randomDirection.y = Random.Range(0f, 10f); // Полностью рандомная позиция по высоте
        return randomDirection;
    }

    private GameObject GetRandomMonsterPrefab()
    {
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        return monsterPrefabs[randomIndex];
    }
}
