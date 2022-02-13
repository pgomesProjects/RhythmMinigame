using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SongManager : MonoBehaviour
{
    [HideInInspector]
    public float bpm;
    public float noteSpawnZ, buttonZ;
    [HideInInspector]
    public float scrollSpeed;
    [HideInInspector]
    public int measureBeats = 8;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI statText;
    [HideInInspector]
    public float songLength = 144;
    [HideInInspector]
    public float noteSpeed;
    [HideInInspector]
    public int combo = 0;
    [HideInInspector]
    public int[] stats;

    private AudioClip gameMusic;

    private bool statTextShown = false;
    private float statsTextCooldown = 0.5f;
    private float currentCooldown;
    private float currentScrollSpeed;

    enum Hit { Perfect, Great, Good, Bad, Miss };

    void Awake()
    {
        bpm = SongInfo.Instance.bpm;
        scrollSpeed = SongInfo.Instance.scrollSpeed;
        measureBeats = SongInfo.Instance.measureBeats;
        songLength = SongInfo.Instance.songLength;
    }

    // Start is called before the first frame update
    void Start()
    {
        noteSpeed = (noteSpawnZ - buttonZ) / 4 * scrollSpeed;
        currentScrollSpeed = scrollSpeed;
        //Stats: Perfect, Great, Good, Bad, Miss
        stats = new int[5];

        comboText.alpha = 0;
        statText.alpha = 0;

        //Play instrumental and vocals at the same time
        FindObjectOfType<AudioManager>().Play(SongInfo.Instance.instrumentalFile, SongInfo.Instance.instrumentalVolume); // Play music upon level start
        FindObjectOfType<AudioManager>().Play(SongInfo.Instance.vocalsFile, SongInfo.Instance.vocalsVolume); // Play music upon level start

        StartCoroutine(EndSong());
    }

    // Update is called once per frame
    void Update()
    {
        //If the scroll speed changes in any way mid-game, change the note speed
        if(currentScrollSpeed != scrollSpeed)
        {
            currentScrollSpeed = scrollSpeed;
            noteSpeed = (noteSpawnZ - buttonZ) / 4 * scrollSpeed;
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
    IEnumerator EndSong()
    {
        yield return new WaitForSeconds(songLength);

        //Stop both audio tracks if playing
        if (FindObjectOfType<AudioManager>().IsPlaying(SongInfo.Instance.instrumentalFile))
            FindObjectOfType<AudioManager>().Stop(SongInfo.Instance.instrumentalFile);
        
        if(FindObjectOfType<AudioManager>().IsPlaying(SongInfo.Instance.vocalsFile))
            FindObjectOfType<AudioManager>().Stop(SongInfo.Instance.vocalsFile);

        SceneManager.LoadScene("LevelSelect");
    }

    public void AddToCombo(int hit)
    {
        combo += 1;
        if(combo == 10)
            comboText.alpha = 1;
        DisplayCombo(hit);
    }//end of AddToCombo

    void DisplayCombo(int hit)
    {
        comboText.text = "Combo: " + combo;

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
