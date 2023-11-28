using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameObject current;

    private static int width = 100;
    private static int height = 100;

    private static bool isSurvival = false;

    private static string scene = "Menu";

    //true is host, false is client
    private static bool networkMode = false;

    public static int Width { get => width; set => width = value; }
    public static int Height { get => height; set => height = value; }
    public static string Scene { get => scene; set => scene = value; }
    public static bool NetworkMode { get => networkMode; set => networkMode = value; }
    public static bool IsSurvival { get => isSurvival; set => isSurvival = value; }

    private void Awake()
    {
        
        if (current == null)
        {
            current = this.gameObject;
            GameObject.DontDestroyOnLoad(current);
        }
        else
        {
            Destroy(this);
        }

    }

}
