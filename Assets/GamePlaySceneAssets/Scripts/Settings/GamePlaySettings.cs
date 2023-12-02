using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GamePlaySettings : ScriptableObject
{
    //Settings

    [Range(30, 100)]
    public int fieldOfView;
    [Range(1, 100)]
    public float MouseSensitivity;

}
