using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeStartAndEnd : MonoBehaviour
{

    

    [SerializeField] GameObject portalPrefab;
    [SerializeField] GameObject arrowStart;

    [SerializeField] private Vector3 playerStartPos = new(15, 4, 15);
    [SerializeField] private Vector3 playerEndPos = new(0, 0, 0);

    [SerializeField] private Transform container;

    public Vector3 FindStartAndEnd()
    {

        Dictionary<Vector3, MazeNodeNetwork.MazeNode> pathNodes = GetComponent<MazeNodeNetwork>().PathNodes;

        int sizeOfDictionary = pathNodes.Count;

        int startIndex = Random.Range(0, sizeOfDictionary-1);
        int endIndex = Random.Range(0, sizeOfDictionary - 1);

        while (endIndex == startIndex)
        {
            endIndex = Random.Range(0, sizeOfDictionary - 1);
        }

        int count = 0;
        foreach (var node in pathNodes)
        {
            if (count == startIndex)
            {
                playerStartPos = node.Key;
            }
            if (count == endIndex)
            {
                playerEndPos = node.Key;
            }
            count++;
        }

        GameObject instantiatedObjectTemporaryHolder = Instantiate(arrowStart, container) as GameObject;

        Vector3 arrowPosition = playerStartPos;

        arrowPosition[1] = 5;

        instantiatedObjectTemporaryHolder.transform.position = arrowPosition;

        instantiatedObjectTemporaryHolder = Instantiate(portalPrefab, container) as GameObject;

        playerEndPos.y += 2;

        instantiatedObjectTemporaryHolder.transform.position = playerEndPos;

        playerStartPos.y += 6;
        return playerStartPos;
    }

    /*
    #region Finds Start and End of Maze Functions


        private void CheckAllPathInterSections(MazeNodeNetwork.MazeNode currentPath, MazeNodeNetwork.MazeNode previousNode)
    {

        currentPath.CheckIntersection();

        foreach (MazeNodeNetwork.MazeNode nextPath in currentPath.NextPathNodes)
        {
            if (nextPath != previousNode)
            {
                CheckAllPathInterSections(nextPath, currentPath);
            }
        }

    }

    //Struct to help find the longest path in the maze
    private class LongestPathNode
    {

        //The start and end node for the currently set longest path
        public MazeNodeNetwork.MazeNode startNode;
        public MazeNodeNetwork.MazeNode endNode;
        //keeps track of the current path count go through forward iterations, and the longest found path going backwards
        public int currentPathCount;
        //the count of paths between the start and end node
        public int longestPathCount;

        //used to keep track if the currently longest path on the path of exiting iterations
        //if it is not then the current path has to be updated with the new add tiles inbetween it and the next intersection
        public bool pathBacktracking = false;


        //initializes the Node
        public LongestPathNode(MazeNodeNetwork.MazeNode Start, MazeNodeNetwork.MazeNode End, int CurrentPathCount, int LongestPathCount)
        {
            this.startNode = Start;
            this.endNode = End;
            this.currentPathCount = CurrentPathCount;
            this.longestPathCount = LongestPathCount;
        }

        public LongestPathNode(MazeNodeNetwork.MazeNode Start, MazeNodeNetwork.MazeNode End, int CurrentPathCount, int LongestPathCount, bool PathBackTracking)
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

            List<MazeNodeNetwork.MazeNode> possibleStartNodes = new();
            List<MazeNodeNetwork.MazeNode> possibleEndNodes = new();

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

            MazeNodeNetwork.MazeNode chosenStartNode = possibleStartNodes[randomChoice];

            randomChoice = UnityEngine.Random.Range(0, possibleEndNodes.Count);

            MazeNodeNetwork.MazeNode chosenEndNode = possibleEndNodes[randomChoice];

            playerStartPos = chosenStartNode.GameLocation;
            playerStartPos.y += 4;
            playerEndPos = chosenEndNode.GameLocation;

        }
        else
        {
            MazeNodeNetwork.MazeNode firstNode = Maze[1, 1];

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

    private LongestPathNode FindLongestPathInMaze(LongestPathNode CurrentNode, MazeNodeNetwork.MazeNode CurrentMazeNode, MazeNodeNetwork.MazeNode LastMazeNode)
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

            foreach (MazeNodeNetwork.MazeNode nextNode in CurrentMazeNode.NextPathNodes)
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
            foreach (MazeNodeNetwork.MazeNode nextNode in CurrentMazeNode.NextPathNodes)
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
    */
}
