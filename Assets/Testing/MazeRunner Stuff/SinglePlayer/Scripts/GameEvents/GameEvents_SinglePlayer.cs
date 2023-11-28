using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents_SinglePlayer : MonoBehaviour
{

    public Canvas canvas;

    public static GameEvents_SinglePlayer current;

    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    public void SpawnPlayer(Vector3 spawnPosition)
    {
       Instantiate(player, spawnPosition, new Quaternion(0,0,0,0));
    }

    public event Action OnUpdateCharicterHealth;

    public void UpdateCharicterHealthTrigger()
    {
        OnUpdateCharicterHealth?.Invoke();
    }

    //triggers when the game has been won
    public event Action OnWinTrigger;
    public void WinTrigger()
    {
        OnWinTrigger?.Invoke();
    }


    public event Action<Vector3> OnPlayerPositionChanged;
    public void PlayerPositionChangedTrigger( Vector3 playerPosition)
    {
        OnPlayerPositionChanged?.Invoke(playerPosition);
    }

    public event Action<Vector3> OnTileDestroyedTrigger;

    public void TileDestroyedTrigger(Vector3 tilePosition)
    {
        OnTileDestroyedTrigger?.Invoke(tilePosition);
    }

    public event Action<Dictionary<Vector3, bool>> OnDestroyTileTrigger;

    public void TileUpdatedTrigger(Dictionary<Vector3, bool> tilesToDestroy)
    {
        OnDestroyTileTrigger?.Invoke(tilesToDestroy);
    }


    public event Action<int> OnTileExtendDistruction;

    public void TileExtendDistructionTrigger(int tileId)
    {
        OnTileExtendDistruction?.Invoke(tileId);
    }

    public event Action<int, Vector3> OnTeleportPlayer;

    public void TeleportPlayerTrigger(int playerId, Vector3 newPosition)
    {
        OnTeleportPlayer?.Invoke(playerId, newPosition);
    }

    public event Action<int> OnEnemyDeath;
    
    public void EnemyDeathTrigger(int enemyId)
    {
        OnEnemyDeath?.Invoke(enemyId);
    }

}
