using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [SerializeField]
    private GameObject _staticCameras;
    [SerializeField]
    private GameObject _carCamera;

    private int _activeCameraIndex;
    private List<GameObject> _cameras = new List<GameObject>();

	// Use this for initialization
	void Start () {
        var staticCameras = _staticCameras.transform.GetComponentsInChildren<Transform>();
        foreach (var camera in staticCameras)
        {
            _cameras.Add(camera.gameObject);
        }
        _cameras.RemoveAt(0);
        _cameras.Add(_carCamera);
        _activeCameraIndex = 0;
        SwitchCamera();
    }

    // Update is called once per frame
    void Update () {
        SwitchCamera();
    }

    public void NextCamera()
    {
        _activeCameraIndex = (_activeCameraIndex + 1) % _cameras.Count;
        SwitchCamera();
    }

    public void PreviousCamera()
    {
        _activeCameraIndex = (_activeCameraIndex - 1) % _cameras.Count;
        SwitchCamera();
    }

    public void SwitchCamera()
    {
        foreach(var c in _cameras)
        {
            c.SetActive(false);
        }
        _cameras[_activeCameraIndex].SetActive(true);
    }
}
