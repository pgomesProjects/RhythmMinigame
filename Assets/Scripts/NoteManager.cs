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
                        Destroy(gameObject);
                    }
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        Destroy(gameObject);
                    }
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        Destroy(gameObject);
                    }
                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        Destroy(gameObject);
                    }
                    break;
            }
        }

        if (transform.position.z <= lowerBound)
            Destroy(gameObject);
    }

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
        }
    }
}
