using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.Netcode;
/*
public class GameData : NetworkBehaviour
{

    [SerializeField] NetworkVariable<int> mapSeed;

    private int MapSeed { get => mapSeed.Value; set => mapSeed.Value = value; }

    void Start()
    {

        if (IsHost)
        {

            GameEvents.current.OnSendSeedTrigger += SetMapSeed;

        }
        else
        {
            GameEvents.current.OnReadyToRecieveSeedTrigger += SendMapSeed;
        }

    }


    private void SetMapSeed(int seed)
    {

        MapSeed = seed;

        GameEvents.current.OnSendSeedTrigger -= SetMapSeed;
    }

    private void SendMapSeed()
    {

        GameEvents.current.SendSeedTrigger(MapSeed);

        if (MapSeed != 0)
        {
            GameEvents.current.OnReadyToRecieveSeedTrigger += SendMapSeed;
        }

    }

}
*/