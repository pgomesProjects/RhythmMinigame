using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public float bpm;
    public float noteSpawnZ, buttonZ;
    public float scrollSpeed;
    [HideInInspector]
    public float noteSpeed;

    // Start is called before the first frame update
    void Start()
    {
        noteSpeed = (noteSpawnZ - buttonZ) / 4 * scrollSpeed;
        //Play instrumental and vocals at the same time
        FindObjectOfType<AudioManager>().Play("Sadness_Instrumental", 1.0f); // Play music upon level start
        FindObjectOfType<AudioManager>().Play("Sadness_Vocals", 1.0f); // Play music upon level start
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
