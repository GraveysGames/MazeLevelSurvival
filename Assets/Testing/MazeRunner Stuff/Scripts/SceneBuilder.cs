using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SceneBuilder : MonoBehaviour
{

    // Start is called before the first frame update

    [SerializeField] GameObject wall;
    [SerializeField] GameObject path;
    [SerializeField] GameObject pathLight;

    [SerializeField] Transform contentContainer;

    [SerializeField] GameObject player;

    [SerializeField] GameObject arrowEnd;
    [SerializeField] GameObject arrowStart;

    [SerializeField] GameObject winCollider;

    //public int[,] maze;
    private GameObject fTile;

    private Vector3 playerStartPos = new Vector3(10,10,10);
    private Vector3 playerEndPos = new Vector3(0,0,0);

    private int[] start = new int[2];
    private int[] end = new int[2];

    private int width = 10;
    private int height = 10;
    private readonly int lightInterval = 5;

    private GameObject[,] mazeTiles;

    private bool[,] Maze { get; set; }

    private void Start()
    {

        if (GameManager.Width > 2)
        {
            width = GameManager.Width;
        }
        if (GameManager.Height > 2)
        {
            height = GameManager.Height;
        }

        Maze = gameObject.AddComponent<GenerateMaze>().GetMaze(width, height);

        mazeTiles = new GameObject[width, height];

        //maze = myMaze.MazeGrid;



        findStartEnd();

        buildMaze();

        OnMazeBuilt();

        //OnPlayerPositionAvailable();

        GameEvents.current.OnPlayerReadyForPositionTrigger += OnPlayerPositionAvailable;
        //player.transform.position = playerStartPos;
    }


 

    private void Update()
    {





    }

    private void OnMazeBuilt()
    {
        GameEvents.current.MazeBuiltTrigger(mazeTiles);
    }

    private void OnPlayerPositionAvailable(int playerID)
    {
        GameEvents.current.PlayerPositionAvailableTrigger(playerID, playerStartPos);
    }

    public Vector3 PlayerStartPosition { get => playerStartPos; }

    //gets the ring inside the outer wall around the maze
    //does not check if its a path or wall
    private List<int[]> fillEdgePaths(List<int[]> edgePaths)
    {

        //grab them so we calculate them only once 
        int lastK = (Maze.GetLength(1) - 2);
        int lastI = (Maze.GetLength(0) - 2);

        //Dont need the first or the last because its the outer maze wall

        //upper line and lower line
        for (int i = 1; i < lastI; i++)
        {
            if (Maze[i,1])
            {
                edgePaths.Add(new int[] { i, 1 });
            }
            if (Maze[i,lastK])
            {
                edgePaths.Add(new int[] { i, lastK });
            }

        }

        //both side lanes
        for (int k = 1; k < lastK; k++)
        {
            if (Maze[1,k])
            {
                edgePaths.Add(new int[] { 1, k });
            }
            if (Maze[lastI, k])
            {
                edgePaths.Add(new int[] { lastI, k });
            }
            
            
        }

        return edgePaths;

    }

    private void findStartEnd()
    {

        List<int[]> edgePaths = new List<int[]>();

        //get the edge tiles
        edgePaths = fillEdgePaths(edgePaths);

        int randomChoice = Random.Range(0, edgePaths.Count);

        int[] choosenLocation = edgePaths[randomChoice];

        start = new int[] { choosenLocation[0], choosenLocation[1] };

        playerStartPos = getNextGridPos(choosenLocation[0], choosenLocation[1]);
        playerStartPos[1] = 2;

        edgePaths.RemoveAt(randomChoice);

        randomChoice = Random.Range(0, edgePaths.Count);

        choosenLocation = edgePaths[randomChoice];

        end = new int[] { choosenLocation[0], choosenLocation[1] };

        playerEndPos = getNextGridPos(end[0], end[1]);

    }



    private void buildMaze()
    {
        //tileBoard = generateMaze.getMazeLayout();


        int pathCount = 0;

        for (int i = 0; i < Maze.GetLength(0); i++)
        {
            for (int k = 0; k < Maze.GetLength(1); k++)
            {
                if (Maze[i,k])
                {

                    

                    if (pathCount == lightInterval)
                    {
                        fTile = Instantiate(pathLight) as GameObject;
                        pathCount = 0;
                    }
                    else
                    {
                        fTile = Instantiate(path) as GameObject;
                        pathCount++;
                    }

                    if ((start[0] == i) && (start[1] == k))
                    {
                        GameObject arrow = Instantiate(arrowStart) as GameObject;
                        
                        Vector3 arrowPosition = getNextGridPos(i, k);

                        arrowPosition[1] = 5;

                        arrow.transform.position = arrowPosition;
                    }
                    else if ((end[0] == i) && (end[1] == k))
                    {
                        GameObject arrow = Instantiate(arrowEnd) as GameObject;
                        GameObject winBox = Instantiate(winCollider) as GameObject;

                        Vector3 arrowPosition = getNextGridPos(i, k);
                        Vector3 winBoxPosition = arrowPosition;

                        arrowPosition[1] = 5;

                        winBox.transform.position = winBoxPosition;
                        arrow.transform.position = arrowPosition;
                    }
                    
                }
                else
                {
                    fTile = Instantiate(wall) as GameObject;
                }
                
                fTile.transform.position = getNextGridPos(i, k);
                fTile.transform.SetParent(contentContainer);
            }
        }
    }

    Vector3 getNextGridPos(int x, int z)
    {
        const int xIncrement = 10;
        const int zIncrement = 10;

        Vector3 gridPos = new Vector3();

        gridPos.x += (x*xIncrement) + 5;
        gridPos.z += (z*zIncrement) + 5;


        return gridPos;
    }



}
