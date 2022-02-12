using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class NoteManager : MonoBehaviour
{
    private float lowerBound = -5.4f;
    private bool canHit = false;
    private ButtonManager currentButton;

    private Renderer rend;

    public GameObject holdObject;
    public List<GameObject> holdNotes;

    [HideInInspector]
    public int noteID;
    [HideInInspector]
    public float noteRot;
    [HideInInspector]
    public int holdCount;
    [HideInInspector]
    public Color noteColor;

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

    // Start is called before the first frame update
    void Start()
    {
        //Generate note color
        rend = gameObject.GetComponent<Renderer>();
        rend.material.SetColor("_Color", noteColor);

        //Generate arrow rotation
        gameObject.transform.Find("Arrow").GetComponent<Renderer>().material.SetColor("_Color", noteColor);
        gameObject.transform.Find("Arrow").Rotate(0.0f, noteRot, 0.0f, Space.World);

        //If there are hold segments, generate them
        if (holdCount > 0)
        {
            Vector3 spawnLoc = gameObject.transform.position;
            for (int i = 1; i <= holdCount; i++)
            {
                holdObject.GetComponent<HoldManager>().holdColor = noteColor;
                holdObject.GetComponent<HoldManager>().parentID = noteID;
                holdObject.GetComponent<HoldManager>().noteIndex = i;
                holdObject.GetComponent<HoldManager>().allNotesCount = holdCount;
                Instantiate(holdObject, new Vector3(spawnLoc.x, spawnLoc.y, spawnLoc.z + i), holdObject.transform.rotation);
            }
        }
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * FindObjectOfType<SongManager>().noteSpeed);

        if (canHit)
        {
            //Check to see if note can be hit
            switch (currentButton.buttonIndex)
            {
                case 0:
                    if (keyPressed[(int)Controls.LEFT])
                        NoteHit();
                    break;
                case 1:
                    if (keyPressed[(int)Controls.DOWN])
                        NoteHit();
                    break;
                case 2:
                    if (keyPressed[(int)Controls.UP])
                        NoteHit();
                    break;
                case 3:
                    if (keyPressed[(int)Controls.RIGHT])
                        NoteHit();
                    break;
            }
        }

        if (transform.position.z <= lowerBound)
        {
            canHit = false;
            currentButton = null;
            FindObjectOfType<SongManager>().ResetCombo(4);
            Destroy(gameObject);
        }
    }

    private void NoteHit()
    {
        if (Mathf.Abs(currentButton.transform.position.z - gameObject.transform.position.z) <= 0.3f)
            FindObjectOfType<SongManager>().AddToCombo(0);
        else if (Mathf.Abs(currentButton.transform.position.z - gameObject.transform.position.z) <= 0.75f)
            FindObjectOfType<SongManager>().AddToCombo(1);
        else if (currentButton.transform.position.z - gameObject.transform.position.z <= 1.0f)
            FindObjectOfType<SongManager>().AddToCombo(2);
        else if (currentButton.transform.position.z - gameObject.transform.position.z <= 1.64f)
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
}
