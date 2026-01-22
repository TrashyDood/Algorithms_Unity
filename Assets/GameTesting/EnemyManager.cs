using System;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    EnemyPool _enemyPool;
    [SerializeField]
    Transform _spawnCenter;
    [SerializeField]
    EnemySpawnConfig _config;

    float _spawnIntervalTimer;
    bool _isSpawning;

    public static EnemyManager Instance { get; private set; }
    public EnemySpawnConfig Config => _config;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);

        Instance = this;
    }

    private void FixedUpdate()
    {
        if (_spawnIntervalTimer <= 0 && !_isSpawning)
        {
            _spawnIntervalTimer = UnityEngine.Random.Range(_config.minInterval, _config.maxInterval);
            StartCoroutine(
                SpawnEnemies(
                    UnityEngine.Random.Range(_config.minDuration, _config.maxDuration),
                    (int)UnityEngine.Random.Range(_config.minCount, _config.maxCount)));
        }
        else
            _spawnIntervalTimer -= Time.fixedDeltaTime;
    }

    Vector3 DeterminePosition() =>
        Utils.RandomCircleEdge(_config.spawnRadius) + _spawnCenter.position;

    Enemy SpawnEnemy(Vector3 position)
    {
        Enemy enemy = _enemyPool.FetchObject();
        enemy.transform.position = position;

        return enemy;
    }

    IEnumerator SpawnEnemies(float duration, int count)
    {
        _isSpawning = true;
        float interval = duration / count;

        for (int i = 0; i < count; i++)
        {
            SpawnEnemy(DeterminePosition());
            yield return new WaitForSeconds(interval);
        }

        _isSpawning = false;
    }
}

[Serializable]
public class EnemyPool : ComponentPool<Enemy>
{

}

[Serializable]
public class EnemySpawnConfig
{
    public float minInterval = 5,
        maxInterval = 8,
        minCount = 1,
        maxCount = 3,
        minDuration = 2.5f,
        maxDuration = 5,
        spawnRadius = 10;
}