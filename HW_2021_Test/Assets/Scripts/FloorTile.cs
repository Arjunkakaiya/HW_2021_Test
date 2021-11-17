using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(Animator))]
public class FloorTile : MonoBehaviour
{
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

    internal void SetTimer(float value)
    {
        initialTimer = value;
        isAlive = true;
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
            Debug.Log("Destroying the tile");
            tileAnimController.SetTrigger("tDestroy");
        }
    }

    public void DestroyTile()
    {
        Debug.Log("Tile destroyed");
        Destroy(this.gameObject);
    }
}
