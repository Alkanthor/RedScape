using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyboardController : MonoBehaviour {

    [SerializeField]
    private Transform _player;
    [SerializeField]
    public float _appearDuration = 1f;

    private CanvasGroup _canvasGroup;
    private bool _isAppearing;
    private InputField _input;
    public string Name = "Body";
	// Use this for initialization
	void Start ()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _input = GetComponentInChildren<InputField>();
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("keyboard trigger enter " + other.name + " " + other.tag);
        if (other.name == Name && !_isAppearing)
        {
            if (_canvasGroup.alpha != 1)
                StartCoroutine(KeyboardFade(0, 1, _appearDuration));
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("keyboard trigger exit " + other.name + " " + other.tag);
        if (other.name == Name && !_isAppearing)
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
    }


}
