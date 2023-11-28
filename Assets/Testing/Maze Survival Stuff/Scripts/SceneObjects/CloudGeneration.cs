using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGeneration : MonoBehaviour
{

    [SerializeField] private Material cloudMat;

    private int cloudHeight = 100;

    private int cloudColumns = 100;
    private int cloudRows = 100;

    // Start is called before the first frame update
    void Start()
    {
        CreateClouds();
    }


    private void CreateClouds()
    {
        float r = Random.Range(0f, 1f);
        float r2 = Random.Range(0f, 1f);
        for (int x = 0; x < (cloudColumns); x++)
        {
            for (int z = 0; z < (cloudRows); z++)
            {
                
                float xf = (float) x / ((float)cloudColumns);
                float zf = (float)z / ((float)cloudRows);
                float sample = Mathf.PerlinNoise(r +xf, r2 + zf);
                //Debug.Log(r + xf + " " + r2 + zf);
                sample *= 100;
                float r3 = Random.Range(-4f, 4f);
                SpawnCapsule(x * 10, cloudHeight + sample + r3, z * 10);
                r3 = Random.Range(-4f, 4f);
                SpawnCapsule(-x * 10, cloudHeight + sample + r3, z * 10);
                r3 = Random.Range(-4f, 4f);
                SpawnCapsule(x * 10, cloudHeight + sample + r3, -z * 10);
                r3 = Random.Range(-4f, 4f);
                SpawnCapsule(-x * 10, cloudHeight + sample + r3, -z * 10);
            }
        }
    }

    private void SpawnCapsule(int x, float y, int z)
    {
        GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        capsule.transform.SetParent(this.transform);
        capsule.transform.position = new Vector3(x, y, z);
        capsule.transform.localScale *= 14;
        capsule.GetComponent<Renderer>().material = cloudMat;
    }

}
