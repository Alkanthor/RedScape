﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeManager : MonoBehaviour {

    public UnityEvents.UnityEventString OnInputEntered;
    public AudioSource Sound;


    [SerializeField]
    private string _inputCorrectText;
    [SerializeField]
    private string _inputIncorrectText;
    [SerializeField]
    private float _correctTimeInterval = 2;

    private CanvasGroup _canvasGroup;
    private bool _isAppearing;
    private InputField _input;
    private bool _canWrite;


    // Use this for initialization
    void Start()
    {
        _canWrite = true;
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _input = GetComponentInChildren<InputField>();

    }

    public void ClickKey(string character)
    {
        if (_canWrite)
        {
            _input.text += character;
        }

    }

    public void Clear()
    {
        _input.text = "";
    }

    public void Enter()
    {
        Debug.Log("entered input is " + _input.text);
        OnInputEntered.Invoke(_input.text);
    }

    public void CorrectInputCheck(GameObject door, bool correct)
    {
        Debug.Log(this.name + ": correct input check");
        StartCoroutine(CorrectInput(correct));
    }

    IEnumerator CorrectInput(bool correct)
    {
        _canWrite = false;
        _input.textComponent.alignment = TextAnchor.MiddleCenter;
        if (correct)
        {
            _input.text = _inputCorrectText;
        }
        else
        {
            _input.text = _inputIncorrectText;
        }
        yield return new WaitForSeconds(_correctTimeInterval);
        _input.textComponent.alignment = TextAnchor.MiddleLeft;
        _input.text = "";
        _canWrite = true;
    }


}
