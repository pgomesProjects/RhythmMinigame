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
        timer = FindObjectOfType<SongManager>().bpm / 60;
        virtualCamera.m_Lens.FieldOfView = normalFieldOfView;
    }

    void Update()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= (timer / 2))
        {
            isZoomed = true;

            currentTimer = 0;
        }

        if (isZoomed)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, zoomIntensity, 0.125f);
            StartCoroutine(EndZoom());
        }
    }

    IEnumerator EndZoom()
    {
        yield return new WaitForSeconds(0.125f);
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, normalFieldOfView, 0.125f);
        isZoomed = false;
    }
}
