using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Calls the function to generate a maze
/// Creates the node network for the maze and fills the maze with certain tiles
/// 
/// </summary>
public class MazeBuilder_SinglePlayer : MonoBehaviour
{
    #region Constants

    //the length of one side of the square
    const int CHUNKSIZE = 20;


    #endregion



    //Tile prefabs
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject path;
    [SerializeField] private GameObject pathLight;

    //Tile container in heiarchy for the unity editor
    [SerializeField] private Transform contentContainer;

    [SerializeField] GameObject arrowEnd;
    [SerializeField] GameObject arrowStart;

    [SerializeField] GameObject winCollider;
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] private Vector3 playerStartPos = new(15, 4, 15);
    [SerializeField] private Vector3 playerEndPos = new(0, 0, 0);


    [SerializeField] int seed = -1;

    [SerializeField] int tendralSpawnReach = 1;

    private Vector3 playerPosition;

    private MazeNode[,] Maze;


    private List<MazeNode> spawnNodes;
    private int enemyCount = 0;
    private int maxAmountOfEnemies = 5;

    #region Start
    private void Start()
    {
        spawnNodes = new();
        //ready to recieve player ID and location

        seed = (int)DateTime.Now.Ticks;

        BuildMaze();

        GameEvents_SinglePlayer.current.SpawnPlayer(playerStartPos);

        
    }
    #endregion

    private void Update()
    {
        spawnNodes.Clear();
        //BuildMazeChunk(playerPosition);
        BuildMazeChunkTendrals(playerPosition);

        SpawnEnemies();
    }


    private void SpawnEnemies()
    {
        if (spawnNodes.Count < 1 || (enemyCount >= maxAmountOfEnemies))
        {
            return;
        }
        Vector3 spawnLocation = spawnNodes[UnityEngine.Random.Range(0,spawnNodes.Count-1)].GameLocation;
        spawnLocation.y += 5;
        Instantiate(enemyPrefab, spawnLocation, new Quaternion(0,0,0,0));
        enemyCount++;
    }

    public int BuildMaze()
    {
        int[] mazeSize = { 10,10 };

        bool[,] GeneratedMaze;
        GeneratedMaze = gameObject.AddComponent<GenerateMaze>().GetMaze(mazeSize[0], mazeSize[1], seed);

        Maze = BuildMazeNodes(GeneratedMaze, mazeSize[0], mazeSize[1]);

        FindStartToEnd();


        GameEvents_SinglePlayer.current.OnPlayerPositionChanged += OnPlayerPositionChanged;
        GameEvents_SinglePlayer.current.OnTileDestroyedTrigger += OnTileDestroyed;

        return seed;
    }

    private void OnPlayerPositionChanged(Vector3 playerPosition)
    {

        this.playerPosition = playerPosition;

    }

    private void OnTileDestroyed(Vector3 tilePosition)
    {
        int[] tileGridPosition = GetGridPositionFromGamePosition(tilePosition);

        Maze[tileGridPosition[0], tileGridPosition[1]].IsInstantiated = false;

    }


    private void BuildMazeChunkTendrals(Vector3 position)
    {

        int[] gridPos = GetGridPositionFromGamePosition(position);

        if (!(gridPos[0] > 0))
        {
            return;
        }

        if (!(gridPos[1] > 0))
        {
            return;
        }

        if (!((Maze.GetLength(1) - 1) > gridPos[1]))
        {
            return;
        }

        if (!((Maze.GetLength(0) - 1) > gridPos[0]))
        {
            return;
        }

        BuildTendrals(gridPos, 0, -1);


    }

    private void BuildTendrals(int[] gridLocation, int count, int direction)
    {
        
        if (count >= tendralSpawnReach)
        {
            return;
        }

        //direction north: 0 , east: 1, south: 2, west: 3,   None: -1

        if (!Maze[gridLocation[0], gridLocation[1]].IsInstantiated)
        {
            Maze[gridLocation[0], gridLocation[1]].InstantiatTile(contentContainer);
            BuildHedgesAroundPath(gridLocation);
            if (count < 13 && count > 8)
            {
                spawnNodes.Add(Maze[gridLocation[0], gridLocation[1]]);
            }
            
        }
        else
        {
            Maze[gridLocation[0], gridLocation[1]].ExtendTileDestruction();
            ExtendHedgesAroundPath(gridLocation);

            if (count < 13 && count > 8)
            {
                spawnNodes.Add(Maze[gridLocation[0], gridLocation[1]]);
            }

        }
        int newCount = count + 1;

        if ((direction != 2) && Maze[gridLocation[0], gridLocation[1] + 1].IsPath)
        {
            int[] newGridLocation = { gridLocation[0], gridLocation[1] + 1 };
            BuildTendrals(newGridLocation, newCount, 0);
        }

        if ((direction != 0) && Maze[gridLocation[0], gridLocation[1] - 1].IsPath)
        {
            int[] newGridLocation = { gridLocation[0], gridLocation[1] - 1 };
            BuildTendrals(newGridLocation, newCount, 2);
        }

        if ((direction != 3) && Maze[gridLocation[0] + 1, gridLocation[1]].IsPath)
        {
            int[] newGridLocation = { gridLocation[0] + 1, gridLocation[1] };
            BuildTendrals(newGridLocation, newCount, 1);
        }

        if ((direction != 1) && Maze[gridLocation[0] - 1, gridLocation[1]].IsPath)
        {
            int[] newGridLocation = { gridLocation[0] - 1, gridLocation[1] };
            BuildTendrals(newGridLocation, newCount, 3);
        }


    }

    private void BuildHedgesAroundPath(int[] gridLocation)
    {
        if (!Maze[gridLocation[0], gridLocation[1] + 1].IsInstantiated && !Maze[gridLocation[0], gridLocation[1] + 1].IsPath)
        {
            Maze[gridLocation[0], gridLocation[1] + 1].InstantiatTile(contentContainer);
        }

        if (!Maze[gridLocation[0], gridLocation[1] - 1].IsInstantiated && !Maze[gridLocation[0], gridLocation[1] - 1].IsPath)
        {
            Maze[gridLocation[0], gridLocation[1] - 1].InstantiatTile(contentContainer);
        }

        if (!Maze[gridLocation[0] + 1, gridLocation[1]].IsInstantiated && !Maze[gridLocation[0] + 1, gridLocation[1]].IsPath)
        {
            Maze[gridLocation[0] + 1, gridLocation[1]].InstantiatTile(contentContainer);
        }

        if (!Maze[gridLocation[0] - 1, gridLocation[1]].IsInstantiated && !Maze[gridLocation[0] - 1, gridLocation[1]].IsPath)
        {
            Maze[gridLocation[0] - 1, gridLocation[1]].InstantiatTile(contentContainer);
        }
    }
    private void ExtendHedgesAroundPath(int[] gridLocation)
    {
        if (Maze[gridLocation[0], gridLocation[1] + 1].IsInstantiated && !Maze[gridLocation[0], gridLocation[1] + 1].IsPath)
        {
            Maze[gridLocation[0], gridLocation[1] + 1].ExtendTileDestruction();
        }

        if (Maze[gridLocation[0], gridLocation[1] - 1].IsInstantiated && !Maze[gridLocation[0], gridLocation[1] - 1].IsPath)
        {
            Maze[gridLocation[0], gridLocation[1] - 1].ExtendTileDestruction();
        }

        if (Maze[gridLocation[0] + 1, gridLocation[1]].IsInstantiated && !Maze[gridLocation[0] + 1, gridLocation[1]].IsPath)
        {
            Maze[gridLocation[0] + 1, gridLocation[1]].ExtendTileDestruction();
        }

        if (Maze[gridLocation[0] - 1, gridLocation[1]].IsInstantiated && !Maze[gridLocation[0] - 1, gridLocation[1]].IsPath)
        {
            Maze[gridLocation[0] - 1, gridLocation[1]].ExtendTileDestruction();
        }
    }


    private void BuildMazeChunk(Vector3 position)
    {

        if (position.x < 10f || position.z < 10f)
        {
            //position = playerStartPos;
        }


        int[] chunkDimensions = DetermineChunkLocation(position);


        for (int i = chunkDimensions[0]; i < chunkDimensions[2]; i++)
        {
            for (int k = chunkDimensions[1]; k < chunkDimensions[3]; k++)
            {
                if (!Maze[i, k].IsInstantiated)
                {
                    Maze[i, k].InstantiatTile(contentContainer);
                }
            }
        }

    }

    /// <summary>
    /// Determines the area around the player to load objects; Square
    /// </summary>
    /// <param name="position">Vector3 positon of a player</param>
    /// <returns>int[4] - int[0]: Start X; int[1]: Start Z; int[2]: Width; int[3]: height;</returns>
    private int[] DetermineChunkLocation(Vector3 position)
    {
        const int chunkRadius = CHUNKSIZE / 2;

        int rowSize = chunkRadius * 2;
        int columnSize = chunkRadius * 2;

        int[] gridPos = GetGridPositionFromGamePosition(position);
        int[] buildStartGridPos = new int[2] { gridPos[0] - chunkRadius, gridPos[1] - chunkRadius };

        //checks if the radius goes out of the range of the grid
        if ((Maze.GetLength(0) - buildStartGridPos[0]) < rowSize)
        {
            rowSize = ((Maze.GetLength(0)) - buildStartGridPos[0]);
            //rowSize = Maze.GetLength(0);
        }

        if (((Maze.GetLength(1) - 1) - buildStartGridPos[1]) < columnSize)
        {
            columnSize = ((Maze.GetLength(1)) - buildStartGridPos[1]);
            //columnSize = Maze.GetLength(1);
        }

        if (buildStartGridPos[0] < 0)
        {
            buildStartGridPos[0] = 0;
        }

        if (buildStartGridPos[1] < 0)
        {
            buildStartGridPos[1] = 0;
        }

        if (Maze.GetLength(0) < CHUNKSIZE)
        {
            buildStartGridPos[0] = 0;
            rowSize = Maze.GetLength(0);
        }

        if (Maze.GetLength(1) < CHUNKSIZE)
        {
            buildStartGridPos[1] = 0;
            columnSize = Maze.GetLength(1);
        }

        return new int[] { buildStartGridPos[0], buildStartGridPos[1], (buildStartGridPos[0] + rowSize), (buildStartGridPos[1] + columnSize) };

    }


    #region BuildMazeNodeNetwork

    private class MazeNode
    {
        private GameObject tileType;
        private GameObject mazeTile;
        private int mazeTileId;
        private Vector3 gameLocation;
        private int[] gridPosition;

        private bool isInstantiated;
        private bool isPath;
        private bool isIntersection;
        private bool isEnd;

        //used for path nodes
        public static Dictionary<int[], MazeNode> pathNodes = new();

        //private static Dictionary<int[], MazeNode> endNodes = new();

        private List<MazeNode> nextPathNodes = new();

        public List<MazeNode> NextPathNodes { get => nextPathNodes; }

        public bool IsInstantiated { get => isInstantiated; set => isInstantiated = value; }
        public bool IsEnd { get => isEnd; }

        public bool IsPath { get => isPath; }
        public bool IsIntersection { get => isIntersection; set => isIntersection = value; }
        public Vector3 GameLocation { get => gameLocation; }
        public int[] GridLocation { get => gridPosition; }
        //public static Dictionary<int[], MazeNode> EndNodes { get => endNodes; }

        #region Constructors
        /// <summary>
        /// Wall tile constructor
        /// </summary>
        /// <param name="tileType">String: name of tile type</param>
        /// <param name="mazeTile">GameObject: the tile object</param>
        /// <param name="gameLocation">Vector3: location in game</param>
        /// <param name="gridPosition">int[]: location in the grid of objects</param>
        public MazeNode(GameObject tileType, Vector3 gameLocation, int[] gridPosition)
        {
            this.tileType = tileType;
            this.gameLocation = gameLocation;
            this.gridPosition = gridPosition;
            this.isPath = false;
            this.isInstantiated = false;
        }

        /// <summary>
        /// Path tile constructor
        /// </summary>
        /// <param name="tileType">String: name of tile type</param>
        /// <param name="mazeTile">GameObject: the tile object</param>
        /// <param name="gameLocation">Vector3: location in game</param>
        /// <param name="gridPosition">int[]: location in the grid of objects</param>
        /// <param name="MazeNodeNetwork">MazeNode[,]: the current maze node network</param>
        public MazeNode(GameObject tileType, Vector3 gameLocation, int[] gridPosition, MazeNode[,] MazeNetwork)
        {
            this.tileType = tileType;
            this.gameLocation = gameLocation;
            this.isPath = true;
            this.isInstantiated = false;

            //add to path dictionary and find next and previous nodes

            pathNodes.Add(gridPosition, this);

            if (gridPosition[0] > 1)
            {
                if (MazeNetwork[gridPosition[0] - 1, gridPosition[1]].isPath)
                {
                    //adds previous west node
                    this.nextPathNodes.Add(MazeNetwork[gridPosition[0] - 1, gridPosition[1]]);

                    //adds next east node of previous node
                    MazeNetwork[gridPosition[0] - 1, gridPosition[1]].AddNextNode(this);
                }
            }

            if (gridPosition[1] > 1)
            {
                if (MazeNetwork[gridPosition[0], gridPosition[1] - 1].isPath)
                {
                    //adds previous south node
                    this.nextPathNodes.Add(MazeNetwork[gridPosition[0], gridPosition[1] - 1]);

                    //adds next north node of previous node
                    MazeNetwork[gridPosition[0], gridPosition[1] - 1].AddNextNode(this);
                }
            }


        }

        /// <summary>
        /// Adds the next node
        /// </summary>
        /// <param name="nextNode">MaseNode: object of next node</param>
        /// <param name="direction">int: direction of next node; 0 for north, 1 for east</param>
        public void AddNextNode(MazeNode nextNode)
        {
            this.nextPathNodes.Add(nextNode);
        }

        /// <summary>
        /// Checks if this node is an intersection and marks it as true 
        /// </summary>
        public void CheckIntersection()
        {
            if (nextPathNodes.Count > 2)
            {
                isIntersection = true;
            }
            else if (nextPathNodes.Count == 1)
            {
                isEnd = true;
                //endNodes.Add(this.gridPosition, this);
            }
        }
        #endregion

        public void InstantiatTile(Transform ContentContainer)
        {
            this.mazeTile = Instantiate(tileType) as GameObject;
            this.mazeTile.transform.position = gameLocation;
            this.mazeTile.transform.SetParent(ContentContainer);
            this.mazeTileId = this.mazeTile.GetComponent<MazeTileController>().TileId;
            this.isInstantiated = true;

        }

        public void ExtendTileDestruction()
        {
            GameEvents_SinglePlayer.current.TileExtendDistructionTrigger(this.mazeTileId);
        }

    }
    private MazeNode[,] BuildMazeNodes(bool[,] GeneratedMaze, int width, int height)
    {

        MazeNode[,] MazeNetwork = new MazeNode[width, height];

        const int lightInterval = 5;
        int lightDistanceCount = 0;

        //iterate though the whole maze
        for (int i = 0; i < GeneratedMaze.GetLength(0); i++)
        {
            for (int k = 0; k < GeneratedMaze.GetLength(1); k++)
            {
                MazeNetwork[i, k] = TileSelection(i, k);
            }
        }

        CheckAllPathInterSections(MazeNetwork[1, 1], null);

        return MazeNetwork;

        MazeNode TileSelection(int i, int k)
        {

            GameObject selectedTile;

            Vector3 tilePosition = GetGamePositionFromGridPosition(i, k);

            if (GeneratedMaze[i, k])
            {

                if (lightDistanceCount == lightInterval)
                {
                    //Path Light
                    selectedTile = pathLight;
                    lightDistanceCount = 0;
                }
                else
                {
                    //Path
                    selectedTile = path;
                    lightDistanceCount++;
                }

                return new MazeNode(selectedTile, tilePosition, new int[] { i, k }, MazeNetwork);

            }
            else
            {
                //Wall
                selectedTile = wall;

                return new MazeNode(selectedTile, tilePosition, new int[] { i, k });

            }

        }


    }

    private void CheckAllPathInterSections(MazeNode currentPath, MazeNode previousNode)
    {

        currentPath.CheckIntersection();

        foreach (MazeNode nextPath in currentPath.NextPathNodes)
        {
            if (nextPath != previousNode)
            {
                CheckAllPathInterSections(nextPath, currentPath);
            }
        }

    }
    #endregion



    #region Finds Start and End of Maze Functions

    //Struct to help find the longest path in the maze
    private class LongestPathNode
    {

        //The start and end node for the currently set longest path
        public MazeNode startNode;
        public MazeNode endNode;
        //keeps track of the current path count go through forward iterations, and the longest found path going backwards
        public int currentPathCount;
        //the count of paths between the start and end node
        public int longestPathCount;

        //used to keep track if the currently longest path on the path of exiting iterations
        //if it is not then the current path has to be updated with the new add tiles inbetween it and the next intersection
        public bool pathBacktracking = false;


        //initializes the Node
        public LongestPathNode(MazeNode Start, MazeNode End, int CurrentPathCount, int LongestPathCount)
        {
            this.startNode = Start;
            this.endNode = End;
            this.currentPathCount = CurrentPathCount;
            this.longestPathCount = LongestPathCount;
        }

        public LongestPathNode(MazeNode Start, MazeNode End, int CurrentPathCount, int LongestPathCount, bool PathBackTracking)
        {
            this.startNode = Start;
            this.endNode = End;
            this.currentPathCount = CurrentPathCount;
            this.longestPathCount = LongestPathCount;
            this.pathBacktracking = PathBackTracking;
        }

        public LongestPathNode(LongestPathNode node)
        {
            this.startNode = node.startNode;
            this.endNode = node.endNode;
            this.currentPathCount = node.currentPathCount;
            this.longestPathCount = node.longestPathCount;
        }

    }

    /// <summary>
    /// finds the Start and End of the maze
    /// </summary>
    private void FindStartToEnd()
    {

        if ((Maze.GetLength(0) * Maze.GetLength(1)) > 10_001) //100X100 + 1
        {
            //Dictionary<int[], MazeNode> endNodes = MazeNode.EndNodes;

            List<MazeNode> possibleStartNodes = new();
            List<MazeNode> possibleEndNodes = new();

            if (Maze.GetLength(0) >= Maze.GetLength(1))
            {
                for (int i = 1; i < 2; i++)
                {
                    for (int k = 1; k < Maze.GetLength(1) - 2; k++)
                    {

                        if (Maze[i, k].IsEnd)
                        {
                            possibleStartNodes.Add(Maze[i, k]);
                        }

                        if (Maze[((Maze.GetLength(0) - 1) - i), k].IsEnd)
                        {
                            possibleEndNodes.Add(Maze[((Maze.GetLength(0) - 1) - i), k]);
                        }

                    }
                }
            }
            else
            {
                for (int k = 1; k < 2; k++)
                {
                    for (int i = 1; i < Maze.GetLength(0) - 2; i++)
                    {

                        if (Maze[i, k].IsEnd)
                        {
                            possibleStartNodes.Add(Maze[i, ((Maze.GetLength(0) - 1) - k)]);
                        }

                        if (Maze[i, ((Maze.GetLength(0) - 1) - k)].IsEnd)
                        {
                            possibleEndNodes.Add(Maze[i, ((Maze.GetLength(0) - 1) - k)]);
                        }


                    }
                }
            }

            int randomChoice = UnityEngine.Random.Range(0, possibleStartNodes.Count);

            MazeNode chosenStartNode = possibleStartNodes[randomChoice];

            randomChoice = UnityEngine.Random.Range(0, possibleEndNodes.Count);

            MazeNode chosenEndNode = possibleEndNodes[randomChoice];

            playerStartPos = chosenStartNode.GameLocation;
            playerStartPos.y += 4;
            playerEndPos = chosenEndNode.GameLocation;

        }
        else
        {
            MazeNode firstNode = Maze[1, 1];

            firstNode.IsIntersection = true;

            LongestPathNode node = new(firstNode, null, 0, 0);

            LongestPathNode longestPath = FindLongestPathInMaze(node, firstNode, Maze[0, 0]);

            playerStartPos = longestPath.startNode.GameLocation;
            playerStartPos.y += 4;
            playerEndPos = longestPath.endNode.GameLocation;

        }




        GameObject arrow = Instantiate(arrowStart) as GameObject;

        Vector3 arrowPosition = playerStartPos;

        arrowPosition[1] = 5;

        arrow.transform.position = arrowPosition;

        arrow = Instantiate(arrowEnd) as GameObject;
        GameObject winBox = Instantiate(winCollider) as GameObject;

        arrowPosition = playerEndPos;
        Vector3 winBoxPosition = arrowPosition;

        arrowPosition[1] = 5;

        winBox.transform.position = winBoxPosition;
        arrow.transform.position = arrowPosition;


    }

    private LongestPathNode FindLongestPathInMaze(LongestPathNode CurrentNode, MazeNode CurrentMazeNode, MazeNode LastMazeNode)
    {

        if (CurrentMazeNode.IsIntersection)
        {


            CurrentNode.endNode = CurrentMazeNode;
            CurrentNode.longestPathCount = CurrentNode.currentPathCount;
            //reset to look for a new branch
            LongestPathNode toNextNode = new(CurrentMazeNode, null, 1, 0);



            LongestPathNode longest = new(CurrentNode);
            LongestPathNode secondLongest = new(null, null, 0, 0);

            LongestPathNode previousLongestPath = CurrentNode;

            foreach (MazeNode nextNode in CurrentMazeNode.NextPathNodes)
            {

                if (nextNode != null && nextNode != LastMazeNode)
                {
                    LongestPathNode nodeReturned = FindLongestPathInMaze(toNextNode, nextNode, CurrentMazeNode);

                    if (nodeReturned.longestPathCount > previousLongestPath.longestPathCount)
                    {
                        previousLongestPath = nodeReturned;
                    }

                    if (nodeReturned.currentPathCount > longest.currentPathCount)
                    {
                        longest = nodeReturned;
                    }
                    else if (nodeReturned.currentPathCount > secondLongest.currentPathCount)
                    {
                        secondLongest = nodeReturned;
                    }
                }
            }


            if ((longest.currentPathCount + secondLongest.currentPathCount) < previousLongestPath.longestPathCount)
            {
                return previousLongestPath;
            }




            //because the intersection is in the middle of the two paths you have to meld them together
            //they either both start at the intersection or one ends at the intersection
            if (longest.startNode == CurrentMazeNode && secondLongest.startNode == CurrentMazeNode)
            {
                return new(longest.endNode, secondLongest.endNode, longest.currentPathCount, (longest.currentPathCount + secondLongest.currentPathCount));
            }
            else if (longest.startNode == CurrentMazeNode)
            {
                return new(longest.endNode, secondLongest.startNode, longest.currentPathCount, (longest.currentPathCount + secondLongest.currentPathCount), true);
            }
            else
            {
                return new(longest.startNode, secondLongest.endNode, longest.currentPathCount, (longest.currentPathCount + secondLongest.currentPathCount), true);
            }

        }
        else
        {

            if (CurrentMazeNode.IsEnd)
            {
                LongestPathNode endNode = new(CurrentNode.startNode, CurrentMazeNode, CurrentNode.currentPathCount, CurrentNode.currentPathCount);
                return endNode;
            }


            LongestPathNode straitNode = new(CurrentNode.startNode, null, CurrentNode.currentPathCount + 1, 0);
            foreach (MazeNode nextNode in CurrentMazeNode.NextPathNodes)
            {
                if (nextNode != null && nextNode != LastMazeNode)
                {
                    LongestPathNode longestFoundSoFar = FindLongestPathInMaze(straitNode, nextNode, CurrentMazeNode);

                    if (!longestFoundSoFar.pathBacktracking)
                    {
                        longestFoundSoFar.currentPathCount += 1;
                    }

                    return longestFoundSoFar;
                }
            }

        }
        Debug.Log("An unexpected problem");
        return null;
    }


    #endregion

    #region cordinant helper Functions

    /// <summary>
    /// Converts Grid cordinants to game cordinants
    /// </summary>
    /// <param name="x">int x</param>
    /// <param name="z">int z</param>
    /// <returns>Vector3 position in game cordinants</returns>
    Vector3 GetGamePositionFromGridPosition(int x, int z)
    {
        const int xIncrement = 10;
        const int zIncrement = 10;

        Vector3 gridPos = new();

        gridPos.x += (x * xIncrement) + 5;
        gridPos.z += (z * zIncrement) + 5;


        return gridPos;
    }

    /// <summary>
    /// Converts game cordinants to Grid cordinants 
    /// </summary>
    /// <param name="position">Vector3 position</param>
    /// <returns>int[2] grid Cordinants</returns>
    int[] GetGridPositionFromGamePosition(Vector3 position)
    {
        int[] gridPos = new int[2];

        gridPos[0] = (int)position.x / 10;
        gridPos[1] = (int)position.z / 10;


        return gridPos;
    }

    MazeNode GetMazeNodeFromGamePosition(Vector3 position)
    {
        MazeNode closestNode = null;
        float distance = float.MaxValue;

        foreach (var Item in MazeNode.pathNodes)
        {
            float currentNodeDistance = Vector3.Distance(position, Item.Value.GameLocation);
            if (distance > currentNodeDistance)
            {
                distance = currentNodeDistance;
                closestNode = Item.Value;
            }
        }

        return closestNode;
    }

    #endregion
}
