using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        speed = GameManager.playerSpeed;
    }

    private void OnEnable()
    {
        TickTimer.OnUpdateTick += OnUpdateTick;
    }
    private void OnDisable()
    {
        TickTimer.OnUpdateTick -= OnUpdateTick;
    }

    private void OnUpdateTick(object sender, TickTimer.OnTickEventArgs tick)
    {
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(hori, 0, vert).normalized;

        transform.position += dir * speed * tick._ticks; 
    }
}
