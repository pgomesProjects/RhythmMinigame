using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class Beatmap_Read : MonoBehaviour
{
    public string fileName;
    public GameObject[] noteSpawners;
    public GameObject note;
    public GameObject hold;

    [HideInInspector]
    public string[,] allNoteData;
    [HideInInspector]
    public bool isPlaying;

    private float gameTime;
    private int noteCount;

    // Start is called before the first frame update
    void Start()
    {
        string readFromFilePath = Application.dataPath + "/Data/" + fileName;

        //Get all lines from text file
        List<string> allNotes = File.ReadAllLines(readFromFilePath).ToList();
        noteCount = allNotes.Count;
        allNoteData = new string[noteCount, 4];

        int counter = 0;
        foreach(string noteData in allNotes)
        {
            string [] noteSplit = noteData.Split(',');
            for(int i = 0; i < 3; i++)
            {
                allNoteData[counter, i] = noteSplit[i];
            }
            allNoteData[counter, 3] = "false";
            counter++;
        }

        isPlaying = true;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        //Debug.Log("Time: " + (int)gameTime);
        for(int i = 0; i < noteCount; i++)
        {
            if(RoughlyEqual(gameTime, (float.Parse(allNoteData[i, 1])) - (4 / FindObjectOfType<SongManager>().scrollSpeed)) && allNoteData[i, 3] == "false")
            {
                SpawnNote(i);
            }
        }

    }

    private bool RoughlyEqual(float a, float b)
    {
        //If two numbers are roughly equal to each other
        float treshold = 0.05f;
        return (Math.Abs(a - b) < treshold);
    }//end of RoughlyEqual

    private void SpawnNote(int currentIndex)
    {
        int numOfHoldNotes = int.Parse(allNoteData[currentIndex, 2]);
        if (allNoteData[currentIndex, 0] == "left")
        {
            note.GetComponent<NoteManager>().noteColor = new Color32(204, 91, 154, 255);
            Instantiate(note, noteSpawners[0].transform.position, note.transform.rotation);
            
            //If there are hold segments, generate them
            if (numOfHoldNotes > 0)
            {
                Vector3 spawnLoc = noteSpawners[0].transform.position;
                for (int i = 1; i <= numOfHoldNotes; i++)
                {
                    hold.GetComponent<HoldManager>().holdColor = new Color32(204, 91, 154, 255);
                    Instantiate(hold, new Vector3(spawnLoc.x, spawnLoc.y, spawnLoc.z + i), hold.transform.rotation);
                }
            }
        }

        else if (allNoteData[currentIndex, 0] == "down")
        {
            note.GetComponent<NoteManager>().noteColor = new Color32(0, 231, 254, 255);
            Instantiate(note, noteSpawners[1].transform.position, note.transform.rotation);

            //If there are hold segments, generate them
            if (numOfHoldNotes > 0)
            {
                Vector3 spawnLoc = noteSpawners[1].transform.position;
                for (int i = 1; i <= numOfHoldNotes; i++)
                {
                    hold.GetComponent<HoldManager>().holdColor = new Color32(0, 231, 254, 255);
                    Instantiate(hold, new Vector3(spawnLoc.x, spawnLoc.y, spawnLoc.z + i), hold.transform.rotation);
                }
            }
        }

        else if (allNoteData[currentIndex, 0] == "up")
        {
            note.GetComponent<NoteManager>().noteColor = new Color32(4, 197, 11, 255);
            Instantiate(note, noteSpawners[2].transform.position, note.transform.rotation);

            //If there are hold segments, generate them
            if (numOfHoldNotes > 0)
            {
                Vector3 spawnLoc = noteSpawners[2].transform.position;
                for (int i = 1; i <= numOfHoldNotes; i++)
                {
                    hold.GetComponent<HoldManager>().holdColor = new Color32(4, 197, 11, 255);
                    Instantiate(hold, new Vector3(spawnLoc.x, spawnLoc.y, spawnLoc.z + i), hold.transform.rotation);
                }
            }
        }

        else if (allNoteData[currentIndex, 0] == "right")
        {
            note.GetComponent<NoteManager>().noteColor = new Color32(255, 78, 68, 255);
            Instantiate(note, noteSpawners[3].transform.position, note.transform.rotation);

            //If there are hold segments, generate them
            if (numOfHoldNotes > 0)
            {
                Vector3 spawnLoc = noteSpawners[3].transform.position;
                for (int i = 1; i <= numOfHoldNotes; i++)
                {
                    hold.GetComponent<HoldManager>().holdColor = new Color32(255, 78, 68, 255);
                    Instantiate(hold, new Vector3(spawnLoc.x, spawnLoc.y, spawnLoc.z + i), hold.transform.rotation);
                }
            }
        }
        allNoteData[currentIndex, 3] = "true";
    }//end of SpawnNote

}
