using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyboardController : MonoBehaviour {

    [SerializeField]
    private Transform _player;
    [SerializeField]
    private float _appearDistance;
    [SerializeField]
    public float _appearDuration = 1f;

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

    // Update is called once per frame
    void Update()
    {

        var distance = Vector3.Distance(_player.position, this.transform.position);
        if (distance < _appearDistance && !_isAppearing)
        {
            if(_canvasGroup.alpha != 1)
            StartCoroutine(KeyboardFade(0, 1, _appearDuration));
        }
        else if(distance > _appearDistance && !_isAppearing)
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
