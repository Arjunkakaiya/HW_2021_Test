using UnityEngine;
using System;

public class Pit : MonoBehaviour
{
    public static Action OnGameover;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Pit destroy player");
            OnGameover?.Invoke();
        }
    }
}