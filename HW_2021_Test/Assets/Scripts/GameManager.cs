using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static float minLifeSpan = 4f;
    public static float maxLifeSpan = 5f;
    public static float nextSpawnTime = 2.5f;
    public static float playerSpeed = 8;
    static int _playerScore;
    public int playerScore
    {
        set
        {
            _playerScore = value;
            playerScoreText.text = value.ToString();
        }
        get
        {
            return _playerScore;
        }
    }
    public string DoofusDiaryURL = "doofus_diary";

    [SerializeField] TMP_Text playerScoreText;

    private void Awake()
    {
        OnLoadDoofusDiary(DoofusDiaryURL);
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
    }
}
