using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Vector3 playerPosition;

    private void Start()
    {
        loadedMaze = GetComponent<MazeChunkLoader>();

        InvokeRepeating(nameof(SpawnEnemies), 3f, 5f);

        GameEvents_SinglePlayer.current.OnPlayerPositionChanged += OnPlayerPositionChanged;
        GameEvents_SinglePlayer.current.OnEnemyDeath += this.OnEnemyDeath;
    }
    private void SpawnEnemies()
    {
        List<MazeNodeNetwork.MazeNode> loadedNodes = loadedMaze.SpawnedNodes;
        List<MazeNodeNetwork.MazeNode> spawnNodes = new();

        foreach (MazeNodeNetwork.MazeNode node in loadedNodes)
        {
            if (Vector3.Distance(playerPosition ,node._worldLocation) > _distanceThreshHold)
            {
                spawnNodes.Add(node);
            }
        }

        if (true)
        {
            int justSpawnedCount = 0;
            while (justSpawnedCount <= _amountOfEnemiesThatCanSpawnAtOneTime && spawnNodes.Count > 0 && _enemyCount < _maxAmountOfEnemiesInGame)
            {
                Vector3 spawnLocation = spawnNodes[Random.Range(0, spawnNodes.Count)]._worldLocation;
                spawnLocation.y += 4;
                Instantiate(_enemyPrefab, enemyContainer);
                _enemyPrefab.transform.position = spawnLocation; 
                justSpawnedCount++;
                _enemyCount++;
            }
        }

    }

    private void OnPlayerPositionChanged(Vector3 playerPosition)
    {

        this.playerPosition = playerPosition;

    }

    private void OnEnemyDeath(int enemyId)
    {
        _enemyCount--;
    }


    private void OnDestroy()
    {
        GameEvents_SinglePlayer.current.OnPlayerPositionChanged -= OnPlayerPositionChanged;
        GameEvents_SinglePlayer.current.OnEnemyDeath -= this.OnEnemyDeath;
    }
}
