using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillMaze : MonoBehaviour
{


    //fill maze with tiles 

    #region Constants

    //the length of one side of the square
    const int CHUNKSIZE = 20;


    #endregion

    public enum TileType
    {
        wall,
        path
    }

    //Tile prefabs
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _pathPrefab;
    [SerializeField] private GameObject _pathLightPrefab;

    [SerializeField] private int _lightInterval = 5;
    private int _lightDistanceCount = 0;

    [SerializeField] public int seed { get; private set; }

    private MazeNodeNetwork mazeNodeNetwork;

    private List<MazeNodeNetwork.MazeNode> _lastColumn = new();

    private MazeNodeNetwork.MazeNode _lastNode = null;

    public void BuildMaze(Vector3 mazeLocationCordinants, (int, int) mazeSize)
    {

        mazeNodeNetwork = GetComponent<MazeNodeNetwork>();

        seed = (int)DateTime.Now.Ticks;

        bool[,] GeneratedMaze = GetComponent<GenerateMaze>().GetMaze(mazeSize, seed);

        FillMazeTiles(GeneratedMaze, mazeSize);

    }

    private void FillMazeTiles(bool[,] GeneratedMaze, (int width, int height) mazeSize)
    {

        (FillMaze.TileType tileType, GameObject tilePrefab, Vector3 worldLocation) nodeData;

        for (int row = 0; row < mazeSize.width; row++)
        {
            for (int column = 0; column < mazeSize.height; column++)
            {
                nodeData = FillMazeTile(GeneratedMaze[row, column]);

                nodeData.worldLocation = GetWorldPositionFromGridPosition(row, column);

                MazeNodeNetwork.MazeNode currentMazeNode = new(nodeData.tileType , nodeData.tilePrefab, nodeData.worldLocation);

                if (nodeData.tileType == TileType.path)
                {
                    mazeNodeNetwork.AddPathNodeToDictionary(nodeData.worldLocation, currentMazeNode);
                }


                if (row > 0)
                {
                    FindLeftNeighbor(currentMazeNode);
                }

                if (column != 0)
                {
                    FindUpNeighbor(currentMazeNode);
                }

                _lastColumn.Add(currentMazeNode);
                _lastNode = currentMazeNode;
            }
        }
    }

    Vector3 GetWorldPositionFromGridPosition(int x, int z)
    {
        const int xIncrement = 10;
        const int zIncrement = 10;

        Vector3 worldPos = new();

        worldPos.x += (x * xIncrement) + 5;
        worldPos.z += (z * zIncrement) + 5;


        return worldPos;
    }

    private (FillMaze.TileType tileType, GameObject tilePrefab, Vector3 worldLocation) FillMazeTile(bool generatedMazeTile)
    {
        (FillMaze.TileType tileType, GameObject tilePrefab, Vector3 worldLocation) currentNodeData = new();

        if (generatedMazeTile == true)
        {
            currentNodeData.tileType = TileType.path;
            if (_lightDistanceCount == _lightInterval)
            {
                //Path Light
                currentNodeData.tilePrefab = _pathLightPrefab;
                _lightDistanceCount = 0;
            }
            else
            {
                //Path
                currentNodeData.tilePrefab = _pathPrefab;
                _lightDistanceCount++;
            }
        }
        else
        {
            //wall
            currentNodeData.tileType = TileType.wall;
            currentNodeData.tilePrefab = _wallPrefab;
        }


        return currentNodeData;
    }


    private void FindLeftNeighbor(MazeNodeNetwork.MazeNode currentMazeNode)
    {
        MazeNodeNetwork.MazeNode leftNode = _lastColumn[0];
        leftNode.AddNeighboringNode(currentMazeNode);
        currentMazeNode.AddNeighboringNode(leftNode);
        _lastColumn.RemoveAt(0);
    }

    private void FindUpNeighbor(MazeNodeNetwork.MazeNode currentMazeNode)
    {
        MazeNodeNetwork.MazeNode upperNode = _lastNode;
        upperNode.AddNeighboringNode(currentMazeNode);
        currentMazeNode.AddNeighboringNode(upperNode);
    }

}
