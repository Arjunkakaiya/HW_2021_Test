using UnityEngine;
using TMPro;
using System;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static float minLifeSpan = 4f;
    public static float maxLifeSpan = 5f;
    public static float nextSpawnTime = 2.5f;
    public static float playerSpeed = 8;
    public static int playerScore = 0;
    public string DoofusDiaryURL = "doofus_diary";

    public PlayerController playerPrefab;
    public GameObject player;
    public CinemachineVirtualCamera virtualCamera;

    [SerializeField] TMP_Text playerScoreText;

    private void Awake()
    {
        OnLoadDoofusDiary(DoofusDiaryURL);
    }

    private void OnEnable()
    {
        UIManager.OnStartGame += OnGameStart;
        FloorTile.OnScoreUpdate += OnScoreUpdated;
        Pit.OnGameover += OnGameover;
    }

    private void OnDisable()
    {
        UIManager.OnStartGame -= OnGameStart;
        FloorTile.OnScoreUpdate -= OnScoreUpdated;
        Pit.OnGameover -= OnGameover;
    }

   void OnGameStart()
    {
        player = Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity).gameObject;
        virtualCamera.Follow = player.transform;
        virtualCamera.LookAt = player.transform;
    }

    /// <summary>
    /// To load the Doofus diary
    /// </summary>
    /// <param name="url">path to load.</param>
    void OnLoadDoofusDiary(string url)
    {
        string data = Resources.Load<TextAsset>(url).ToString();
        Doofus doofusData = JsonUtility.FromJson<Doofus>(data);

        minLifeSpan = doofusData.pulpit_data.min_pulpit_destroy_time;
        maxLifeSpan = doofusData.pulpit_data.max_pulpit_destroy_time;
        nextSpawnTime = doofusData.pulpit_data.pulpit_spawn_time;
        playerSpeed = doofusData.player_data.speed;
        Debug.Log($"player speed: {playerSpeed}");
    }

    /// <summary>
    /// Reflects the updated score on UI.
    /// </summary>
    private void OnScoreUpdated()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();
    }

    /// <summary>
    /// Event called when gameover happens
    /// </summary>
    private void OnGameover()
    {
        //Redirect to Start screen.
        playerScore = 0;
        playerScoreText.text = playerScore.ToString();
        Destroy(player);
    }
}
