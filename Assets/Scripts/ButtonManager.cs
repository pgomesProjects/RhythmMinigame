using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public int buttonIndex;
    public GameObject arrow;

    private Renderer rend;
    public Color idleColor;
    public Color pressColor;
    private Vector3 scale;
    private Vector3 scaleChange;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();

        scale = new Vector3(1.5f, 0.25f, 1.5f);
        scaleChange = new Vector3(1.5f, 0.125f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        switch(buttonIndex) {
            case 0:
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
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
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
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
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
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
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
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
