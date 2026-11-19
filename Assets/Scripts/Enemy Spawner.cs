using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int minEnemies;
    public int maxEnemies;

    private void Start()
    {
        int count = Random.Range(minEnemies, maxEnemies + 1);
        count = Mathf.Min(count, spawnPoints.Length);

        // Makes a list with the points so they can be removed so two enemies dont spawn in one point
        List<Transform> available = new List<Transform>(spawnPoints);

        for (int i = 0; i < count; i++)
        {
            int pick = Random.Range(0, available.Count);
            Instantiate(enemyPrefab, available[pick].position, Quaternion.identity);
            available.RemoveAt(pick);   
        }
    }


}
