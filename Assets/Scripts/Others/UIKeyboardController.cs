using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyboardController : MonoBehaviour {


    public UnityEvents.UnityEventString OnInputEntered;

    public AudioSource Sound;


    [SerializeField]
    private float _appearDuration = 1f;
    [SerializeField]
    private string _playerCollisionName = "[VRTK][AUTOGEN][BodyColliderContainer]";
    [SerializeField]
    private string _inputCorrectText;
    [SerializeField]
    private string _inputIncorrectText;
    [SerializeField]
    private float _correctTimeInterval = 2;

    private CanvasGroup _canvasGroup;
    private bool _isAppearing;
    private InputField _input;


    // Use this for initialization
    void Start ()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _input = GetComponentInChildren<InputField>();

    }

    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("keyboard trigger enter " + other.name + " " + other.tag);
        if (other.name == _playerCollisionName && !_isAppearing)
        {
            if (_canvasGroup.alpha != 1)
                StartCoroutine(KeyboardFade(0, 1, _appearDuration));
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
       // Debug.Log("keyboard trigger exit " + other.name + " " + other.tag);
        if (other.name == _playerCollisionName && !_isAppearing)
        {
            if (_canvasGroup.alpha != 0)
                StartCoroutine(KeyboardFade(1, 0, _appearDuration));
        }
    }


    IEnumerator KeyboardFade(float from, float to, float duration)
    {
        _isAppearing = true;
        var curve = new AnimationCurve(new Keyframe[]
        {
            new Keyframe(0, from),
            new Keyframe(duration, to),
        });

        var time = 0.0f;
        while(time < duration)
        {
            _canvasGroup.alpha = curve.Evaluate(time);
            time += Time.deltaTime;
            yield return null;

        }
        _canvasGroup.alpha = curve.Evaluate(duration);
        _isAppearing = false;
    }

    public void ClickKey(string character)
    {
        _input.text += character;
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

    public void CorrectInputCheck(bool correct)
    {
        Debug.Log(this.name + ": correct input check");
        StartCoroutine(CorrectInput(correct));
    }

    IEnumerator CorrectInput(bool correct)
    {
        _input.textComponent.alignment = TextAnchor.MiddleCenter;
        if(correct)
        {
            _input.text = _inputCorrectText;
        }
        else
        {
            _input.text = _inputIncorrectText;
        }
        Sound.Play();
        yield return new WaitForSeconds(_correctTimeInterval);
        _input.textComponent.alignment = TextAnchor.MiddleLeft;
        _input.text = "";
    }
}
