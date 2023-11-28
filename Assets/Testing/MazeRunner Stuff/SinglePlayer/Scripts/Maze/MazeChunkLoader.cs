using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeChunkLoader : MonoBehaviour
{

    //Tile container in heiarchy for the unity editor
    [SerializeField] private Transform contentContainer;
    [SerializeField] private int tendralSpawnReach = 1;


    [SerializeField] private MazeNodeNetwork nodeNetwork;

    private Vector3 playerPosition;

    //private bool paused;


    private const float DELAY_BETWEEN_CHUNK_UPDATES = 0.5f;

    public List<MazeNodeNetwork.MazeNode> SpawnedNodes { get; private set; }

    private void Start()
    {
        GameEvents_SinglePlayer.current.OnPlayerPositionChanged += OnPlayerPositionChanged;
        SpawnedNodes = new();

        InvokeRepeating("SpawnChunk", 0.1f, DELAY_BETWEEN_CHUNK_UPDATES);
    }

    private void SpawnChunk()
    {
        BuildChunkTypeTendrals();
    }


    private void BuildChunkTypeTendrals()
    {
        SpawnedNodes.Clear();

        MazeNodeNetwork.MazeNode nodePlayerIsOn = nodeNetwork.GetMazeNodeFromWorldPosition(playerPosition);
        nodePlayerIsOn.InstantiatTile(contentContainer);

        List<MazeNodeNetwork.MazeNode> nodesToSpawn = new();

        nodesToSpawn.Add(nodePlayerIsOn);

        int tendralSpawnCount = 0;

        while (nodesToSpawn.Count > tendralSpawnCount && tendralSpawnReach > tendralSpawnCount)
        {
            nodesToSpawn[tendralSpawnCount].InstantiatTile(contentContainer);

            SpawnedNodes.Add(nodesToSpawn[tendralSpawnCount]);

            List<MazeNodeNetwork.MazeNode> neighbors = nodesToSpawn[tendralSpawnCount].NextPathNodes;

            foreach (MazeNodeNetwork.MazeNode node in neighbors)
            {
                if (!nodesToSpawn.Contains(node))
                {
                    nodesToSpawn.Add(node);
                }
            }

            tendralSpawnCount++;
        }

    }

    private void OnPlayerPositionChanged(Vector3 playerPosition)
    {

        this.playerPosition = playerPosition;

    }


    /*

    #region ChunkBuilding
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
    #endregion



    */
}
