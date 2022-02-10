using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldManager : MonoBehaviour
{

    private float lowerBound = -7.0f;
    private ButtonManager currentButton;

    private Renderer rend;

    [HideInInspector]
    public Color holdColor;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        rend.material.SetColor("_Color", holdColor);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * FindObjectOfType<SongManager>().noteSpeed);

        if (transform.position.z <= lowerBound)
            Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            currentButton = other.GetComponent<ButtonManager>();
            switch (currentButton.buttonIndex)
            {
                case 0:
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                    {
                        Destroy(gameObject);
                    }
                    break;
                case 1:
                    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                    {
                        Destroy(gameObject);
                    }
                    break;
                case 2:
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                    {
                        Destroy(gameObject);
                    }
                    break;
                case 3:
                    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                    {
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }
}
