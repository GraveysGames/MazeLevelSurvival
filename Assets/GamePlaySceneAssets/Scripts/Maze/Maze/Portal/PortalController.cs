using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private float height;

    [SerializeField] public string ObjectName;

    public float Height { get; }

    public void GoThroughPortal()
    {
        MazeEvents.Singleton.MazePortalEnterTrigger();
    }

}
