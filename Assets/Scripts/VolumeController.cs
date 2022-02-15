using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public GameObject[] volumeBars;
    public GameObject volumeObject;
    public static VolumeController Instance { get; private set; }
    
    private PlayerControls playerControls;
    private float volumeVar = 0.1f;
    private int volumeOffset = 0;
    private bool isGoingLeft = true;
    private bool volumeActive;
    private float timer = 1.0f;
    private float currentTimer;

    public Color activeColor;
    public Color inactiveColor;

    void Awake()
    {
        //Make sure this object does not destroy itself when loading between scenes
        if (Instance == null)
        {
            PlayerPrefs.SetFloat("MusicVolume", 1.0f);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerControls = new PlayerControls();
        playerControls.UI.LowerVolume.performed += _ => AdjustVolume();
        playerControls.UI.RaiseVolume.performed += _ => AdjustVolume();

    }

    // Start is called before the first frame update
    void Start()
    {
        volumeObject.SetActive(false);
        volumeActive = false;
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
        //Only show the volume 
        if (volumeActive)
        {
            currentTimer += Time.deltaTime;
            if(currentTimer >= timer)
            {
                volumeObject.SetActive(false);
                volumeActive = false;
            }
        }
    }

    private void AdjustVolume()
    {
        volumeActive = true;
        volumeObject.SetActive(true);
        currentTimer = 0;

        //If the player wants to lower the volume and the volume is greater than 0, lower the volume
        if (playerControls.UI.LowerVolume.ReadValue<float>() > 0 && PlayerPrefs.GetFloat("MusicVolume") > 0.0f)
        {
            PlayerPrefs.SetFloat("MusicVolume", Mathf.Round((PlayerPrefs.GetFloat("MusicVolume") - volumeVar) * 10) / 10);
            //Get rid of the offset if the user switches directions when adjusting volume
            if (!isGoingLeft)
            {
                isGoingLeft = true;
                volumeOffset = 0;
            }
            UpdateBar(PlayerPrefs.GetFloat("MusicVolume") * 10.0f, inactiveColor);
        }
        //If the player wants to raise the volume and the volume is less than 1, raise the volume
        else if (playerControls.UI.RaiseVolume.ReadValue<float>() > 0 && PlayerPrefs.GetFloat("MusicVolume") < 1.0f)
        {
            PlayerPrefs.SetFloat("MusicVolume", Mathf.Round((PlayerPrefs.GetFloat("MusicVolume") + volumeVar) * 10) / 10);
            //Provide an offset if the user switches directions when adjusting volume
            if (isGoingLeft)
            {
                isGoingLeft = false;
                volumeOffset = -1;
            }
            UpdateBar(PlayerPrefs.GetFloat("MusicVolume") * 10.0f, activeColor);
        }

    }//end of AdjustVolume

    private void UpdateBar(float index, Color currentColor)
    {
        Debug.Log("Volume: " + PlayerPrefs.GetFloat("MusicVolume"));

        volumeBars[(int)index + volumeOffset].GetComponent<Image>().color = currentColor;

        //If there is any song playing, update the volume immediately
        if(SongInfo.Instance.instrumentalFile != "" && SongInfo.Instance.vocalsFile != "")
        {
            FindObjectOfType<AudioManager>().ChangeVolume(SongInfo.Instance.instrumentalFile, SongInfo.Instance.instrumentalVolume * PlayerPrefs.GetFloat("MusicVolume"));
            FindObjectOfType<AudioManager>().ChangeVolume(SongInfo.Instance.vocalsFile, SongInfo.Instance.vocalsVolume * PlayerPrefs.GetFloat("MusicVolume"));
        }
    }//end of UpdateBarPos
}
