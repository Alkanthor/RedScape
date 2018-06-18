using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCabinetPuzzle : MonoBehaviour {


    public string DoorKeySymbolLeft;
    public string DoorKeySymbolRight;
    public GameObject[] SecretDoorsKeys;
    public GameObject SecretDoor;
    public float OpenDoorLimit = 1;
    public float OpenDoorSpeed = 1;
    //right - 0, left - 1
    private int[] code;
    private AudioSource _secretDoorSound;
    private Dictionary<GameObject, bool> _doorKeyCheck;
    private bool _isOpening;
    private bool _isClosing;
    private Coroutine _doorCoroutine;
    private Vector3 _initDoorPosition;

    private bool _isCombintionCorrect;
    public bool IsCombintionCorrect
    {
        get
        {
            return _isCombintionCorrect;
        }
        set
        {
            _isCombintionCorrect = value;
            if(_isCombintionCorrect)
            {
                Debug.Log("Opening secrect cabinet door");
                var doorTo = _initDoorPosition + SecretDoor.transform.forward * OpenDoorLimit * -1;
                if (_doorCoroutine != null) StopCoroutine(_doorCoroutine);
                _isClosing = false;
                _isOpening = true;
                _doorCoroutine = StartCoroutine(OpeningDoor(SecretDoor.transform.position, doorTo, OpenDoorSpeed));
            }
            else
            {
                Debug.Log("Closing secrect cabinet door");
                if (_doorCoroutine != null) StopCoroutine(_doorCoroutine);
                _isOpening = false;
                _isClosing = true;
                _doorCoroutine = StartCoroutine(OpeningDoor(SecretDoor.transform.position, _initDoorPosition, OpenDoorSpeed));
            }
        }
    }
    // Use this for initialization
    void Start () {
        _secretDoorSound = SecretDoor.GetComponent<AudioSource>();
        _initDoorPosition = SecretDoor.transform.position;
        InitPuzzle();
	}
	
    private void InitPuzzle()
    {

        code = new int[SecretDoorsKeys.Length];
        _doorKeyCheck = new Dictionary<GameObject, bool>();
        for(int i = 0; i < SecretDoorsKeys.Length; ++i)
        {
            var secretDoorController = SecretDoorsKeys[i].GetComponent<SecretCabinetDoorController>();
            var code = UnityEngine.Random.Range(0, 1000) % 2;
            var doorKey = "";
            if (code == 0) doorKey = DoorKeySymbolRight;
            if (code == 1) doorKey = DoorKeySymbolLeft;
            secretDoorController.SetCode(code, doorKey);
            secretDoorController.OnIsDoorOnCorrectSide.AddListener(OnIsDoorOnCorrectSide);
        }
    }

    private void OnIsDoorOnCorrectSide(GameObject door, bool value)
    {
        if(_doorKeyCheck.ContainsKey(door))
        {
            _doorKeyCheck[door] = value;
        }
        else
        {
            _doorKeyCheck.Add(door, value);
        }
        CheckCombination();
    }

    private void CheckCombination()
    {
        if(!_doorKeyCheck.ContainsValue(false))
        {
            IsCombintionCorrect = true;
        }
        else
        {
            IsCombintionCorrect = false;
        }

    }

    IEnumerator OpeningDoor(Vector3 from, Vector3 to, float speed)
    {
        yield return new WaitForSeconds(0.5f);
        _secretDoorSound.Play();
        float step = (speed / (from - to).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            SecretDoor.transform.position = Vector3.Lerp(from, to, t); // Move objectToMove closer to b
            if (!_isClosing && !_isOpening)
            {
                _secretDoorSound.Stop();
            }
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame

        }
        SecretDoor.transform.position = to;
        _isOpening = false;
        _isClosing = false;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
