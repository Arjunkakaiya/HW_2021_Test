using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public static Action OnStartGame;

    public Canvas menuCanvas;
    public Canvas gameCanvas;
    public Canvas reloadCanvas;

    private void OnEnable()
    {

        Pit.OnGameover += OnGameover;
    }

    private void OnDisable()
    {
        Pit.OnGameover -= OnGameover;
    }

    private void Start()
    {
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
        reloadCanvas.enabled = false;
    }

    public void OnGameStart()
    {
        menuCanvas.enabled = false;
        reloadCanvas.enabled = false;
        gameCanvas.enabled = true;
        OnStartGame?.Invoke();
    }

    void OnGameover()
    {
        gameCanvas.enabled = false;
        reloadCanvas.enabled = true;
    }
}
