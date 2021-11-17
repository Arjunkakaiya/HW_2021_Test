using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public static float minLifeSpan = 4f;
    public static float maxLifeSpan = 5f;
    public static float nextSpawnTime = 2.5f;

    public string DoofusDiaryURL = "doofus_diary";

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
        Debug.Log($"data: {data}");
        Doofus doofusData = JsonUtility.FromJson<Doofus>(data);

        minLifeSpan = doofusData.pulpit_data.min_pulpit_destroy_time;
        maxLifeSpan = doofusData.pulpit_data.max_pulpit_destroy_time;
        nextSpawnTime = doofusData.pulpit_data.pulpit_spawn_time;
    }
}
