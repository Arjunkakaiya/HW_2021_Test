using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] FloorTile floor_Prefab;
    [SerializeField] Vector3Int initialPosition;
    [SerializeField] Vector3Int initialRotation;
    [SerializeField] Transform parent;
    [SerializeField] float tileTime = 0;
    [SerializeField] Vector3 previousSpawnPos;
    public Direction currentTileDirection;
    [SerializeField] Direction previousTileDirection;
    [SerializeField] int spawnCount = 0;

    private void OnEnable()
    {
        TickTimer.OnUpdateTick += OnUpdateTimer;
        FloorTile.OnTileDestroyed += OnTileDestroyed;
    }

    private void OnDisable()
    {
        TickTimer.OnUpdateTick -= OnUpdateTimer;
        FloorTile.OnTileDestroyed -= OnTileDestroyed;
    }

    /// <summary>
    /// To initial values and spawn first tile object.
    /// </summary>
    void Start()
    {
        previousSpawnPos = initialRotation;
        // Spawning the first tile.
        SpawnFloorTile(initialPosition);
    }

    /// <summary>
    /// Event called when tile is destroyed.
    /// </summary>
    void OnTileDestroyed()
    {
        spawnCount--;
    }

    /// <summary>
    /// Spawns a floor tile.
    /// </summary>  
    void SpawnFloorTile(Vector3 pos)
    {
        // Instantiate and set the parent of the floor tile.
        FloorTile tile = Instantiate(floor_Prefab, pos, Quaternion.identity);
        tile.transform.SetParent(parent);

        tileTime = Random.Range(GameManager.minLifeSpan, GameManager.maxLifeSpan); // get the randam value for tile life span between 4 and 5.

        // Set the tile life span.
        tile.SetTimer(tileTime);

        spawnCount++;
    }

    /// <summary>
    /// This will act like update function of the script
    /// </summary>
    /// <param name="sender">the sending object</param>
    /// <param name="timer">Class with time.deltatime value</param>
    private void OnUpdateTimer(object sender, TickTimer.OnTickEventArgs timer)
    {
        tileTime -= timer._ticks;

        if (tileTime <= GameManager.nextSpawnTime)
        {
            tileTime = GameManager.minLifeSpan;
            StartCoroutine(SpawnFloorTile());
        }
    }

    IEnumerator SpawnFloorTile()
    {
        yield return new WaitUntil(() => spawnCount == 1);
        // Get next direction and spawning the tile.
        Direction nextDir = GetNextSpawningDirection();
    dir:
        if (nextDir == currentTileDirection)
        {
            nextDir = GetNextSpawningDirection();
            goto dir;
        }
        Vector3 pos = previousSpawnPos + GetNextPosition(nextDir);
        SpawnFloorTile(pos);
        previousSpawnPos = pos;
        previousTileDirection = currentTileDirection;
        currentTileDirection = nextDir;
    }

    /// <summary>
    /// Returns next direction for spawning the tile.
    /// </summary>
    /// <returns>direction for the tile.</returns>
    Direction GetNextSpawningDirection()
    {
        // Returns the random direction for spawning the tile.
        Direction dir;
        if (currentTileDirection == Direction.Left || currentTileDirection == Direction.Right)
        {
            dir = (Direction)Random.Range(3, 5);
        }
        else 
        {
            dir = (Direction)Random.Range(1, 3);
        }
        return dir;
    }

    /// <summary>
    /// Return position based on the given direction.
    /// </summary>
    /// <param name="dir">the given direction</param>
    /// <returns>position for spawning the next tile.</returns>
    Vector3 GetNextPosition(Direction dir)
    {
        switch (dir)
        {
            case Direction.Left: return new Vector3(-9, 0, 0);
            case Direction.Right: return new Vector3(9, 0, 0);
            case Direction.Top: return new Vector3(0, 0, 9);
            case Direction.Bottom:
            default: return new Vector3(0, 0, -9);
        }
    }
}
