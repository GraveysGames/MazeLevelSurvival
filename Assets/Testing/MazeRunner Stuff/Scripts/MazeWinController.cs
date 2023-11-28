using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWinController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameEvents_SinglePlayer.current.WinTrigger();
        }
    }
}
