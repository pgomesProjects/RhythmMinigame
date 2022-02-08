using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public int buttonIndex;

    private Renderer rend;
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
                if (Input.GetKey(KeyCode.A))
                {
                    gameObject.transform.localScale = scaleChange;
                }
                else
                {
                    gameObject.transform.localScale = scale;
                }
                break;
            case 1:
                if (Input.GetKey(KeyCode.S))
                {
                    gameObject.transform.localScale = scaleChange;
                }
                else
                {
                    gameObject.transform.localScale = scale;
                }
                break;
            case 2:
                if (Input.GetKey(KeyCode.W))
                {
                    gameObject.transform.localScale = scaleChange;
                }
                else
                {
                    gameObject.transform.localScale = scale;
                }
                break;
            case 3:
                if (Input.GetKey(KeyCode.D))
                {
                    gameObject.transform.localScale = scaleChange;
                }
                else
                {
                    gameObject.transform.localScale = scale;
                }
                break;
        }   
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Note"))
        {
            switch (buttonIndex)
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        Destroy(other.gameObject);
                    }
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        Destroy(other.gameObject);
                    }

                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        Destroy(other.gameObject);
                    }

                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        Destroy(other.gameObject);
                    }
                    break;
            }
        }
    }
}
