using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MazeEnemySpawnController : MonoBehaviour
{

    [SerializeField] GameObject _enemyPrefab;
    //private List<MazeNodeNetwork.MazeNode> spawnNodes;

    List<GameObject> _allEnemies = new();

    private MazeChunkLoader loadedMaze;

    [SerializeField] private int _enemyCount = 0;
    [SerializeField] private int _maxAmountOfEnemiesInGame = 50;

    [SerializeField] private int _maxAmountOfEnemiesAroundPlayer = 10;
    [SerializeField] private int _distanceThreshHold = 10;

    [SerializeField] private int _amountOfEnemiesThatCanSpawnAtOneTime = 3;

    [SerializeField] private Transform enemyContainer;

    private Dictionary<int,Vector3> playersPositions;

    private void OnEnable()
    {
        loadedMaze = GetComponent<MazeChunkLoader>();

        InvokeRepeating(nameof(SpawnEnemies), 3f, 5f);

        MazeEvents.Singleton.OnPlayerPositionChanged += OnPlayerPositionChanged;
        MazeEvents.Singleton.OnEnemyDeath += this.OnEnemyDeath;

        playersPositions = new();
    }
    private void SpawnEnemies()
    {
        List<MazeNodeNetwork.MazeNode> loadedNodes = loadedMaze.SpawnedNodes;
        List<MazeNodeNetwork.MazeNode> spawnNodes = new();

        foreach (MazeNodeNetwork.MazeNode node in loadedNodes)
        {
            foreach (var player in playersPositions)
            {
                if (Vector3.Distance(player.Value, node.WorldLocation) > _distanceThreshHold)
                {
                    spawnNodes.Add(node);
                }
            }

        }

        if (true)
        {
            GameObject newEnemy;
            int justSpawnedCount = 0;
            while (justSpawnedCount <= _amountOfEnemiesThatCanSpawnAtOneTime && spawnNodes.Count > 0 && _enemyCount < _maxAmountOfEnemiesInGame)
            {
                Vector3 spawnLocation = spawnNodes[Random.Range(0, spawnNodes.Count)].WorldLocation;
                spawnLocation.y += 4;
                newEnemy = Instantiate(_enemyPrefab, spawnLocation, new Quaternion(0,0,0,0));
                //newEnemy.transform.position = spawnLocation;
                newEnemy.GetComponent<NetworkObject>().Spawn(true);
                justSpawnedCount++;
                _enemyCount++;
            }
        }

    }

    private void OnPlayerPositionChanged(int PlayerId, Vector3 playerPosition)
    {

        if (playersPositions.ContainsKey(PlayerId))
        {
            playersPositions[PlayerId] = playerPosition;
        }
        else
        {
            playersPositions.Add(PlayerId, playerPosition);
        }

    }

    private void OnEnemyDeath(int enemyId)
    {
        _enemyCount--;
    }


    private void OnDestroy()
    {
        MazeEvents.Singleton.OnPlayerPositionChanged -= OnPlayerPositionChanged;
        MazeEvents.Singleton.OnEnemyDeath -= this.OnEnemyDeath;
    }
}
