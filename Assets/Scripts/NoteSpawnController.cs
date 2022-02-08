using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawnController : MonoBehaviour
{
    public GameObject note;

    public float startOffset;
    public float repeatInterval;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnNote", startOffset, repeatInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnNote()
    {
        Instantiate(note, transform.position, note.transform.rotation);
    }
}
