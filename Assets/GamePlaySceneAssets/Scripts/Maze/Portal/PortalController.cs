using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private float height;

    private MazeManager mazeManager;

    [SerializeField] public string ObjectName;

    public float Height { get; }

    public void SetUpPortal(MazeManager manager)
    {
        mazeManager = manager;
    }

    public void GoThroughPortal()
    {
        mazeManager.BuildNewMaze();
    }

}
