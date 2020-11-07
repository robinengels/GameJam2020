using System.Collections;
using UnityEngine;
using Pooling;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float ySpawnPos;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int minScore;
    [SerializeField] private float minSpawnTimeInterval;
    [SerializeField] private float maxSpawnTimeInterval;

    private Camera _mainCamera;
    private Transform _mainCameraTransform;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _mainCameraTransform = _mainCamera.transform;
        StartCoroutine(SpawnEnemy());
    }
    
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return null;
            if (GameManager.Instance.ScoreManager.Score < minScore) continue;
            var interval = Random.Range(minSpawnTimeInterval, maxSpawnTimeInterval + 1);
            yield return new WaitForSeconds(interval);
            var xSpawnPos = _mainCameraTransform.position.x + (_mainCamera.orthographicSize * 3f);
            var spawnPoint = new Vector3(xSpawnPos, ySpawnPos, 0f);
            if (enemyPrefab.TryAcquire(out var enemy))
            {
                enemy.transform.position = spawnPoint;
            }
        }
    }
}
