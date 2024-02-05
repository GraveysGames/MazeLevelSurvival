using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


/// <summary>
/// The maze controller will handle all of the broad pipelining of the maze
/// </summary>
public class MazeManager : NetworkBehaviour
{

    [SerializeField] GameObject mazePrefab;
    private GameObject currentMaze;

    [SerializeField] private int width, height;

    private Vector3 startingPosition;

    public NetworkVariable<int> nv_seed = new NetworkVariable<int>();

    public NetworkVariable<int> nv_round = new NetworkVariable<int>();

    private int _round = 0;

    private int _seed = 0;


    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {

        if (IsHost)
        {
            _round = 0;
            HostBuildNewMaze();
        }
        else
        {
            ClientBuildNewMaze(nv_seed.Value, nv_round.Value);
        }

        MazeEvents.Singleton.OnMazePortalEnter += GoneThoughPortal;
    }


    public void BuildMaze(Vector3 mazeLocationCordinants, (int, int) mazeSize)
    {
        currentMaze = Instantiate(mazePrefab);

        if (GameManager.current != null)
        {
            if (!GameManager.IsSurvival)
            {
                Destroy(currentMaze.GetComponent<MazeEnemySpawnController>());
            }
        }

        if (IsHost)
        {
            currentMaze.GetComponent<FillMaze>().BuildMaze(mazeLocationCordinants, mazeSize);
            _seed = currentMaze.GetComponent<FillMaze>().Seed;
            Debug.Log("Seed: " + _seed);

            nv_seed.Value = _seed;
            nv_round.Value = _round;
        }
        else
        {
            currentMaze.GetComponent<FillMaze>().BuildMaze(_seed,mazeLocationCordinants, mazeSize);
            Destroy(currentMaze.GetComponent<MazeEnemySpawnController>());
            Debug.Log("Seed: " + _seed);
        }

        startingPosition = currentMaze.GetComponent<MazeStartAndEnd>().FindStartAndEnd();

        MazeEvents.Singleton.OnPlayerPositionChanged += PlayerReadyToBeTeleported;

        MazeEvents.Singleton.TeleportPlayerTrigger(startingPosition);
    }

    private void PlayerReadyToBeTeleported(int playerId, Vector3 posistion)
    {
        MazeEvents.Singleton.TeleportPlayerTrigger(startingPosition);
        MazeEvents.Singleton.OnPlayerPositionChanged -= PlayerReadyToBeTeleported;
    }

    public void HostBuildNewMaze()
    {
        if (!IsHost)
        {
            return;
        }

        _round++;

        if (GameManager.current != null)
        {
            height = GameManager.Height;
            width = GameManager.Width;
        }

        if (currentMaze != null)
        {
            Destroy(currentMaze);
        }

        BuildMaze(Vector3.zero, (width + (_round * 5), height + (_round * 5)));
    }

    public void ClientBuildNewMaze(int seed, int round)
    {
        if (IsHost)
        {
            return;
        }

        this._seed = seed;
        this._round = round;

        if (GameManager.current != null)
        {
            height = GameManager.Height;
            width = GameManager.Width;
        }

        if (currentMaze != null)
        {
            Destroy(currentMaze);
        }

        BuildMaze(Vector3.zero, (width + (_round * 5), height + (_round * 5)));

    }

    private void GoneThoughPortal()
    {
        if (IsHost)
        {
            NextStageClientRpc(_seed, _round);
        }
        else
        {
            NextStageServerRpc();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            MazeEvents.Singleton.MazePortalEnterTrigger();
        }
    }

    [ClientRpc]
    private void NextStageClientRpc(int seed, int round)
    {
        ClientBuildNewMaze(seed, round);
    }

    [ServerRpc (RequireOwnership = false)]
    private void NextStageServerRpc()
    {
        HostBuildNewMaze();
        NextStageClientRpc(_seed, _round);
    }
}
