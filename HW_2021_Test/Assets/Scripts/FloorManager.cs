using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] FloorTile floor_Prefab;
    [SerializeField] Vector3Int initialPosition;
    [SerializeField] Vector3Int initialRotation;
    [SerializeField] Transform parent;
    float tileTime = 0;

    private void OnEnable()
    {
        TickTimer.OnUpdateTick += OnUpdateTimer;
    }

    private void OnDisable()
    {
        TickTimer.OnUpdateTick -= OnUpdateTimer;
    }

    /// <summary>
    /// To initial values and spawn first tile object.
    /// </summary>
    void Start()
    {

        //Spawning the first tile.
        SpawnFloorTile(initialPosition, initialRotation);
    }

    /// <summary>
    /// Spawns a floor tile.
    /// </summary>  
    void SpawnFloorTile(Vector3 pos, Vector3 rot)
    {
        // Instantiate and set the parent of the floor tile.
        FloorTile tile = Instantiate(floor_Prefab, pos, Quaternion.Euler(rot));
        tile.transform.SetParent(parent);

        tileTime = Random.Range(GameManager.minLifeSpan, GameManager.maxLifeSpan); // get the randam value for tile life span between 4 and 5.

        //Set the tile life span.
        tile.SetTimer(tileTime);
    }

    /// <summary>
    /// This will act like update function of the script
    /// </summary>
    /// <param name="sender">the sending object</param>
    /// <param name="timer">Class with time.deltatime value</param>
    private void OnUpdateTimer(object sender, TickTimer.OnTickEventArgs timer)
    {

    }


    void GetNextSpawningPosition()
    {

    }
}
