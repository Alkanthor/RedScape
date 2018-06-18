using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour {

    public GameObject _baseCanvas;
    public GameObject _captchaCanvas;
    public GameObject _codeCanvas;

	// Use this for initialization
	void Start () {
        _baseCanvas = GameObject.Find("BaseCanvas");
        _captchaCanvas = GameObject.Find("CaptchaCanvas");
        _codeCanvas = GameObject.Find("CodeCanvas");

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ReturnToMainScreen()
    {
        _baseCanvas.SetActive(true);
        _captchaCanvas.SetActive(false);
        _codeCanvas.SetActive(false);
    }

    public void InsertCode()
    {
        _baseCanvas.SetActive(false);
        _captchaCanvas.SetActive(false);
        _codeCanvas.SetActive(true);
    }

    public void InsertCaptcha()
    {
        _baseCanvas.SetActive(false);
        _captchaCanvas.SetActive(true);
        _codeCanvas.SetActive(false);
    }
}
