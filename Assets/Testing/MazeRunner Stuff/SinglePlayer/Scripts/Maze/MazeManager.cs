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

    private NetworkVariable<int> nv_round = new NetworkVariable<int>();

    // Start is called before the first frame update
    void Start()
    {
        if (IsHost)
        {
            nv_round.Value = 0;
        }
        
        BuildNewMaze();
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

        currentMaze.GetComponent<FillMaze>().BuildMaze(mazeLocationCordinants, mazeSize);

        startingPosition = currentMaze.GetComponent<MazeStartAndEnd>().FindStartAndEnd(this);


        MazeEvents.Singleton.TeleportPlayerTrigger(startingPosition);
    }


    public void BuildNewMaze()
    {
        if (IsHost)
        {
            nv_round.Value++;
        }
        if (GameManager.current != null)
        {
            height = GameManager.Height;
            width = GameManager.Width;
        }

        if (currentMaze != null)
        {
            Destroy(currentMaze);
        }

        BuildMaze(Vector3.zero, (width + (nv_round.Value * 5), height + (nv_round.Value * 5)));

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            BuildNewMaze();
        }
    }
}
