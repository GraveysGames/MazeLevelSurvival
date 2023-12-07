using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{

    public bool[,] GetMaze(int w, int h)
    {

        Height = h;
        Width = w;

        StartingCord = new int[2] { 0,0 };

        PrimsMazeGenerator();
        return MazeGrid;
    }

    public bool[,] GetMaze(int w, int h, int seed)
    {

        Random.InitState(seed);

        Height = h;
        Width = w;

        StartingCord = new int[2] { 0, 0 };

        PrimsMazeGenerator();
        return MazeGrid;
    }

    public bool[,] GetMaze((int width, int height) mazeSize, int seed)
    {

        Random.InitState(seed);

        Height = mazeSize.width;
        Width = mazeSize.height;

        StartingCord = new int[2] { 0, 0 };

        PrimsMazeGenerator();
        return MazeGrid;
    }


    private bool[,] MazeGrid { get; set; }

    private int Height { get; set; }

    private int Width { get; set; }

    private int[] StartingCord { get; set; }

    private void PrimsMazeGenerator()
    {

        List<int[]> walls = new();

        int randomWall;
        MazeGrid = new bool[Width, Height];

        MazeGrid[1, 1] = true;

        walls.AddRange(GetWalls(1,1));

        while (walls.Count > 0)
        {
            randomWall = Random.Range(0, walls.Count);

            int[] thisWall = walls[randomWall];

            if (DeterminePath(thisWall))
            {
                MazeGrid[thisWall[0], thisWall[1]] = true;
                walls.AddRange(GetWalls(thisWall[0], thisWall[1]));
            }

            walls.RemoveAt(randomWall);
            
        }

    }

    bool DeterminePath(int[] thisWall)
    {
        int i = thisWall[0];
        int k = thisWall[1];

        int visitCount = 0;

        //Check North
        if (MazeGrid[i, k + 1] == true)
        {
            visitCount++;
        }
        //Check East
        if (MazeGrid[i + 1, k] == true)
        {
            visitCount++;
        }
        //Check South
        if (MazeGrid[i, k - 1] == true)
        {
            visitCount++;
        }
        //Check West
        if (MazeGrid[i - 1, k] == true)
        {
            visitCount++;
        }

        if (visitCount > 1)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    private List<int[]> GetWalls(int i, int k)
    {

        List<int[]> walls = new();

        int[] filler;

        //add North wall
        if (k < (Height - 2))
        {
            filler = new[] { i, k + 1 };
            walls.Add(filler);
        }

        //add East wall
        if ( i < (Width - 2) )
        {
            filler = new[] { i + 1, k };
            walls.Add(filler);
        }

        //add South wall
        if (k > (StartingCord[1] + 1))
        {
            filler = new[] { i, k - 1 };
            walls.Add(filler);
        }

        //add East wall
        if (i > (StartingCord[0] + 1))
        {
            filler = new[] { i - 1, k };
            walls.Add(filler);
        }

        return walls;
    }


}
