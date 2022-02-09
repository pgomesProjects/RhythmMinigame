using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldSpawnController : MonoBehaviour
{
    public GameObject holdObject;

    public int length = 1;

    private GameObject[] holdNotes;

    // Start is called before the first frame update
    void Start()
    {
        holdNotes = new GameObject[length];
        for(int i = 0; i < length; i++)
            holdNotes[i] = holdObject;

        SpawnHold();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnHold()
    {
        Vector3 spawnLoc = transform.position;

        for(int i = 0; i < length; i++)
            Instantiate(holdNotes[i], new Vector3(spawnLoc.x, spawnLoc.y, spawnLoc.z + i), holdNotes[i].transform.rotation);
    }
}
