using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NoteManager : MonoBehaviour
{
    private float lowerBound = -7.0f;
    private bool canHit = false;
    private ButtonManager currentButton;

    private Renderer rend;

    [HideInInspector]
    public Color noteColor;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        rend.material.SetColor("_Color", noteColor);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * FindObjectOfType<SongManager>().noteSpeed);

        //Check to see if note can be hit
        if (canHit)
        {
            switch (currentButton.buttonIndex)
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        NoteHit();
                    }
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        NoteHit();
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        NoteHit();
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        NoteHit();
                    }
                    break;
            }
        }

        if (transform.position.z <= lowerBound)
            Destroy(gameObject);
    }

    private void NoteHit()
    {
        if(Mathf.Abs(currentButton.transform.position.z - gameObject.transform.position.z) <= 0.19f)
            FindObjectOfType<SongManager>().AddToCombo(0);
        else if (Mathf.Abs(currentButton.transform.position.z - gameObject.transform.position.z) <= 0.3f)
            FindObjectOfType<SongManager>().AddToCombo(1);
        else if (Mathf.Abs(currentButton.transform.position.z - gameObject.transform.position.z) <= 0.494f)
            FindObjectOfType<SongManager>().ResetCombo(2);
        else if (Mathf.Abs(currentButton.transform.position.z - gameObject.transform.position.z) <= 0.74f)
            FindObjectOfType<SongManager>().ResetCombo(3);
        Destroy(gameObject);
    }//end of NoteHit
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            canHit = true;
            currentButton = other.GetComponent<ButtonManager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            canHit = false;
            currentButton = null;
            FindObjectOfType<SongManager>().ResetCombo(4);
        }
    }
}
