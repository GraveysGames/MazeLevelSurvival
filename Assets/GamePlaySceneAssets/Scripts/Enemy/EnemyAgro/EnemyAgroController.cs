using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgroController : MonoBehaviour
{

    Dictionary<GameObject, int> _playerAgroPool = new();

    public GameObject AgroedPlayer { private set; get; }

    public void Agro(GameObject agroedPlayer)
    {
        int agroAmount = 2;

        if (_playerAgroPool.Count > 0)
        {
            agroAmount = 0;
        }

        _playerAgroPool[agroedPlayer] = _playerAgroPool.TryGetValue(agroedPlayer, out int value) ? ++value : agroAmount;
        DetermineAgroedPlayer();
    }

    public void DeAgro(GameObject agroedPlayer)
    {
        if (_playerAgroPool.ContainsKey(agroedPlayer))
        {
            _playerAgroPool.Remove(agroedPlayer);
        }
        DetermineAgroedPlayer();
    }

    private void DetermineAgroedPlayer()
    {
        int agroScore = 0;
        GameObject playerToAgro = null;
        foreach (var player in _playerAgroPool)
        {
            if (player.Value > agroScore)
            {
                agroScore = player.Value;
                playerToAgro = player.Key;
            }
        }

        AgroedPlayer = playerToAgro;
        //Debug.Log("Agroed player: " + AgroedPlayer);
    }

}
