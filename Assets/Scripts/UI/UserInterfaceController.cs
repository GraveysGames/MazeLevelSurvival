using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{
    Canvas mainCanvas;
    [SerializeField] GameObject menuPrefab;
    // Start is called before the first frame update
    void Start()
    {
        mainCanvas = GetComponent<Canvas>();
        Instantiate(menuPrefab);
    }

}
