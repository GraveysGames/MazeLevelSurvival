using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharicter : MonoBehaviour
{
    
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        int x = 0;
        int z = 0;

        if (Input.GetKey(KeyCode.W))
        {
            z = z + 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            z = z - 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x += 1;
        }

        float stepSize = 0.1f;


        Vector3 position = transform.position;

        position.x += (x * stepSize);
        position.z += (z * stepSize);

        transform.position = position;

    }

}
