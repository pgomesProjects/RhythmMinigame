
using UnityEngine;

[System.Serializable]
public class SongInfo : MonoBehaviour
{
    public float bpm = 120;
    public float scrollSpeed;
    public int measureBeats = 8;
    public float songLength = 60;

    public string beatmapFile = "sadness_beatmap.txt";
    public string instrumentalFile;
    public string vocalsFile;

    public float instrumentalVolume;
    public float vocalsVolume;

    public static SongInfo Instance { get; private set; }
    void Awake()
    {
        //Make sure this object does not destroy itself when loading between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

    }

}
