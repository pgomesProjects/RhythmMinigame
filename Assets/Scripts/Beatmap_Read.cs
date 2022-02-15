using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using UnityEngine.Networking;

public class Beatmap_Read : MonoBehaviour
{
    public GameObject[] noteSpawners;
    public GameObject note;
    public GameObject hold;

    [HideInInspector]
    public string[,] allNoteData;
    public float[] allNoteTimes;
    [HideInInspector]
    public bool isPlaying;

    private string fileName;
    private float gameTime;
    private int noteCount;
    private int currentNoteID;
    private string readFromFilePath;
    private string directory;

    // Start is called before the first frame update
    void Start()
    {
        fileName = SongInfo.Instance.beatmapFile;

        //Find server path if running on WebGL, if not use streamingAssetsPath
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            //Figure out how to read file from WebGL
            readFromFilePath = Application.dataPath + "/StreamingAssets/Data/" + fileName;
            directory = readFromFilePath;
        }
        else
        {
            readFromFilePath = Application.streamingAssetsPath + "/Data/" + fileName;
            directory = readFromFilePath;
        }

        //Get all lines from text file
        List<string> allNotes = File.ReadAllLines(directory).ToList();
        noteCount = allNotes.Count;
        allNoteData = new string[noteCount, 4];
        allNoteTimes = new float[noteCount];

        int counter = 0;
        foreach(string noteData in allNotes)
        {
            //Split the data using a comma delimiter
            string [] noteSplit = noteData.Split(',');
            for(int i = 0; i < 3; i++)
            {
                allNoteData[counter, i] = noteSplit[i];
                //If storing the beat, also store it in the master times array
                if (i == 1)
                    allNoteTimes[counter] = float.Parse(noteSplit[i]);
            }
            //Create an extra cell which says whether the note has been spawned or not
            allNoteData[counter, 3] = "false";
            counter++;
        }

        currentNoteID = 1;
        isPlaying = true;
    }

    private void Update()
    {
        //When the game is not paused, spawn the notes
        if (!FindObjectOfType<SongManager>().isPaused)
        {
            gameTime += Time.deltaTime;

            //Find the nearest beat with an offset to allow the notes to spawn early
            float nearestBeat = ((float)Math.Round((gameTime + ((SongInfo.Instance.measureBeats / 2) / SongInfo.Instance.scrollSpeed)) * SongInfo.Instance.measureBeats) / SongInfo.Instance.measureBeats);

            int[] allPossibleNotes = FindAllIndexOf(allNoteTimes, nearestBeat * SongInfo.Instance.measureBeats);

            if (allPossibleNotes.Length > 0)
            {
                for (int i = 0; i < allPossibleNotes.Length; i++)
                {
                    //If the note time is just about the same time as the game's recorded time with the offset, spawn the note
                    if (RoughlyEqual(gameTime + ((SongInfo.Instance.measureBeats / 2) / SongInfo.Instance.scrollSpeed), float.Parse(allNoteData[allPossibleNotes[i], 1]) / SongInfo.Instance.measureBeats) && allNoteData[allPossibleNotes[i], 3] == "false")
                    {
                        SpawnNote(allPossibleNotes[i]);
                    }
                }
            }
        }
    }

    private static int[] FindAllIndexOf(float[] values, float key)
    {
        //Selects all values that equals the given key and stores it in an array
        return values.Select((b, i) => object.Equals(b, key) ? i : -1).Where(i => i != -1).ToArray();
    }//end of FindAllIndexOf

    private bool RoughlyEqual(float a, float b)
    {
        //If two numbers are roughly equal to each other
        float treshold = 0.05f;
        return (Math.Abs(a - b) < treshold);
    }//end of RoughlyEqual

    private void SpawnNote(int currentIndex)
    {
        //Get the hold count and give the note an ID
        note.GetComponent<NoteManager>().holdCount = int.Parse(allNoteData[currentIndex, 2]);
        note.GetComponent<NoteManager>().noteID = currentNoteID;

        //Depending on the row the note is spawned, color the note, rotate the arrow accordingly, and then instantiate
        if (allNoteData[currentIndex, 0] == "left")
        {
            note.GetComponent<NoteManager>().noteColor = new Color32(204, 91, 154, 255);
            note.GetComponent<NoteManager>().noteRot = 90.0f;
            Instantiate(note, noteSpawners[0].transform.position, note.transform.rotation);
        }

        else if (allNoteData[currentIndex, 0] == "down")
        {
            note.GetComponent<NoteManager>().noteColor = new Color32(0, 231, 254, 255);
            note.GetComponent<NoteManager>().noteRot = 0.0f;
            Instantiate(note, noteSpawners[1].transform.position, note.transform.rotation);
        }

        else if (allNoteData[currentIndex, 0] == "up")
        {
            note.GetComponent<NoteManager>().noteColor = new Color32(4, 197, 11, 255);
            note.GetComponent<NoteManager>().noteRot = 180.0f;
            Instantiate(note, noteSpawners[2].transform.position, note.transform.rotation);
        }

        else if (allNoteData[currentIndex, 0] == "right")
        {
            note.GetComponent<NoteManager>().noteColor = new Color32(255, 78, 68, 255);
            note.GetComponent<NoteManager>().noteRot = -90.0f;
            Instantiate(note, noteSpawners[3].transform.position, note.transform.rotation);
        }
        currentNoteID += 1;
        //Tell the array that the note has been spawned
        allNoteData[currentIndex, 3] = "true";
    }//end of SpawnNote

}
