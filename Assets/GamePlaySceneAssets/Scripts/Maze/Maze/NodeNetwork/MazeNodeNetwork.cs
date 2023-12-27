using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNodeNetwork : MonoBehaviour
{

    public Dictionary<Vector3, MazeNode> PathNodes { get; private set; }

    public void AddPathNodeToDictionary(Vector3 worldLocation, MazeNode node)
    {
        if (PathNodes == null)
        {
            PathNodes = new();
        }

        PathNodes.Add(worldLocation, node);
    }

    public MazeNode GetMazeNodeFromWorldPosition(Vector3 currentWorldPosition)
    {
        MazeNode closestNode = null;
        double closestNodeDistance = float.MaxValue;

        foreach (var node in PathNodes)
        {
            double currentDistance = Vector3.Distance(currentWorldPosition, node.Key);
            if (currentDistance < closestNodeDistance)
            {
                closestNodeDistance = currentDistance;
                closestNode = node.Value;
            }
        }

        return closestNode;
    }


    private void OnTileDestroyed(Vector3 tilePosition)
    {
        //do nothing
    }
    // Start is called before the first frame update
    void Start()
    {
        MazeEvents.Singleton.OnTileDestroyedTrigger += OnTileDestroyed;
    }


    public class MazeNode
    {
        public FillMaze.TileType TileType { get; private set; }
        private GameObject _tilePrefab;

        private int _tileId;
        private static int tileCount = 0;

        public Vector3 WorldLocation { get; private set; }

        private GameObject _instantiatedTile;

        //used for path nodes
        

        public List<MazeNode> NextPathNodes { get; private set; }

        public List<MazeNode> NeighboringWalls { get; private set; }

        #region Constructors
        public MazeNode(FillMaze.TileType tileType, GameObject tilePrefab, Vector3 worldLocation)
        {
            TileType = tileType;
            _tilePrefab = tilePrefab;
            WorldLocation = worldLocation;

            _tileId = tileCount;
            tileCount++;

            _instantiatedTile = null;
            NextPathNodes = new();
            NeighboringWalls = new();

        }

        /// <summary>
        /// Adds the next node
        /// </summary>
        /// <param name="neighbor">A neighbor or this node</param>
        public void AddNeighboringNode(MazeNode neighbor)
        {
            if (neighbor.TileType == FillMaze.TileType.path)
            {
                this.NextPathNodes.Add(neighbor);
            }
            else
            {
                if (this.TileType == FillMaze.TileType.path)
                {
                    this.NeighboringWalls.Add(neighbor);
                }
            }
        }

        #endregion

        public void InstantiatTile(Transform ContentContainer)
        {
            if (this._instantiatedTile == null)
            {
                this._instantiatedTile = Instantiate(_tilePrefab) as GameObject;
                this._instantiatedTile.transform.position = WorldLocation;
                this._instantiatedTile.transform.SetParent(ContentContainer);
                this._tileId = this._instantiatedTile.GetComponent<MazeTileController>().TileId;
            }
            else
            {
                ExtendTileDestruction();
            }
            if (TileType == FillMaze.TileType.path)
            {
                InstantiateNeighboringWalls(ContentContainer);
            }
            
        }

        public void InstantiateNeighboringWalls(Transform ContentContainer)
        {
            foreach (MazeNode wall in NeighboringWalls)
            {
                wall.InstantiatTile(ContentContainer);
            }
        }

        public void ExtendTileDestruction()
        {
            MazeEvents.Singleton.TileExtendDistructionTrigger(this._tileId);
        }

    }

}
