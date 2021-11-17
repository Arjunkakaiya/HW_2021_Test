using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(Animator))]
public class FloorTile : MonoBehaviour
{
    public static Action OnTileDestroyed;
    public TextMeshPro timer;
    private float _initialTimer;
    public float initialTimer
    {
        set
        {
            _initialTimer = value;
            timer.text = value.ToString("0.##");
        }
        get
        {
            return _initialTimer;
        }
    }

    private float _nextSpawnTime;

    public float nextSpawnTime
    {
        set { _nextSpawnTime = value; }
        get { return _nextSpawnTime; }
    }

    [SerializeField] Animator tileAnimController;
    bool isAlive = false;
    bool isTileVisited = false;

    private void Awake()
    {
        tileAnimController = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        TickTimer.OnUpdateTick += UpdateTime;
    }

    private void OnDisable()
    {
        TickTimer.OnUpdateTick -= UpdateTime;
    }

    /// <summary>
    /// Set the timer of the tile.
    /// </summary>
    /// <param name="value"></param>
    internal void SetTimer(float value)
    {
        initialTimer = value;
        isAlive = true;
        isTileVisited = false;
    }

    private void UpdateTime(object sender, TickTimer.OnTickEventArgs onTick)
    {
        //Check the tile is alive.
        if (!isAlive) return;

        //Decrement the tile life span.
        initialTimer -= onTick._ticks;

        //Check if the timer is over.
        if (initialTimer <= 0)
        {
            isAlive = false;
            initialTimer = 0; //Resetting the timer to zero.

            //Destroy the tile.
            tileAnimController.SetTrigger("tDestroy");
        }
    }

    public void DestroyTile()
    {
        OnTileDestroyed?.Invoke();
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !isTileVisited)
        {
            //Increase the score.
            Debug.Log("Increase Score");
            GameManager.playerScore++;
        }
    }
}
