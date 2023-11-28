using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents current;

    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    public event Action<GameObject[,]> OnMazeBuiltTrigger;
    public void MazeBuiltTrigger(GameObject[,] maze)
    {
        OnMazeBuiltTrigger?.Invoke(maze);

    }

    public event Action<int, Vector3> OnPlayerPositionAvailableTrigger;
    public void PlayerPositionAvailableTrigger(int id, Vector3 playerPos)
    {
        OnPlayerPositionAvailableTrigger?.Invoke(id, playerPos);
    }

    public event Action<int> OnPlayerReadyForPositionTrigger;
    public void PlayerReadyForPositionTrigger(int id)
    {
        OnPlayerReadyForPositionTrigger?.Invoke(id); 
    }

    //triggers when the game has been won
    public event Action OnWinTrigger;
    public void WinTrigger()
    {
        OnWinTrigger?.Invoke();
    }


    public event Action<int, Vector3> OnPlayerPositionTrigger;
    public void PlayerPositionTrigger(int playerID, Vector3 playerPosition)
    {
        OnPlayerPositionTrigger?.Invoke(playerID, playerPosition);
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

    #region Seed Related
    public event Action<int> OnSendSeedTrigger;

    public void SendSeedTrigger(int mapSeed)
    {
        OnSendSeedTrigger?.Invoke(mapSeed);
    }

    public event Action OnReadyToRecieveSeedTrigger;

    public void ReadyToRecieveSeedTrigger()
    {
        OnReadyToRecieveSeedTrigger?.Invoke();
    }

    #endregion
}
