using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPulse : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    public float normalFieldOfView = 60;
    public float zoomIntensity = 55;

    private float timer;
    private float currentTimer;

    private bool isZoomed;

    void Start()
    {
        isZoomed = false;
        //How many seconds are in one beat
        timer = SongInfo.Instance.bpm / 60.0f;
        virtualCamera.m_Lens.FieldOfView = normalFieldOfView;
    }

    void Update()
    {
        //Pulse the camera when not paused
        if (!FindObjectOfType<SongManager>().isPaused)
        {
            currentTimer += Time.deltaTime;

            //If the current timer hits the specified time, pulse the camera and reset
            if (currentTimer >= ((float)(timer / 2)))
            {
                isZoomed = true;

                currentTimer = 0;
            }

            //Use a lerp to zoom into the camera and start a coroutine to zoom back out
            if (isZoomed)
            {
                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, zoomIntensity, (float)(timer / SongInfo.Instance.measureBeats));
                StartCoroutine(EndZoom());
            }
        }
    }

    IEnumerator EndZoom()
    {
        yield return new WaitForSeconds((float)(timer / SongInfo.Instance.measureBeats));
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, normalFieldOfView, (float)(timer / SongInfo.Instance.measureBeats));
        isZoomed = false;
    }
}
