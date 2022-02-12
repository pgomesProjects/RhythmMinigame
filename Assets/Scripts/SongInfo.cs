
using UnityEngine;

[System.Serializable]
public class SongInfo : MonoBehaviour
{
    [HideInInspector]
    public float bpm = 120;
    [HideInInspector]
    public float scrollSpeed;
    [HideInInspector]
    public int measureBeats = 8;
    [HideInInspector]
    public float songLength = 60;

    [HideInInspector]
    public string beatmapFile = "sadness_beatmap.txt";
    [HideInInspector]
    public string instrumentalFile;
    [HideInInspector]
    public string vocalsFile;

    [HideInInspector]
    public float instrumentalVolume;
    [HideInInspector]
    public float vocalsVolume;

    public static SongInfo instance;
    // Start is called before the first frame update
    void Start()
    {
        //Make sure this object does not destroy itself when loading between scenes
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetBPM(float bpm) { this.bpm = bpm; }
    public void SetScrollSpeed(float scrollSpeed) { this.scrollSpeed = scrollSpeed; }
    public void SetMeasureBeats(int measureBeats) { this.measureBeats = measureBeats; }
    public void SetSongLength(float songLength) { this.songLength = songLength; }
    public void SetBeatmapFile(string beatmapFile) { this.beatmapFile = beatmapFile; }
    public void SetInstrumentalsFile(string instrumentalFile) { this.instrumentalFile = instrumentalFile; }
    public void SetVocalsFile(string vocalsFile) { this.vocalsFile = vocalsFile; }
    public void SetInstrumentalVolume(float instrumentalVolume) { this.instrumentalVolume = instrumentalVolume; }
    public void SetVocalsVolume(float vocalsVolume) { this.vocalsVolume = vocalsVolume; }

}
