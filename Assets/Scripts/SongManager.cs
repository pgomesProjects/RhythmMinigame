using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SongManager : MonoBehaviour
{
    public float bpm;
    public float noteSpawnZ, buttonZ;
    public float scrollSpeed;
    public int measureBeats = 8;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI statText;
    public float songLength = 144;
    [HideInInspector]
    public float noteSpeed;
    [HideInInspector]
    public int combo = 0;
    [HideInInspector]
    public int[] stats;

    private AudioClip gameMusic;

    enum Hit { Perfect, Great, Good, Bad, Miss };

    // Start is called before the first frame update
    void Start()
    {
        noteSpeed = (noteSpawnZ - buttonZ) / 4 * scrollSpeed;
        //Stats: Perfect, Great, Good, Bad, Miss
        stats = new int[5];
        //Play instrumental and vocals at the same time
        FindObjectOfType<AudioManager>().Play("Sadness_Instrumental", 1.0f); // Play music upon level start
        FindObjectOfType<AudioManager>().Play("Sadness_Vocals", 1.0f); // Play music upon level start

        StartCoroutine(EndSong());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EndSong()
    {
        yield return new WaitForSeconds(songLength);

        SceneManager.LoadScene("LevelSelect");
    }

    public void AddToCombo(int hit)
    {
        combo += 1;
        comboText.text = "Combo: " + combo;
        switch (hit)
        {
            case (int)Hit.Perfect:
                stats[(int)Hit.Perfect] += 1;
                statText.text = "Perfect!";
                break;
            case (int)Hit.Great:
                stats[(int)Hit.Great] += 1;
                statText.text = "Great!";
                break;
        }
    }//end of AddToCombo

    public void ResetCombo(int hit)
    {
        combo = 0;
        comboText.text = "Combo: " + combo;
        switch(hit)
        {
            case (int)Hit.Good:
                stats[(int)Hit.Good] += 1;
                statText.text = "Good!";
                break;
            case (int)Hit.Bad:
                stats[(int)Hit.Bad] += 1;
                statText.text = "Bad!";
                break;
            case (int)Hit.Miss:
                stats[(int)Hit.Miss] += 1;
                statText.text = "Miss!";
                break;
        }
    }//end of AddToCombo
}
