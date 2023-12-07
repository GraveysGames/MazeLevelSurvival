using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTileController : MonoBehaviour
{
    static int tileCount = 0;
    int tileId;
    Coroutine destructionTimer;

    public int TileId { get => tileId; }

    void Awake()
    {
        tileId = tileCount;
        tileCount++;
        destructionTimer = StartCoroutine(DestroyTimer());
        MazeEvents.Singleton.OnTileExtendDistruction += ExtendDistruction;
    }


    private void ExtendDistruction(int tileId)
    {
        if (this.tileId == tileId)
        {
            StopCoroutine(destructionTimer);
            destructionTimer = StartCoroutine(DestroyTimer());
        }

    }


    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(1);

        MazeEvents.Singleton.TileDestroyedTrigger(this.transform.position);

        yield return new WaitForEndOfFrame();

        Destroy(this.gameObject);

        
    }


    private void OnDestroy()
    {
        MazeEvents.Singleton.OnTileExtendDistruction -= ExtendDistruction;
    }

}
