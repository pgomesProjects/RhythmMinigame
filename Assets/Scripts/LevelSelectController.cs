using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{

    public GameObject[] songWindows;
    public GameObject[] arrowButtons;
    private int currentSong;

    private PlayerControls playerControls;

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.UI.Escape.performed += _ => EscapePress();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSong = 0;
        CheckArrows();

        //Set all song windows inactive except for the first one
        for (int i = 1; i < songWindows.Length; i++)
            songWindows[i].SetActive(false);
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchSongSelect(int direction)
    {
        songWindows[currentSong].SetActive(false);
        currentSong += direction;
        songWindows[currentSong].SetActive(true);

        CheckArrows();
    }//end of switchSongSelect

    private void CheckArrows()
    {

        if (currentSong == 0)
            arrowButtons[0].SetActive(false);
        else
            arrowButtons[0].SetActive(true);

        if (currentSong >= songWindows.Length - 1)
            arrowButtons[1].SetActive(false);
        else
            arrowButtons[1].SetActive(true);

    }//end of CheckArrows

    public void SetBPM(float bpm) { SongInfo.Instance.bpm = bpm; }
    public void SetScrollSpeed(float scrollSpeed) { SongInfo.Instance.scrollSpeed = scrollSpeed; }
    public void SetMeasureBeats(float measureBeats) { SongInfo.Instance.measureBeats = measureBeats; }
    public void SetSongLength(float songLength) { SongInfo.Instance.songLength = songLength; }
    public void SetBeatmapFile(string beatmapFile) { SongInfo.Instance.beatmapFile = beatmapFile; }
    public void SetInstrumentalFile(string instrumentalFile) { SongInfo.Instance.instrumentalFile = instrumentalFile; }
    public void SetVocalsFile(string vocalsFile) { SongInfo.Instance.vocalsFile = vocalsFile; }
    public void SetInstrumentalVolume(float instrumentalVolume) { SongInfo.Instance.instrumentalVolume = instrumentalVolume; }
    public void SetVocalsVolume(float vocalsVolume) { SongInfo.Instance.vocalsVolume = vocalsVolume; }

    public void PlayLevel()
    {
        SceneManager.LoadScene("SongLevel");
    }//end of PlayLevel

    public void EscapePress()
    {
        Application.Quit();
    }//end of EscapePress
}
