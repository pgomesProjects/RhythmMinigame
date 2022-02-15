using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SongManager : MonoBehaviour
{
    public GameObject pauseMenu;

    [HideInInspector]
    public float bpm;
    public float noteSpawnZ, buttonZ;
    [HideInInspector]
    public float scrollSpeed;
    [HideInInspector]
    public float measureBeats = 8;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI statText;
    public TextMeshProUGUI countDownText;
    [HideInInspector]
    public float songLength = 144;
    [HideInInspector]
    public float noteSpeed;
    [HideInInspector]
    public int combo = 0;
    [HideInInspector]
    public int[] stats;
    [HideInInspector]
    public bool isPaused;

    private bool isSongStarted;
    private bool isSongResuming;
    private int countDownNumber = 3;
    private float songTimer;
    private float countDownTimer;
    private float currentTimer;

    private bool statTextShown = false;
    private float statsTextCooldown = 0.5f;
    private float currentCooldown;
    private float currentScrollSpeed;
    private PlayerControls playerControls;

    IEnumerator songDuration;
    enum Hit { Perfect, Great, Good, Bad, Miss };

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.UI.Escape.performed += _ => PauseToggle();
        bpm = SongInfo.Instance.bpm;
        scrollSpeed = SongInfo.Instance.scrollSpeed;
        measureBeats = SongInfo.Instance.measureBeats;
        songLength = SongInfo.Instance.songLength;
    }

    // Start is called before the first frame update
    void Start()
    {
        songDuration = PlaySong();
        pauseMenu.SetActive(false);
        isPaused = true;
        noteSpeed = 0.05989583333f * SongInfo.Instance.bpm * scrollSpeed;
        currentScrollSpeed = scrollSpeed;

        //Stats: Perfect, Great, Good, Bad, Miss
        stats = new int[5];

        comboText.alpha = 0;
        statText.alpha = 0;

        countDownText.text = "" + countDownNumber;
        countDownTimer = (SongInfo.Instance.bpm / 60) / 4;
        currentTimer = 0;
        isSongStarted = false;
        isSongResuming = false;
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
        //If the song has not started, begin the countdown for the start of the song
        if (!isSongStarted)
        {
            StartSong();
        }

        //If the game has been unpaused, begin the countdown to continue the song
        if (isSongResuming)
        {
            ContinueSong();
        }

        //If the scroll speed changes in any way mid-game, change the note speed
        if(currentScrollSpeed != scrollSpeed)
        {
            currentScrollSpeed = scrollSpeed;
            noteSpeed = (noteSpawnZ - buttonZ) / (SongInfo.Instance.measureBeats / 2) * scrollSpeed;
        }

        //If stats are showing, use a cooldown to make them invisible again
        if (statTextShown)
        {
            currentCooldown += Time.deltaTime;
            if (currentCooldown >= statsTextCooldown)
            {
                statText.alpha = 0;
                statTextShown = false;
            }
        }
    }

    private void StartSong()
    {
        currentTimer += Time.deltaTime;

        //If the current timer hits the specified timer (which is relative to the BPM), count down by one
        if(currentTimer >= countDownTimer)
        {
            countDownNumber -= 1;
            if (countDownNumber == 0)
                countDownText.text = "Go!";
            else
                countDownText.text = "" + (int)countDownNumber;

            currentTimer = 0;
        }

        //If the countdown has finished, hide the countdown, unpause, and start the song
        if(countDownNumber <= -1)
        {
            countDownText.alpha = 0;
            isSongStarted = true;
            isPaused = false;
            StartCoroutine(songDuration);
        }

    }//end of StartSong

    private void ContinueSong()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= countDownTimer)
        {
            countDownNumber -= 1;
            Debug.Log(countDownNumber);
            if (countDownNumber == 0)
                countDownText.text = "Go!";
            else
                countDownText.text = "" + (int)countDownNumber;

            currentTimer = 0;
        }

        //If the countdown has finished, hide the countdown, unpause, and resume the song
        if (countDownNumber <= -1)
        {
            countDownText.alpha = 0;
            isPaused = false;
            isSongResuming = false;
            FindObjectOfType<AudioManager>().Resume(SongInfo.Instance.instrumentalFile);
            FindObjectOfType<AudioManager>().Resume(SongInfo.Instance.vocalsFile);
            StartCoroutine(songDuration);
        }

    }//end of ContinueSong

    IEnumerator PlaySong()
    {
        //Play instrumental and vocals at the same time for the song
        FindObjectOfType<AudioManager>().Play(SongInfo.Instance.instrumentalFile, SongInfo.Instance.instrumentalVolume * PlayerPrefs.GetFloat("MusicVolume")); // Play music upon level start
        FindObjectOfType<AudioManager>().Play(SongInfo.Instance.vocalsFile, SongInfo.Instance.vocalsVolume * PlayerPrefs.GetFloat("MusicVolume")); // Play music upon level start

        //While the coroutine is going, wait one second
        while (true)
        {
            yield return new WaitForSeconds(1);
            songTimer += 1;
            
            //If the song timer has reached the end of the song, stop the music if still playing and then go back to the level select
            if(songTimer >= songLength)
            {
                //Stop both audio tracks if playing
                if (FindObjectOfType<AudioManager>().IsPlaying(SongInfo.Instance.instrumentalFile))
                    FindObjectOfType<AudioManager>().Stop(SongInfo.Instance.instrumentalFile);

                if (FindObjectOfType<AudioManager>().IsPlaying(SongInfo.Instance.vocalsFile))
                    FindObjectOfType<AudioManager>().Stop(SongInfo.Instance.vocalsFile);

                SceneManager.LoadScene("LevelSelect");
            }
        }

    }

    public void PauseToggle()
    {
        //Allow the player to pause and unpause once the song is playing
        if (isSongStarted && !isSongResuming)
        {
            if (!isPaused)
                PauseLevel();
            else
                UnpauseLevel();
        }
    }//end of PauseToggle

    public void PauseLevel()
    {
        //When paused, stop the time scale, song counter, music, and put up the pause menu
        StopCoroutine(songDuration);
        pauseMenu.SetActive(true);
        FindObjectOfType<AudioManager>().Pause(SongInfo.Instance.instrumentalFile);
        FindObjectOfType<AudioManager>().Pause(SongInfo.Instance.vocalsFile);
        isPaused = true;
        Time.timeScale = 0.0f;
    }//end of PauseLevel

    public void UnpauseLevel()
    {
        //When unpaused, hide the pause menu, bring back the time scale, and begin a countdown
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;

        countDownNumber = 3;
        countDownText.text = "" + countDownNumber;
        countDownText.alpha = 1;
        isPaused = true;
        isSongResuming = true;
    }//end of UnpauseLevel

    public void ReturnToLevelSelect()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LevelSelect");
    }//end of ReturnToLevelSelect

    public void AddToCombo(int hit)
    {
        combo += 1;
        if(combo == 10)
            comboText.alpha = 1;
        DisplayCombo(hit);
    }//end of AddToCombo

    void DisplayCombo(int hit)
    {
        //Set the combo text
        comboText.text = "Combo: " + combo;

        //Display the correct hit type depending on what was given
        switch (hit)
        {
            case (int)Hit.Perfect:
                statTextShown = true;
                statText.alpha = 1;
                currentCooldown = 0;
                stats[(int)Hit.Perfect] += 1;
                statText.text = "Perfect!";
                break;
            case (int)Hit.Great:
                statTextShown = true;
                statText.alpha = 1;
                currentCooldown = 0;
                stats[(int)Hit.Great] += 1;
                statText.text = "Great!";
                break;
            case (int)Hit.Good:
                statTextShown = true;
                statText.alpha = 1;
                currentCooldown = 0;
                stats[(int)Hit.Good] += 1;
                statText.text = "Good!";
                break;
            case (int)Hit.Bad:
                statTextShown = true;
                statText.alpha = 1;
                currentCooldown = 0;
                stats[(int)Hit.Bad] += 1;
                statText.text = "Bad!";
                break;
            case (int)Hit.Miss:
                statTextShown = true;
                statText.alpha = 1;
                currentCooldown = 0;
                stats[(int)Hit.Miss] += 1;
                statText.text = "Miss!";
                break;
        }
    }

    public void ResetCombo(int hit)
    {
        combo = 0;
        comboText.alpha = 0;
        DisplayCombo(hit);
    }//end of AddToCombo
}
