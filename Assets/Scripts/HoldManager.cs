using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldManager : MonoBehaviour
{

    private float lowerBound = -5.6f;
    private ButtonManager currentButton;
    private Renderer rend;
    private bool canHold;

    [HideInInspector]
    public int parentID;
    [HideInInspector]
    public int noteIndex;
    [HideInInspector]
    public int allNotesCount;
    [HideInInspector]
    public Color holdColor;

    private PlayerControls playerControls;

    private bool[] keyPressed = new bool[4] { false, false, false, false };
    enum Controls { LEFT, DOWN, UP, RIGHT };

    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Controls.Left.performed += _ => { keyPressed[(int)Controls.LEFT] = true; };
        playerControls.Controls.Left.canceled += _ => { keyPressed[(int)Controls.LEFT] = false; };
        playerControls.Controls.Down.performed += _ => { keyPressed[(int)Controls.DOWN] = true; };
        playerControls.Controls.Down.canceled += _ => { keyPressed[(int)Controls.DOWN] = false; };
        playerControls.Controls.Up.performed += _ => { keyPressed[(int)Controls.UP] = true; };
        playerControls.Controls.Up.canceled += _ => { keyPressed[(int)Controls.UP] = false; };
        playerControls.Controls.Right.performed += _ => { keyPressed[(int)Controls.RIGHT] = true; };
        playerControls.Controls.Right.canceled += _ => { keyPressed[(int)Controls.RIGHT] = false; };
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        canHold = false;
        rend = gameObject.GetComponent<Renderer>();
        rend.material.SetColor("_Color", holdColor);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * FindObjectOfType<SongManager>().noteSpeed);

        if (transform.position.z <= lowerBound)
            Destroy(gameObject);

        //Check to see if the player releases on the right hold note
        if(canHold)
            CheckRelease();
    }

    private void CheckRelease()
    {
        switch (currentButton.buttonIndex)
        {
            case 0:
                if (!keyPressed[(int)Controls.LEFT])
                {
                    if(noteIndex != allNotesCount)
                    {
                        DestroyHold();
                    }
                }
                break;
            case 1:
                if (!keyPressed[(int)Controls.DOWN])
                {
                    if (noteIndex != allNotesCount)
                    {
                        DestroyHold();
                    }
                }
                break;
            case 2:
                if (!keyPressed[(int)Controls.UP])
                {
                    if (noteIndex != allNotesCount)
                    {
                        DestroyHold();
                    }
                }
                break;
            case 3:
                if (!keyPressed[(int)Controls.RIGHT])
                {
                    if (noteIndex != allNotesCount)
                    {
                        DestroyHold();
                    }
                }
                break;
        }

    }//end of CheckRelease

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            currentButton = other.GetComponent<ButtonManager>();
            if(noteIndex != 1)
            {
                switch (currentButton.buttonIndex)
                {
                    case 0:
                        if (!keyPressed[(int)Controls.LEFT])
                        {
                            DestroyHold();
                        }
                        break;
                    case 1:
                        if (!keyPressed[(int)Controls.DOWN])
                        {
                            DestroyHold();
                        }
                        break;
                    case 2:
                        if (!keyPressed[(int)Controls.UP])
                        {
                            DestroyHold();
                        }
                        break;
                    case 3:
                        if (!keyPressed[(int)Controls.RIGHT])
                        {
                            DestroyHold();
                        }
                        break;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            currentButton = other.GetComponent<ButtonManager>();
            if (noteIndex != allNotesCount)
            {
                switch (currentButton.buttonIndex)
                {
                    case 0:
                        if (!keyPressed[(int)Controls.LEFT])
                        {
                            DestroyHold();
                        }
                        break;
                    case 1:
                        if (!keyPressed[(int)Controls.DOWN])
                        {
                            DestroyHold();
                        }
                        break;
                    case 2:
                        if (!keyPressed[(int)Controls.UP])
                        {
                            DestroyHold();
                        }
                        break;
                    case 3:
                        if (!keyPressed[(int)Controls.RIGHT])
                        {
                            DestroyHold();
                        }
                        break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            currentButton = other.GetComponent<ButtonManager>();
            switch (currentButton.buttonIndex)
            {
                case 0:
                    if (keyPressed[(int)Controls.LEFT])
                    {
                        FindObjectOfType<SongManager>().AddToCombo(-1);
                        Destroy(gameObject);
                    }
                    break;
                case 1:
                    if (keyPressed[(int)Controls.DOWN])
                    {
                        FindObjectOfType<SongManager>().AddToCombo(-1);
                        Destroy(gameObject);
                    }
                    break;
                case 2:
                    if (keyPressed[(int)Controls.UP])
                    {
                        FindObjectOfType<SongManager>().AddToCombo(-1);
                        Destroy(gameObject);
                    }
                    break;
                case 3:
                    if (keyPressed[(int)Controls.RIGHT])
                    {
                        FindObjectOfType<SongManager>().AddToCombo(-1);
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }

    public void DestroyHold()
    {
        HoldManager[] holdNotes = FindObjectsOfType<HoldManager>();
        for (int i = 0; i < holdNotes.Length; i++)
            if (holdNotes[i].parentID == parentID)
                Destroy(holdNotes[i].gameObject);
        FindObjectOfType<SongManager>().ResetCombo(4);
    }//end of DestroyHold
}
