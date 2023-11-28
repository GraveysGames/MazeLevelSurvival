using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The maze controller will handle all of the broad pipelining of the maze
/// </summary>
public class MazeManager : MonoBehaviour
{

    [SerializeField] GameObject mazePrefab;
    private GameObject currentMaze;

    [SerializeField] private int width, height;

    private Vector3 startingPosition;

    public int Round { get; private set; }

    private bool playerInstantiated;

    // Start is called before the first frame update
    void Start()
    {
        playerInstantiated = false;
        Round = 0;
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

        if (playerInstantiated == false)
        {
            GameEvents_SinglePlayer.current.SpawnPlayer(startingPosition);
            playerInstantiated = true;
        }
        else
        {
            GameEvents_SinglePlayer.current.TeleportPlayerTrigger(0, startingPosition);
        }

    }


    public void BuildNewMaze()
    {
        Round++;
        if (GameManager.current != null)
        {
            height = GameManager.Height;
            width = GameManager.Width;
        }

        if (currentMaze != null)
        {
            Destroy(currentMaze);
        }

        BuildMaze(Vector3.zero, (width + (Round * 5), height + (Round * 5)));

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            BuildNewMaze();
        }
    }
}
