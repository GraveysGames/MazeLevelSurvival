using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEvents : MonoBehaviour
{

    public Canvas canvas;

    public static MazeEvents Singleton;

    //[SerializeField] GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        Singleton = this;
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


    public event Action<int, Vector3> OnPlayerPositionChanged;
    public void PlayerPositionChangedTrigger(int PlayerId, Vector3 playerPosition)
    {
        OnPlayerPositionChanged?.Invoke(PlayerId, playerPosition);
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

    public event Action<Vector3> OnTeleportPlayer;

    public void TeleportPlayerTrigger(Vector3 newPosition)
    {
        OnTeleportPlayer?.Invoke(newPosition);
    }

    public event Action<int> OnEnemyDeath;
    
    public void EnemyDeathTrigger(int enemyId)
    {
        OnEnemyDeath?.Invoke(enemyId);
    }

}
