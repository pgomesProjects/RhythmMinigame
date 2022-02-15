using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonManager : MonoBehaviour
{
    public int buttonIndex;
    public GameObject arrow;

    private Renderer rend;
    public Color idleColor;
    public Color pressColor;
    private Vector3 scale;
    private Vector3 scaleChange;

    public PlayerControls playerControls;

    private bool[] keyPressed = new bool[4] {false, false, false, false};

    enum Controls { LEFT, DOWN, UP, RIGHT};

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
        rend = gameObject.GetComponent<Renderer>();

        scale = new Vector3(1.5f, 0.25f, 1.5f);
        scaleChange = new Vector3(1.5f, 0.125f, 1.5f);
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
        //Depending on the note pressed, scale the note and change the color of the note. If released, reset
        switch(buttonIndex) {
            case 0:
                if (keyPressed[(int)Controls.LEFT])
                {
                    rend.material.SetColor("_Color", pressColor);
                    arrow.GetComponent<Renderer>().material.SetColor("_Color", pressColor);
                    gameObject.transform.localScale = scaleChange;
                }
                else
                {
                    rend.material.SetColor("_Color", idleColor);
                    arrow.GetComponent<Renderer>().material.SetColor("_Color", idleColor);
                    gameObject.transform.localScale = scale;
                }
                break;
            case 1:
                if (keyPressed[(int)Controls.DOWN])
                {
                    rend.material.SetColor("_Color", pressColor);
                    arrow.GetComponent<Renderer>().material.SetColor("_Color", pressColor);
                    gameObject.transform.localScale = scaleChange;
                }
                else
                {
                    rend.material.SetColor("_Color", idleColor);
                    arrow.GetComponent<Renderer>().material.SetColor("_Color", idleColor);
                    gameObject.transform.localScale = scale;
                }
                break;
            case 2:
                if (keyPressed[(int)Controls.UP])
                {
                    rend.material.SetColor("_Color", pressColor);
                    arrow.GetComponent<Renderer>().material.SetColor("_Color", pressColor);
                    gameObject.transform.localScale = scaleChange;
                }
                else
                {
                    rend.material.SetColor("_Color", idleColor);
                    arrow.GetComponent<Renderer>().material.SetColor("_Color", idleColor);
                    gameObject.transform.localScale = scale;
                }
                break;
            case 3:
                if (keyPressed[(int)Controls.RIGHT])
                {
                    rend.material.SetColor("_Color", pressColor);
                    arrow.GetComponent<Renderer>().material.SetColor("_Color", pressColor);
                    gameObject.transform.localScale = scaleChange;
                }
                else
                {
                    rend.material.SetColor("_Color", idleColor);
                    arrow.GetComponent<Renderer>().material.SetColor("_Color", idleColor);
                    gameObject.transform.localScale = scale;
                }
                break;
        }   
    }
}
